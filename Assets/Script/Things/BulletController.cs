//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//public class BulletController : MonoBehaviour
//{
//    /// <summary>
//    /// ȱ��distroy
//    /// </summary>
//    public float speed = 5f; 
//    private Rigidbody2D rb;
//    private Vector2 playerPosition; 

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position; 
//    }

//    void Update()
//    {
//        // �����ͷ����
//        Vector2 direction = (playerPosition - rb.position).normalized;
        
//        rb.velocity = direction * speed;
//    }
//}
