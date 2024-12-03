using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    public SceneLoadEventSO loadEventSO;
    public GameSceneSO sceneToGo;
    public Vector3 positionToGo;
    public bool canTrigger;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (canTrigger != true) 
        {
            
            loadEventSO.RaiseLoadRequestEvent(sceneToGo, positionToGo, true);
            canTrigger = true;
        }
        
    }
}
