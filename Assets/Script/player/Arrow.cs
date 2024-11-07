using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public float destoryDistance;

    private Rigidbody2D rb;
    private Vector3 startPos;
    private GameObject player;

    private void Awake()
    {
         player = GameObject.FindGameObjectWithTag("Player");

    }
    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        int faceDir = (int)player.transform.localScale.x;
        if (faceDir < 0)
        {
            rb.velocity = Vector2.right * speed;
            transform.localScale = new Vector3(1, 1, 1);
        }
        if(faceDir > 0)
        {
            rb.velocity = Vector2.left * speed;
            transform.localScale = new Vector3(-1,1,1);
        }
        
        startPos = transform.position;  
    }
    private void Update()
    {
        float distance = (transform.position - startPos).sqrMagnitude;
        if (distance > destoryDistance)
        {
            Destroy(gameObject);
        }
    }
}
