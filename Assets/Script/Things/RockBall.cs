using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBall : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private bool isRock;
    public float destroyTime;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isRock)
        {
            destroyTime -= Time.deltaTime;
        }

        if (destroyTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isRock = true;
            anim.SetBool("isrock", true);
        }
    }

    public void RockFall()
    {
        rb.gravityScale = 2;
    }
    
}
