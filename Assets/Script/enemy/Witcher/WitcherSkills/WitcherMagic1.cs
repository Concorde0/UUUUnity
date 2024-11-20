using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitcherMagic1 : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject witcher;
    private Vector3 startPos;
    public float speed;
    public float destroyDistance;
    // public Transform witcherTrans;

    private void Awake()
    {
        witcher = GameObject.FindGameObjectWithTag("Witcher");
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        transform.position = witcher.transform.position;
    }

    private void Start()
    {

        int faceDir = (int)witcher.transform.localScale.x;
        if (faceDir > 0)
        {
            rb.velocity = Vector2.right * speed;
            transform.localScale = new Vector3(1, 1, 1);
        }
        if(faceDir < 0)
        {
            rb.velocity = Vector2.left * speed;
            transform.localScale = new Vector3(-1,1,1);
        }
        
        startPos = transform.position;  
    }
    private void Update()
    {
        // transform.localScale = witcherTrans.localScale * 0.5f;
        float distance = (transform.position - startPos).sqrMagnitude;
        if (distance > destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
