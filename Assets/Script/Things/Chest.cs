using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Chest : MonoBehaviour, IInteractable
{
    private SpriteRenderer spriteRenderer;
    public Sprite openSprite;
    public Sprite closeSprite;
    public bool isDone;
    public GameObject Coin;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }
    private void OnEnable()
    {
        spriteRenderer.sprite = isDone? openSprite : closeSprite;
    }
    public void TriggerAction()
    {
        /*Debug.Log("1");*/
        if(!isDone)
        {
            OpenCheast();
        }
    }

    private void OpenCheast()
    {
        spriteRenderer.sprite = openSprite;
        isDone = true;
        this.gameObject.tag = "Untagged";
        Instantiate(Coin,transform.position,Quaternion.identity);
    }
}
