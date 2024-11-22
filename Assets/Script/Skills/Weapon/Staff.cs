using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
    public static Staff instance;
    public GameObject staffSign;
    private void Awake()
    {
        instance = this;
    }
}
