using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWall : MonoBehaviour
{
    public GameObject wall;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            wall.SetActive(true);
        }
    }
}
