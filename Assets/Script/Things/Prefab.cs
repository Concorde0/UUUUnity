using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefab : MonoBehaviour
{
    public GameObject arrowPrefab; 
    public float spawnInterval = 1f; 
    public BeeHitState beeHitState;

    void Start()
    {

    }
    private void Update()
    {

        if (beeHitState.arrow == true)
        {
            //0fÐèÒª¸Ä
            InvokeRepeating("SpawnArrow", 0f, spawnInterval); 
        }
    }

    void SpawnArrow()
    {
            Instantiate(arrowPrefab, transform.position, Quaternion.identity); 
        
    }
}
