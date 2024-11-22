using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public static Sword instance;
    public GameObject swordSign;
    private void Awake()
    {
        instance = this;
    }
    
}
