using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    public float timeToDestry;
    private void Start()
    {
        Destroy(gameObject, timeToDestry);
    }
}
