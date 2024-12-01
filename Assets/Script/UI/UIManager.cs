using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    [Header("事件监听")]
    public CharacterEventSO healthEvent;
    public CharacterEventSO magicEvent;
    public CharacterEventSO powerEvent;
    public SceneLoadEventSO unloadedSceneEvent;
    public VoidEventSO loadDataEvent;
    public VoidEventSO gameOverEvent;
    public VoidEventSO gameWinEvent;
    public VoidEventSO backToMenuEvent;
    public FloatEventSO syncVolumeEvent;
    [Header("广播")]
    public VoidEventSO pauseEvent;
    [Header("组件")]
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public GameObject restartButton;
    public GameObject pausePanel;
    public Button settingsButton;
    public Slider volumeSlider;
    private void Awake()
    {
        settingsButton.onClick.AddListener(TogglePausePanel);
    }

    private void TogglePausePanel()
    {
        if (pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pauseEvent.RaiseEvent();
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
        powerEvent.OnEventRaised += OnPowerEvent;
        magicEvent.OnEventRaised += OnMagicEvent;
        //healthEvent.OnEventRaised += KnightOnHealEvent;
        unloadedSceneEvent.LoadRequestEvent += OnUnLoadedSceneEvent;
        loadDataEvent.OnEventRaised += OnLoadDataEvnet;
        gameOverEvent.OnEventRaised += OnGameOverEvnet;
        gameWinEvent.OnEventRaised += OnWinEvent;
        backToMenuEvent.OnEventRaised += OnLoadDataEvnet;
        syncVolumeEvent.OnEventRaised += OnsyncVolumeEvent;
    }
    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
        powerEvent.OnEventRaised -= OnPowerEvent;
        magicEvent.OnEventRaised -= OnMagicEvent;
        //healthEvent.OnEventRaised -= KnightOnHealEvent;
        unloadedSceneEvent.LoadRequestEvent -= OnUnLoadedSceneEvent;
        loadDataEvent.OnEventRaised -= OnLoadDataEvnet;
        gameOverEvent.OnEventRaised -= OnGameOverEvnet;
        gameWinEvent.OnEventRaised -= OnWinEvent;
        backToMenuEvent.OnEventRaised -= OnLoadDataEvnet;
        syncVolumeEvent.OnEventRaised -= OnsyncVolumeEvent;
    }

    private void OnsyncVolumeEvent(float amount)
    {
        volumeSlider.value = (amount+80)/100f;
    }


    private void OnGameOverEvnet()
    {
        gameOverPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(restartButton);
    }

    private void OnWinEvent()
    {
        winPanel.SetActive(true);
    }

    private void OnLoadDataEvnet()
    {
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
    }

    private void OnUnLoadedSceneEvent(GameSceneSO scenToLoad, Vector3 arg1, bool arg2)
    {
        var isMenu = scenToLoad.sceneType == SceneType.Menu;
           playerStatBar.gameObject.SetActive(!isMenu);
        
    }

    private void OnHealthEvent(Character character)
    {
        // Debug.Log("health");
        var precentage = character.currentHealth / character.maxHealth;

        playerStatBar.OnHealthChange(precentage);
    }
    private void OnMagicEvent(Character character)
    {
        //Debug.Log("Magic");
        var precentage = character.currentMagic / character.maxMagic;

        playerStatBar.OnMagicChange(precentage);
    }
    private void OnPowerEvent(Character character)
    {
        //Debug.Log("power");
        var precentage = character.currentPower / character.maxPower;
        playerStatBar.OnPowerChange(precentage);
    }
    
}

