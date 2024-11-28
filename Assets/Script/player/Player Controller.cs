using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using Debug = UnityEngine.Debug;

public class PlayerController : MonoBehaviour
{
    [Header("事件监听")]
    public SceneLoadEventSO sceneLoadEvent;
    public VoidEventSO afterSceneLoadedEvent;
    public VoidEventSO loadDataEvnet;
    public VoidEventSO backToMenuEvent;

    public PlayerInputControl inputControl;
    public Rigidbody2D rb;
    private PhysiscCheck physiscCheck;
    private PlayerAnimation playerAnimation;
    public Vector2 inputDirection;
    [Header("基础属性")]
    public float currentSpeed;
    public float speed;
    public float runSpeed;
    public float jumpForce;
    public float hurtForce;
    public CapsuleCollider2D coll;
    [Header("物理材质")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;
    public GameObject BloodEffect;

    [Header("״bools")]
    public bool isHurt;
    public bool isDead;
    public bool isAttack;
    public bool isArrow;
    public bool isRun;
    public bool isMagic;
    public bool isDrink;
    public bool isMagic2;

    [Header("prefab")]
    public GameObject arrowPrefab;
    public GameObject pauseMenu;
    public AudioClip stepGrass1;
    public AudioClip stepGrass2;
    [Header("Counter")]
    public float shootWaitTime;
    public float shootWaitTimeCounter = 0;
    public float PowerWaitTime;
    public float PowerWaitTimeCounter = 0;
    //tmp
    // public float tmpwaitTime;
    // public float tmpwaitTimeConter;
    [Header("Unity Event")]
    public UnityEvent<PlayerController> playerControllerEvent;
    public UnityEvent<Character> Attack;
    public UnityEvent<Character> Magic;
    private Character character;
    public PlayAudioEventSO playAudioEvent;

    
    
    
    


    private void Awake()
    {
       
        // tmpwaitTimeConter = tmpwaitTime;
        currentSpeed = speed;
        character = GetComponent<Character>();
        rb = GetComponent<Rigidbody2D>();
        physiscCheck = GetComponent<PhysiscCheck>();
        inputControl = new PlayerInputControl();
        playerAnimation = GetComponent<PlayerAnimation>();
        coll = GetComponent<CapsuleCollider2D>();
        //jump
        inputControl.GamePlay.Jump.started += Jump;
        //interact
        inputControl.Enable();

        //Attack
        inputControl.GamePlay.LightAttack.started += PlayerLightAttack;

        //shoot
        inputControl.GamePlay.Shoot.started += Shoot;
        //Spical
        inputControl.GamePlay.Spcial.started += Spcial;
        //Skill
        inputControl.GamePlay.Skill.started += skill;
        //bottle
        inputControl.GamePlay.bottle.started += bottle;
        //Run
        inputControl.GamePlay.Run.performed += Run;
        //StopRunning
        inputControl.GamePlay.Run.canceled += StopRunning;
        //XBox UP
        inputControl.GamePlay.UpXbox.started += UpXbox;
        //Xbox Down
        inputControl.GamePlay.DownXbox.started += DownXbox;
        //Xbox Right
        inputControl.GamePlay.RightXbox.started += RightXbox;
        //Xbox Left
        inputControl.GamePlay.LeftXbox.started += LeftXbox;
        //Down
        inputControl.GamePlay.Down.started += Down;
        //pause
        inputControl.GamePlay.Pause.started += Pasue;
        
        //
        

    }

    


    private void Start()
    {
        GameManager.Instance.RegisterPlayer(character); 
        
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
        ShootCoolDown();
        PowerConsume();
        // HurtFix();
        if (isDead)
        {
            GameManager.Instance.NotifyObservers();
        }
        //TODO:




    }
    private void FixedUpdate()
    {
        if (!isHurt && !isAttack && !isArrow)
        {
            Move();
        }

        
    }

    private void PowerConsume()
    {
        //Run
        if (isRun && rb.velocity.x != 0 && character.currentPower > 0)
        {
            currentSpeed = runSpeed;
            (PowerWaitTimeCounter) = PowerWaitTime;
            character.currentPower -= 25 * Time.deltaTime;
            ShadowPool.instance.GetFromPool();
            Attack?.Invoke(character);
        }
        else if (character.currentPower < 0)
        {
            isRun = false;
            currentSpeed = speed;
        }
        
        
        if (PowerWaitTimeCounter > 0)
        {
            PowerWaitTimeCounter -= Time.deltaTime;
        }
        if (PowerWaitTimeCounter < 0 && character.currentPower < 200 && isRun == false)
        {
            character.currentPower += 50 * Time.deltaTime;
            Attack?.Invoke(character);
        }
    }

    private void ShootCoolDown()
    {
        if (shootWaitTimeCounter >= -0.1)
        {
            shootWaitTimeCounter -= Time.deltaTime;
        }
    }

    // private void HurtFix()
    // {
    //     if (rb.velocity.x == 0)
    //     {
    //         tmpwaitTimeConter -= Time.deltaTime;
    //     }
    //     if (rb.velocity.x == 0 && tmpwaitTimeConter < 0)
    //     {
    //         isHurt = false;
    //         tmpwaitTimeConter = tmpwaitTime;
    //     }
    // }
    
    
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

        rb.velocity = new Vector2(inputDirection.x * currentSpeed * Time.deltaTime, rb.velocity.y);

        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
            faceDir = -2;
        if (inputDirection.x < 0)
            faceDir = 2;
        
        transform.localScale = new Vector3(faceDir, 2, 2);
    }

