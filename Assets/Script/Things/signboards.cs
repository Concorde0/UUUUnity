using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signboards : MonoBehaviour, IInteractable
{
    public GameObject TalkObject;
    public void TriggerAction()
    {
        TalkObject.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(TalkObject != null && TalkObject ==true)
        {
            TalkObject.SetActive(false);
        } 
        
    }
}
