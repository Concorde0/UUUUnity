//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BeeAttackController : MonoBehaviour
//{
//    public GameObject player;
//    public FlyEnemy currentEnemy;
//    private Vector2 originalEnemyPosition;
//    public Vector3 playerPosition;
//    public Attack attack;
//    private void Awake()
//    {
        
//        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
//    }
//    private void Update()
//    {
        
//    }
    
//    public void ChasePlayer()
//    {
//        if (currentEnemy.flyFoundPlayer == true)
//        {
//            Vector2 enemyPosition = transform.position;
//            Vector2 playerPosition = player.transform.position;
//            Vector2 direction = (playerPosition - enemyPosition).normalized;
//            //turn
//            if (direction.x <= 0)
//            {                
//                transform.localScale = new Vector3(-1, 1, 1);                
//            }
//            if(direction.x >= 0)
//            {
//                transform.localScale = new Vector3(1, 1, 1);
//            }
//            Vector2.MoveTowards(enemyPosition, enemyPosition + direction, currentEnemy.normalSpeed * Time.deltaTime);

//        }
//        else
//        {
//            Vector2 currentPosition = transform.position;
//            Vector2 direction = (originalEnemyPosition - currentPosition  ).normalized;
//            if (direction.x <= 0)
//            {
//                transform.localScale = new Vector3(-1, 1, 1);
//            }
//            if (direction.x >= 0)
//            {
//                transform.localScale = new Vector3(1, 1, 1);
//            }
//            Vector2.MoveTowards(currentPosition, originalEnemyPosition + direction, currentEnemy.normalSpeed * Time.deltaTime);

//            Vector2 dir = (playerPosition - currentEnemy.transform.position).normalized;
//        }
//    }

//    //到底传什么变量
//    private IEnumerator AttackWait(Vector2 dir)
//    {
        
//        if (attack.dealsDamage == true)
//        {
//            currentEnemy.anim.SetBool("attack", true);
//            currentEnemy.rb.velocity = Vector2.zero;
//            yield return new WaitForSeconds(0.5f);
//            currentEnemy.anim.SetBool("attack", false);
//            currentEnemy.rb.velocity = dir * currentEnemy.normalSpeed;
//        }
        

//    }
//}
