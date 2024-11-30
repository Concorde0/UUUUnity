using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorDestroyed : MonoBehaviour
{
    public void OnDestroy()
    {
        Destroy(this.gameObject);
    }
}
