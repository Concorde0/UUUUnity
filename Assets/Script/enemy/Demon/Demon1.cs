using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Demon1 : MonoBehaviour
{
    public float attackDistance;
    private Animator anim;
    public Transform Knight;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody2D>();   
    }
    private void Update()
    {
        AttackKnight();
    }
    private void AttackKnight()
    {
        if(Knight!= null)
        {
            if (Vector2.Distance(transform.position, Knight.position ) < attackDistance)
            {
                //Debug.Log("!!");
                anim.SetBool("attack", true);
            }
            else
            {
                anim.SetBool("attack", false);

            }
        }
        else
        {
            anim.SetBool("attack", false);
        }
    }

    
}
