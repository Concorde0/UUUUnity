 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SceneFlash : MonoBehaviour
{
    public GameObject flashImage;
    public float flashDuration;

    private void Start()
    {
        
    }

    public void FlashScece()
    {
        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        flashImage.SetActive(true);
        yield return new WaitForSeconds(flashDuration);
        flashImage.SetActive(false);
    }
}
