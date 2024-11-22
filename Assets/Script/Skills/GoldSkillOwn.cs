using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSkillOwn : MonoBehaviour
{
   private Rigidbody2D rb;
   private GameObject player;
   private GameObject goldSkill;
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
      Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y + 2.5f, player.transform.position.z);
      transform.position = pos;
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

   private void OnDisable()
   {
      
   }

   private void Start()
   {
        
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
