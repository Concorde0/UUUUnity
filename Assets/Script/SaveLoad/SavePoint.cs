using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour,IInteractable
{
    [Header("�㲥")]
    public VoidEventSO saveGameEvent;
    [Header("����")]
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
