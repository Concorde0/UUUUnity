using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Knight : GroundEnemy
{
    public bool isAttack = false;
    
    protected override void Awake()
    {
        base.Awake();
        patrolState = new KnightPatrolState();
        attack1State = new KnightAttack1State();
        attack2State = new KnightAttack2State();
        idle2State = new KnightIdleState();
        stabState = new KnightStabState();
        whackState = new KnightWhackState();
    }
    
    protected override void FixedUpdate()
    {
        if (!isHurt & !isDead && !wait && !attackPlayer && !isAttack)
        {
            Move();
        }
        if(!isDead)
        AttackPlayer();

        //DistanceFoundPlayer();
        currentState.PhysicsUpdate();
    }

    //public int GetRandomNumber(int min, int max)
    //{
    //    return Random.Range(min, max);
    //}
    public override void TimeCounter()
    {
        if (wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                wait = false;
                waitTimeCounter = waitTime;
            }

        }
    }

    public override void Move()
    {
        rb.velocity = new Vector2(currentSpeed * -faceDir.x * Time.deltaTime, rb.velocity.y);
    }


   
    public virtual void AttackPlayer()
    {  
        StartCoroutine(Fix());
    }
    public IEnumerator Fix()
    {
        yield return new WaitForSeconds(0.05f); 
        if (Vector2.Distance(transform.position, playerPos.position) < attackDistance)
        {
            attackPlayer = true;
            //Debug.Log("AttackPlayer");
        }
    }

    //public void DistanceFoundPlayer()
    //{

    //    if (Vector2.Distance(transform.position, playerPos.position) < foundDistance)
    //    {
    //        foundPlayer = true;
    //        //Debug.Log("FoundPlayer");
    //    }
    //}
    public override bool FoundPlayer()
    {
        Debug.Log("Found");
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize.normalized, 0, faceDir, checkDistance, attackLayer);
    }
    public void DistanceFoundPlayer()
    {
        if(foundPlayer)
        {
            foundPlayer = true;
        }
    }

    public override void OnDie()
    {
        gameObject.layer = 2;
        anim.SetBool("dead", true);
        isDead = false;
        StartCoroutine(OnDead());
    }
    public IEnumerator OnDead()
    {
        //Debug.Log("IE");
        yield return new WaitForSeconds(0.05f);
        anim.SetBool("dead", false);
    }

    public virtual void BloodEffect()
    {
        Vector3 tmpTrans = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
        Instantiate(bloodEffect, tmpTrans, Quaternion.identity);
    }

    public override void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);
    }
    //public override void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset + new Vector3(checkDistance * -transform.localScale.x, 0), 0.2f);
    //}

}
