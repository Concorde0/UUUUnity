using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkButton : MonoBehaviour, IInteractable
{
    public GameObject talkUI;

    public void TriggerAction()
    {
        
    }
    
    private void PlayText()
    {
        talkUI.SetActive(true);
    }
}
