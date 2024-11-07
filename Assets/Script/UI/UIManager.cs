using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    [Header("事件监听")]
    public CharacterEventSO healthEvent;
    public CharacterEventSO powerEvent;
    public SceneLoadEventSO unloadedSceneEvent;
    public VoidEventSO loadDataEvent;
    public VoidEventSO gameOverEvent;
    public VoidEventSO backToMenuEvent;

    [Header("组件")]
    public GameObject gameOverPanel;
    public GameObject restartButtom;
    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
        powerEvent.OnEventRaised += OnPowerEvent;
        //healthEvent.OnEventRaised += KnightOnHealEvent;
        unloadedSceneEvent.LoadRequestEvent += OnUnLoadedSceneEvent;
        loadDataEvent.OnEventRaised += OnLoadDataEvnet;
        gameOverEvent.OnEventRaised += OnGameOverEvnet;
        backToMenuEvent.OnEventRaised += OnLoadDataEvnet;
    }
    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
        powerEvent.OnEventRaised -= OnPowerEvent;
        //healthEvent.OnEventRaised -= KnightOnHealEvent;
        unloadedSceneEvent.LoadRequestEvent -= OnUnLoadedSceneEvent;
        loadDataEvent.OnEventRaised -= OnLoadDataEvnet;
        gameOverEvent.OnEventRaised -= OnGameOverEvnet;
        backToMenuEvent.OnEventRaised -= OnLoadDataEvnet;
    }

    

    private void OnGameOverEvnet()
    {
        
        gameOverPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(restartButtom);
    }

    private void OnLoadDataEvnet()
    {
        
        gameOverPanel.SetActive(false);
    }

    private void OnUnLoadedSceneEvent(GameSceneSO scenToLoad, Vector3 arg1, bool arg2)
    {
        var isMenu = scenToLoad.sceneType == SceneType.Menu;
           playerStatBar.gameObject.SetActive(!isMenu);
        
    }

    private void OnHealthEvent(Character character)
    {
        //Debug.Log("health");
        var precentage = character.currentHealth / character.maxHealth;

        playerStatBar.OnHealthChange(precentage);
    }
    private void OnPowerEvent(Character character)
    {
        //Debug.Log("power");
        var precentage = character.currentPower / character.maxPower;
        playerStatBar.OnPowerChange(precentage);
    }
    
}

