using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public static Bow instance;
    public GameObject bowSign;
    private void Awake()
    {
        instance = this;
    }
}
