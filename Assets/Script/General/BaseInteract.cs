using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteract : MonoBehaviour
{
    public bool canInteract;
    private PlayerController playerController;
   
    private void Awake()
    {
        canInteract = false; 
        playerController = GetComponent<PlayerController>();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.CompareTag("Player"))
        {
            Debug.Log("aa");
            canInteract = true;

           
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if ( other.CompareTag("Player"))
        {
            Debug.Log("bb");
            canInteract = false;
        }
    }
    
}
