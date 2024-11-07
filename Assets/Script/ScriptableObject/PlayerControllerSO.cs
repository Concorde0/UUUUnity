using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Event/PlayerControllerSO")]
public class PlayerControllerSO : ScriptableObject
{
    public UnityAction<PlayerController> OnEventRaised;

    public void RaiseEvent(PlayerController playerController)
    {
        OnEventRaised?.Invoke(playerController);

    }
}
