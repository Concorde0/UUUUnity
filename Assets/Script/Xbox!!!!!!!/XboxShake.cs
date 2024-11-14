using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class XboxShake : MonoBehaviour
{

    [SerializeField] private float low1 =0.5f;
    [SerializeField] private float low2 =0.8f;
    [SerializeField] private float low3 = 1f;
    [SerializeField] private float high1= 0.5f;
    [SerializeField] private float high2= 0.8f;
    [SerializeField] private float high3 = 1f;
    [SerializeField] private float lightAttackTime=0.2f;
    [SerializeField] private float heavyAttackTime=0.3f;  
    [SerializeField] private float hHeavyAttackTime = 0.4f;
    [SerializeField] private float bridgeBrokeTime = 0.7f;
    public void GamepadVibrate(float low, float high, float time) => StartCoroutine(IEGamepadVibrate(low, high, time));

    public IEnumerator IEGamepadVibrate(float low, float high, float time)
    {
        
        if (Gamepad.current == null)
            yield break;
        
        Gamepad.current.SetMotorSpeeds(low, high);
        Gamepad.current.ResumeHaptics();
        var endTime = Time.time + time;

        while (Time.time < endTime)
        {
            Gamepad.current.ResumeHaptics();
            yield return null;
        }

        if (Gamepad.current == null)
            yield break;

        Gamepad.current.PauseHaptics();
    }

    public void HeavyAttackShake()
    {
        GamepadVibrate(low2, high2, heavyAttackTime);
    }
    public void HheavyAttackShake()
    {
        GamepadVibrate(low3, high3, hHeavyAttackTime);
    }
    public void LightAttackShake()
    {
        GamepadVibrate(low1, high1, lightAttackTime);
    }
    public void BridgeBroke()
    {
        GamepadVibrate(low1, high1, bridgeBrokeTime);
    }
}
