using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerAnimation : MonoBehaviour
{
    
    private Animator anim;
    private Rigidbody2D rb;
    private PhysiscCheck physiscCheck;
    private PlayerController playerController;
    private Character character;
    private void Awake()
    {
        character = GetComponent<Character>();  
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physiscCheck = GetComponent<PhysiscCheck>();
        playerController = GetComponent<PlayerController>();
        
    }
    private void Update()
    {
        SetAnimation();
    }

    public void SetAnimation()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isGround", physiscCheck.isGround);
        anim.SetBool("isDead", playerController.isDead);
        anim.SetBool("isAttack",playerController.isAttack);
        anim.SetBool("isArrow",playerController.isArrow);
        anim.SetBool("isMagic",playerController.isMagic);
        anim.SetBool("isDrink",playerController.isDrink);
        anim.SetBool("isMagic2",playerController.isMagic2);
    }


    public void PlayHurt()
    {
        anim.SetTrigger("hurt");
    }

    public void PlayLightAttack()
    {
        anim.SetTrigger("lightAttack");
    }
    public void PlayHeavyAttack()
    {

        anim.SetTrigger("heavyAttack");
        
    }

    public void PlayArrow()
    {
        anim.SetTrigger("arrow");
    }

    public void PlayMagic()
    {
        anim.SetTrigger("magic");
    }

    public void PlayDrink()
    {
        anim.SetTrigger("drink");
    }
    public void PlayMagicRebecaa()
    {
        anim.SetTrigger("magic2");
    }
}
