using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterSkillCheckEnemy : MonoBehaviour
{
    public static WaterSkillCheckEnemy instance;
    public GameObject waterSkill;
    public GameObject player;
    public float detectionRange ;
    public LayerMask enemyLayer;
    public Vector3 fix = new(0,1, 0);
    private Animator anim;
    private RaycastHit2D hit;
    public Vector2 direction;
    public bool isDirection;
    public bool isWater = true;
    public Transform playerTrans;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        CheckEnemy();
        transform.localScale = playerTrans.localScale*1.8f;
        transform.position = direction;
    }

    private void CheckEnemy()
    {
        // 发射射线
        hit = Physics2D.Raycast(player.transform.position+fix, Vector2.left*-player.transform.localScale, detectionRange, enemyLayer);

        if (hit.collider != null)
        {
            if (!isDirection)
            {
                direction = hit.point;
                isDirection = true;
                isWater = false;
            }
            anim.SetBool("water", true);
        }
        else
        {
            if (!isDirection)
            {
                waterSkill.SetActive(false);
            }
             
        }
        
        Debug.DrawRay(player.transform.position+fix, -Vector2.left*detectionRange*player.transform.localScale,Color.red);
    }
    public void WaterSkillEnd()
    {
        waterSkill.SetActive(false);
        WaterSkill.instance.isWaterSkill = false;
        isDirection = false;
        isWater = true;

    }

}
