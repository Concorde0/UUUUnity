using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCKnight : MonoBehaviour, IInteractable
{
    public float speed = 0;
    private Animator anim;
    private Character character;
    private bool isDone;
    private Rigidbody2D rb;
    private bool isDead;
    public GameObject TalkObject1;
    public GameObject TalkObject2;
    private void Awake()
    {
        character = GetComponent<Character>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        OnDie();
    }
    private void FixedUpdate()
    {
        if (!isDead)
            Move();
    }
    public void TriggerAction()
    {
        TalkObject1.SetActive(true);

        StartCoroutine(Talk());
    }
    private IEnumerator Talk()
    {
        yield return new WaitForSeconds(3f);
        TalkObject1.SetActive(false);
        TalkObject2.SetActive(true);
        if (!isDone)
        {
            Go();
        }
        yield return new WaitForSeconds(2.5f);
        TalkObject2 .SetActive(false);
    }
    private void Go()
    {
        isDone = true;
        speed = 310;
        //rb.velocity = new Vector2(10, 0);
        anim.SetBool("walk", true);
        this.gameObject.tag = "Untagged";
    }
    private void Move()
    {
        rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
    }



    private void OnDie()
    {
        if(character.currentHealth <= 0)
        {
            isDead = true;  
            anim.SetBool("walk", false);
            anim.SetBool("dead", true);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    private void DsitroyAfterAnim()
    {
        Destroy(gameObject);
    }

}
