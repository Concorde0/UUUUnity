using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grass : MonoBehaviour
{
    private Tilemap sr;
    private void Awake()
    {
        sr = GetComponent<Tilemap>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == ("Player"))
        {
            sr.color 
                = new (sr.color.r,sr.color.g,sr.color.b,0.6f);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            sr.color = new(sr.color.r, sr.color.g, sr.color.b, 1f);
        }
    }
}
