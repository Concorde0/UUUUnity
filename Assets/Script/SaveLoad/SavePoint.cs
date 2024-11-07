using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour,IInteractable
{
    [Header("¹ã²¥")]
    public VoidEventSO saveGameEvent;
    [Header("Êý¾Ý")]
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        
    }

    public void TriggerAction()
    {
        anim.SetBool("isLoad", true);
        saveGameEvent.RaiseEvent();
        this.gameObject.tag = "Untagged";
    }
}