    private void Run(InputAction.CallbackContext obj)
    {
        isRun = true;
    }
    
    private void StopRunning(InputAction.CallbackContext obj)
    {
        currentSpeed = speed;
        isRun = false;
    }


    private void Jump(InputAction.CallbackContext obj)
    {
        if (physiscCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
           
    }
    private void Pasue(InputAction.CallbackContext obj)
    {
        if (pauseMenu.activeSelf == false)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    private void Down(InputAction.CallbackContext obj)
    {
        if (physiscCheck.isOneWayPlatform)
        {
            gameObject.layer = LayerMask.NameToLayer("OneWayGround");
            StartCoroutine(RestoreLayer());
        }
    }

    private IEnumerator RestoreLayer()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    
    private void PlayerLightAttack(InputAction.CallbackContext obj)
    {
        if (Sword.instance.swordSign.activeSelf)
        {
            if (character.currentPower > 20)
            {
                playerAnimation.PlayLightAttack();
                isAttack = true;
            }
            Attack?.Invoke(character);
        }

        if (Axe.instance.axeSign.activeSelf)
        {
            if (character.currentPower > 30)
            {
                playerAnimation.PlayHeavyAttack();
                isAttack = true;
            }
            Attack?.Invoke(character);
        }
    }
    private void Spcial(InputAction.CallbackContext obj)
    {
        if (Bow.instance.bowSign.activeSelf)
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
        
        
    }
    private void skill(InputAction.CallbackContext obj) 
    {
        //LightSkill
        if (LightSkill.instance.lightSign.activeSelf && Staff.instance.staffSign.activeSelf)
        {
            if (character.currentMagic >= LightSkill.instance.magicConsumer)
            {
                if (LightSkill.instance.isLight == false)
                {
                    isMagic = true;
                    playerAnimation.PlayMagic();
                }
                LightSkill.instance.LightSkillStart();
                character.currentMagic -= LightSkill.instance.magicConsume;
                Magic?.Invoke(character);
                
            }
        }
        
        //BloodLineSkill
        if (BloodLineSkill.instance.bloodLineSign.activeSelf && Staff.instance.staffSign.activeSelf)
        {
            if (character.currentMagic >= BloodLineSkill.instance.magicConsumer)
            {
                BloodEffect.SetActive(true);
                StartCoroutine(FixBloodLineSkill());
            }
        }
        
        //GoldSkill
        if (GoldSkill.instance.goldSign.activeSelf && Staff.instance.staffSign.activeSelf)
        {
            if (character.currentMagic >= GoldSkill.instance.magicConsumer && character.currentPower >= 55f)
            {
                if (GoldSkill.instance.isGoldSkill == false)
                {
                    inputControl.GamePlay.Disable();
                    playerAnimation.GoldEffect();
                    StartCoroutine(FixGoldEffect());
                }
                
            }
        }
        
        //WaterSkill
        if (WaterSkill.instance.waterSign.activeSelf && Staff.instance.staffSign.activeSelf)
        {
            if (character.currentMagic >= WaterSkill.instance.magicConsumer)
            {
                if (WaterSkill.instance.isWaterSkill == false)
                {
                    isMagic = true;
                    playerAnimation.PlayMagic();
                }
                WaterSkill.instance.WaterSkillStart();
                StartCoroutine(FixWaterSkill());
            
            
            }
        }
    }
    private IEnumerator FixBloodLineSkill()
    {
        yield return new WaitForSeconds(1f);
        playerAnimation.PlayMagicRebecaa();
        isMagic2 = true;
        BloodLineSkill.instance.BloodLineSkillStart();
        character.currentMagic -= BloodLineSkill.instance.magicConsume;
        Magic?.Invoke(character);
        inputControl.GamePlay.Disable();
        BloodEffect.SetActive(false);
        yield return new WaitForSeconds(1.3f);
        inputControl.GamePlay.Enable();
        isMagic2 = false;
    }
    
    private IEnumerator FixWaterSkill()
    {
        yield return new WaitForSeconds(0.05f);
        if (WaterSkillCheckEnemy.instance.isWater == false)
        {
            character.currentMagic -= WaterSkill.instance.magicConsume;
        }
        WaterSkillCheckEnemy.instance.isWater = true;
        Magic?.Invoke(character);
    }

    private IEnumerator FixGoldEffect()
    {
        yield return new WaitForSeconds(1f);
        GoldSkill.instance.GoldSkillStart();
        character.currentMagic -= GoldSkill.instance.magicConsume;
        Magic?.Invoke(character);
        yield return new WaitForSeconds(0.4f);
        inputControl.GamePlay.Enable();
    }
    
    private void bottle(InputAction.CallbackContext obj)
    {
        playerAnimation.PlayDrink();
        HealthBottle.instance.RecoverHealth();
        MagicBottle.instance.RecoverMagic();
        StartCoroutine(FixBottle());
    }

    private IEnumerator FixBottle()
    {
        yield return new WaitForSeconds(0.6f);
        inputControl.GamePlay.Enable();
    }
    private void DownXbox(InputAction.CallbackContext obj)
    {
        SkillsManager.instance.DownChangeSkill();
    }

    private void UpXbox(InputAction.CallbackContext obj)
    {
       SkillsManager.instance.UpChangeSkill();
    }
    private void LeftXbox(InputAction.CallbackContext obj)
    {
        SkillsManager.instance.LeftChangeSkill();
    }

    private void RightXbox(InputAction.CallbackContext obj)
    {
       SkillsManager.instance.RightChangeSkill();
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
        character.currentPower -= 35;
        Attack?.Invoke(character);
    }

    private void HeavyAttackPower()
    {
        PowerWaitTimeCounter = PowerWaitTime + 1f;
        character.currentPower -= 55;
        Attack?.Invoke(character);
        
    }
    private void AttackConter()
    {
        PowerWaitTimeCounter = PowerWaitTime;
    }
    
    private void StepGrass1()
    {
        playAudioEvent.RaiseEvent(stepGrass1);
    }

    private void StepGrass2()
    {
        playAudioEvent.RaiseEvent(stepGrass2);
    }
    

}
