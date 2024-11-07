using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowHit : MonoBehaviour
{
    public Animator anim;
    public GameObject arrowPrefab;
    public PlayerController playerController;
    private PlayerAnimation playerAnimation;
    public PlayerInputControl inputControl;
    
    private void Awake()
    {
        //controller = GetComponent<PlayerController>();
        playerController = GetComponent<PlayerController>();
        playerAnimation = GetComponent<PlayerAnimation>();
        inputControl = new PlayerInputControl();
        //inputControl.GamePlay.Shoot.started += Shoot;
    }
    private void OnEnable()
    {
        inputControl.GamePlay.Enable(); 
    }
    private void OnDisable()
    {
        inputControl.GamePlay.Disable();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    private void Shoot(InputAction.CallbackContext obj)
    {
        playerAnimation.PlayArrow();
        playerController.isArrow = true;
        Vector3 arorwPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        Instantiate(arrowPrefab, arorwPosition, transform.rotation);
    }
    
}
