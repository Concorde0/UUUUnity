using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Event/RigidBodySO")]
public class RigidBodySO : ScriptableObject
{
    public UnityAction<Rigidbody2D> OnEventRaised;

    public void RaiseEvent(Rigidbody2D rb)
    {
        OnEventRaised?.Invoke(rb);
    }
}
