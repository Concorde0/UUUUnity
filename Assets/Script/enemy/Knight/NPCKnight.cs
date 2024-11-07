using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class NPCKnight : MonoBehaviour, IInteractable
{
    public float speed = 0;
    private Animator anim;
    private Character character;
    private bool isDone;
    private Rigidbody2D rb;
    private bool isDead;
    private void Awake()
    {
        character = GetComponent<Character>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        OnDie();
    }
    private void FixedUpdate()
    {
        if (!isDead)
        Move();
    }
    public void TriggerAction()
    {
        if(!isDone)
        {
            Go();
        }
    }
    private void Go()
    {
        isDone = true;
        speed = 310;
        //rb.velocity = new Vector2(10, 0);
        anim.SetBool("walk", true);
        this.gameObject.tag = "Untagged";
    }
    private void Move()
    {
        rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
    }



    private void OnDie()
    {
        if(character.currentHealth <= 0)
        {
            isDead = true;  
            anim.SetBool("walk", false);
            anim.SetBool("dead", true);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    private void DsitroyAfterAnim()
    {
        Destroy(gameObject);
    }

}
