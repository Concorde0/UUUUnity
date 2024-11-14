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
        // 获取所有子对象
        Transform[] children = transform.GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
            // 检查子对象是否激活
            if (child.gameObject.activeSelf)
            {
                // 调用子对象的方法
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

// 假设这是子对象上的一个组件，包含你要调用的方法
public class LightSkillStart : MonoBehaviour
{
    public void YourMethod()
    {
        Debug.Log("Method called on active child!");
    }
    
    
}