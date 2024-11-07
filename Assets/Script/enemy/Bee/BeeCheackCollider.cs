//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
//using UnityEngine;

//public class BeeCheackCollider : MonoBehaviour
//{
//    public Animator anim;
//    public GameObject player;
//    public FlyEnemy currentEnemy;
//    private Vector2 originalEnemyPosition;
//    private void Awake()
//    {
//        originalEnemyPosition = transform.position;
//    }
//    private void Update()
//    {
//        if (currentEnemy.flyFoundPlayer)
//        {
//            CheckPlayer();
//        }
//    }

//    private void CheckPlayer()
//    {
//        if (currentEnemy.flyFoundPlayer)
//        {
//            Vector2 enemyPosition = transform.position;
//            Vector2 playerPosition = player.transform.position;
//            Vector2 direction = (playerPosition - enemyPosition).normalized;
//            if (direction.x <= 0)
//            {
//                transform.localScale = new Vector3(-1, 1, 1);
//            }
//            else
//            {
//                transform.localScale = new Vector3(1, 1, 1);
//                Vector2.MoveTowards(enemyPosition, enemyPosition + direction, currentEnemy.normalSpeed * Time.deltaTime);
//            }
            
//        }
//        else
//        {
//            Vector2 currentPosition = transform.position;
//            Vector2 direction = (currentPosition - originalEnemyPosition).normalized;
//            Vector2.MoveTowards(currentPosition, originalEnemyPosition + direction, currentEnemy.normalSpeed * Time.deltaTime);
//        }
//    }
//}
