using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterSkillCheckEnemy : MonoBehaviour
{
    public GameObject waterSkill;
    public GameObject player;
    public float detectionRange ;
    public LayerMask enemyLayer;
    public Vector3 fix = new(0,1, 0);
    private Animator anim;
    private RaycastHit2D hit;
    public Vector2 direction;
    public bool isDirection;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        
    }

    private void OnDisable()
    {
    }

    void Update()
    {
        CheckEnemy();
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
        
    }

}
