using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSkillOwn : MonoBehaviour
{
   private Rigidbody2D rb;
   private GameObject player;
   private Vector3 startPos;
   public float speed;
   public float destoryDistance;
   public Transform playerTrans;

   private void Awake()
   {
      rb = GetComponent<Rigidbody2D>();
      player = GameObject.FindGameObjectWithTag("Player");
   }

   private void OnEnable()
   {
      transform.position = player.transform.position;
      
   }

   private void Start()
   {

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
      // transform.localScale = playerTrans.localScale * 0.5f;
      float distance = (transform.position - startPos).sqrMagnitude;
      if (distance > destoryDistance)
      {
         gameObject.SetActive(false);
      }
   }
}
