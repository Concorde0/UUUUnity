using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public static Axe instance;
    public GameObject axeSign;
    private void Awake()
    {
        instance = this;
    }
}
