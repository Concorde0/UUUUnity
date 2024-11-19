using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMagic : MonoBehaviour
{
    public GameObject magic;
    public void MagicEnd()
    {
        magic.SetActive(false);
    }
}
