using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
   [Header("事件监听")] 
   public PlayAudioEventSO FxEvent;
   public PlayAudioEventSO hitEvent;
   public PlayAudioEventSO BGMEvent;
   public PlayAudioEventSO StepEvent;
   public FloatEventSO volumeEvent;
   public VoidEventSO pauseEvent;
   [Header("广播")]
   public FloatEventSO syncVolumeEvent;
   
   [Header("组件")]
   public AudioSource FXSource;
   public AudioSource HitSource;
   public AudioSource BGMSource;
   public AudioSource StepSource;
   public AudioMixer audioMixer;
   [Header("bool")] 
   public bool isStep;
   public float isStepTime = 0.5f;
   public float isStepCounter;

   private void OnEnable()
   {
      FxEvent.OnEventRaised += OnFXEvent;
      hitEvent.OnEventRaised += OnHitEvent;
      BGMEvent.OnEventRaised += OnBGMEvent;
      StepEvent.OnEventRaised += OnStepEvent;
      volumeEvent.OnEventRaised += OnVolumeEvent;
      pauseEvent.OnEventRaised += OnPauseEvent;

   }

  

   private void OnDisable()
   {
      FxEvent.OnEventRaised -= OnFXEvent;
      hitEvent.OnEventRaised -= OnHitEvent;
      BGMEvent.OnEventRaised -= OnBGMEvent;
      StepEvent.OnEventRaised -= OnStepEvent;
      volumeEvent.OnEventRaised -= OnVolumeEvent;
      pauseEvent.OnEventRaised -= OnPauseEvent;

   }

   private void Start()
   {
      isStepCounter = isStepTime;
   }

   private void Update()
   {
      if (isStep && isStepCounter >0)
      {
         isStepCounter -= Time.deltaTime;
      }

      if (isStepCounter <= 0)
      {
         isStep = false;
      }
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
   private void OnHitEvent(AudioClip clip)
   {
      HitSource.clip = clip;
      HitSource.Play();
   }

   private void OnStepEvent(AudioClip clip)
   {
      if (!isStep)
      {
         StepSource.clip = clip;
         StepSource.Play();
         isStep = true;
         isStepCounter = isStepTime;
      }
      
   }
}
