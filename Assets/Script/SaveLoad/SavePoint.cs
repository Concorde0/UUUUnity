using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour,IInteractable
{
    [Header("事件监听")]
    public VoidEventSO saveGameEvent;
    [Header("other")]
    private Animator anim;

    public GameObject fireLight;
    public GameObject baseLight;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        
    }

    public void TriggerAction()
    {
        fireLight.SetActive(true);
        baseLight.SetActive(true);
        anim.SetBool("isLoad", true);
        saveGameEvent.RaiseEvent();
        this.gameObject.tag = "Untagged";
    }
}
