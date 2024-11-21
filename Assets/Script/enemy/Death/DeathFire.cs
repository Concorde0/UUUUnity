using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFire : MonoBehaviour
{
    public new GameObject gameObject;
    private void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
