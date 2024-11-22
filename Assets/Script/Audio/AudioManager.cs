using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
   [Header("事件监听")] 
   public PlayAudioEventSO FxEvent;
   public PlayAudioEventSO BGMEvent;
   public FloatEventSO volumeEvent;
   public VoidEventSO pauseEvent;
   [Header("广播")]
   public FloatEventSO syncVolumeEvent;
   
   [Header("组件")]
   public AudioSource FXSource;
   public AudioSource BGMSource;
   public AudioMixer audioMixer;

   private void OnEnable()
   {
      FxEvent.OnEventRaised += OnFXEvent;
      BGMEvent.OnEventRaised += OnBGMEvent;
      volumeEvent.OnEventRaised += OnVolumeEvent;
      pauseEvent.OnEventRaised += OnPauseEvent;

   }

  

   private void OnDisable()
   {
      FxEvent.OnEventRaised -= OnFXEvent;
      BGMEvent.OnEventRaised -= OnBGMEvent;
      volumeEvent.OnEventRaised -= OnVolumeEvent;
      pauseEvent.OnEventRaised -= OnPauseEvent;

   }
   private void OnPauseEvent()
   {
      float amount;
      audioMixer.GetFloat("MasterVolume",out amount);
      syncVolumeEvent.RaiseEvent(amount);
   }
   private void OnVolumeEvent(float amount)
   {
      audioMixer.SetFloat("MasterVolume", amount * 100 - 80);
   }

   private void OnBGMEvent(AudioClip clip)
   {
      BGMSource.clip = clip;
      BGMSource.Play();
   }

   private void OnFXEvent(AudioClip clip)
   {
      FXSource.clip = clip;
      FXSource.Play();
   }
}
