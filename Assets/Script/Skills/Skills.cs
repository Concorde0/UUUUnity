using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    public void CallMethodOnActiveChild()
    {
        Transform[] children = transform.GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
           
            if (child.gameObject.activeSelf)
            {
                
                InvokeMethod(child.gameObject);
            }
        }
    }

    private void InvokeMethod(GameObject activeChild)
    {
        LightSkillStart method = activeChild.GetComponent<LightSkillStart>();
        if (method != null)
        {
            method.YourMethod();
        }
    }
}


public class LightSkillStart : MonoBehaviour
{
    public void YourMethod()
    {
        Debug.Log("Method called on active child!");
    }
    
    
}