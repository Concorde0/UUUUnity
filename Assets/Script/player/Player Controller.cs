using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class PlayerController : MonoBehaviour
{
    [Header("监听事件")]
    public SceneLoadEventSO sceneLoadEvent;
    public VoidEventSO afterSceneLoadedEvent;
    public VoidEventSO loadDataEvnet;
    public VoidEventSO backToMenuEvent;

    public PlayerInputControl inputControl;
    public Rigidbody2D rb;
    private PhysiscCheck physiscCheck;
    private PlayerAnimation playerAnimation;
    public Vector2 inputDirection;
    [Header("基本参数")]
    public float currentSpeed;
    public float speed;
    public float runSpeed;
    public float jumpForce;
    public float hurtForce;
    public CapsuleCollider2D coll;
    [Header("物理材质")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    [Header("状态")]
    public bool isHurt;
    public bool isDead;
    public bool isAttack;
    public bool isInteract;
    public bool isArrow;
    public bool isRun;
    //public bool isPower;

    [Header("prefab")]
    public GameObject arrowPrefab;
    [Header("计时器")]
    //public float shootTime;
    //public float shootTimeCounter;
    public float shootWaitTime;
    public float shootWaitTimeCounter = 0;
    public float PowerWaitTime;
    public float PowerWaitTimeCounter = 0;
    //tmp
    public float tmpwaitTime;
    public float tmpwaitTimeConter;
    [Header("事件")]
    public UnityEvent<PlayerController> playerControllerEvent;
    public UnityEvent<Character> Attack;
    private Character character;
    

    
    private void Awake()
    {
        //tmp
        tmpwaitTimeConter = tmpwaitTime;
        
        currentSpeed = speed;
        character = GetComponent<Character>();
        rb = GetComponent<Rigidbody2D>();
        physiscCheck = GetComponent<PhysiscCheck>();
        inputControl = new PlayerInputControl();
        playerAnimation = GetComponent<PlayerAnimation>();
        coll = GetComponent<CapsuleCollider2D>();
        //jump
        inputControl.GamePlay.Jump.started += Jump;

        //Attack light
        inputControl.GamePlay.LightAttack.started += PlayerLightAttack;

        //Attack Heavy
        inputControl.GamePlay.HeavyAttack.started += PlayerHeavyAttack;
        //shoot
        inputControl.GamePlay.Shoot.started += Shoot;

        //interact
        inputControl.Enable();
        //Run
        inputControl.GamePlay.Run.performed += Run;
        //StopRunning
        inputControl.GamePlay.Run.canceled += StopRunning;

    }

    

    private void OnEnable()
    {
        playerControllerEvent?.Invoke(this);
        sceneLoadEvent.LoadRequestEvent += OnLoadEvent;
        afterSceneLoadedEvent.OnEventRaised += OnafterSceneLoadedEvent;
        loadDataEvnet.OnEventRaised += OnLoadDataEvnet;
        backToMenuEvent.OnEventRaised += OnLoadDataEvnet;

        
    }

    private void OnDisable()
    {
        inputControl.Disable();
        sceneLoadEvent.LoadRequestEvent -= OnLoadEvent;
        afterSceneLoadedEvent.OnEventRaised -= OnafterSceneLoadedEvent;
        loadDataEvnet.OnEventRaised -= OnLoadDataEvnet;
        backToMenuEvent.OnEventRaised -= OnLoadDataEvnet;


    }

   

    private void Update() 
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();

        CheckState();
        if (shootWaitTimeCounter >=-0.1)
        {
            shootWaitTimeCounter -= Time.deltaTime;
        }

        if (PowerWaitTimeCounter > 0)
        {
            PowerWaitTimeCounter -= Time.deltaTime;
        }
        if (PowerWaitTimeCounter < 0 && character.currentPower < 200 && isRun == false)
        {
            character.currentPower += 25*Time.deltaTime;
            Attack?.Invoke(character);
        }
        if (isRun && rb.velocity.x!= 0 && character.currentPower >0)
        {
            currentSpeed = runSpeed;
            (PowerWaitTimeCounter) = PowerWaitTime;
            character.currentPower -= 13*Time.deltaTime;
            Attack?.Invoke(character);
        }
        else if (character.currentPower<0)
        {
            isRun = false;
            currentSpeed =speed;
        }
        ///NNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN///
        if(rb.velocity.x == 0)
        {
            tmpwaitTimeConter -= Time.deltaTime;
        }
        if(rb.velocity.x == 0 && tmpwaitTimeConter < 0)
        {
            isHurt = false;
            tmpwaitTimeConter = tmpwaitTime;
        }
        
       
    }
    private void FixedUpdate()
    {
        if(!isHurt && !isAttack && !isArrow)
        {
            Move();
        }
    }
    private void OnLoadEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        inputControl.GamePlay.Disable();    
    }
    private void OnLoadDataEvnet()
    {
        isDead = false;
        isHurt = false;
        

    }
    
    private void OnafterSceneLoadedEvent()
    {
        inputControl.GamePlay.Enable();
    }

    private void Shoot(InputAction.CallbackContext obj)
    {
        
        if (shootWaitTimeCounter <= 0)
        {
            playerAnimation.PlayArrow();
            if (isArrow)
            {
                return;
            }
            isArrow = true;
            Vector3 arorwPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Instantiate(arrowPrefab, arorwPosition, transform.rotation);
            shootWaitTimeCounter = shootWaitTime;
        }
        
        
    }


    public void Move()
    {
        
        rb.velocity = new Vector2(inputDirection.x * currentSpeed * Time.deltaTime,rb.velocity.y);

        int faceDir = (int)transform.localScale.x;
            
        if (inputDirection.x > 0)
            faceDir = -2;
        if (inputDirection.x < 0)
            faceDir = 2;

        //人物翻转
        transform.localScale = new Vector3(faceDir, 2, 2);
    }

    private void Run(InputAction.CallbackContext obj)
    {

        isRun = true;
        //character.currentPower -= 10*Time.deltaTime;
    }
    private void StopRunning(InputAction.CallbackContext context)
    {

        currentSpeed = speed;
        isRun = false;
    }


    private void Jump(InputAction.CallbackContext obj)
    {
        if(physiscCheck.isGround)
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

   
    private void PlayerLightAttack(InputAction.CallbackContext obj)
    {
        if(character.currentPower > 20)
        {
            playerAnimation.PlayLightAttack();
            isAttack = true;
        }

        //character.currentPower -= 5;
        Attack?.Invoke(character);

    }
    private void PlayerHeavyAttack(InputAction.CallbackContext obj)
    {
        if(character.currentPower > 30)
        {
            playerAnimation.PlayHeavyAttack();
            isAttack = true;
        }

        //character.currentPower -= 10;
        Attack?.Invoke(character);
    }

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        
    }
   

    public void PlayerDead()
    {
        isDead = true;
        inputControl.GamePlay.Disable();
        
    }

    private void CheckState()
    {
        coll.sharedMaterial = physiscCheck.isGround ? normal : wall;
    }
    private void LightAttackPower()
    {
        PowerWaitTimeCounter = PowerWaitTime;
        character.currentPower -= 20;
        Attack?.Invoke(character);
    }
    
    private void HeavyAttackPower()
    {
        PowerWaitTimeCounter = PowerWaitTime;
        character.currentPower -= 30;
        Attack?.Invoke(character);
        
    }
    private void AttackConter()
    {
        PowerWaitTimeCounter = PowerWaitTime;
    }


}
