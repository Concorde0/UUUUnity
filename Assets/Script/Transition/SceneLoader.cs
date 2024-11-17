using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEditor;

public class SceneLoader : MonoBehaviour,ISaveable
{
    public Transform playerTrans;
    public Vector3 firstPosition;
    public Vector3 menuPosition;
    [Header("事件监听")]
    public SceneLoadEventSO loadEventSO;
    public VoidEventSO newGameEvent;
    public VoidEventSO backToMenuEvnet;
    [Header("广播")]
    public VoidEventSO afterSceneLoadedEvent;
    public FadeEvnetSO fadeEvnet;
    public SceneLoadEventSO unloadedeSceneEvent;
    [Header("场景")]
    public GameSceneSO firstLoadScene;
    public GameSceneSO menuLoadScene;
    public GameSceneSO currentLoadedScene;
    private GameSceneSO sceneToLoad;
    private Vector3 positionToGo;
    private bool fadeScreen;
    public float fadeDuration;
    public bool isLoading;
    private void Awake()
    {
        //Addressables.LoadSceneAsync(firstLoadScene.sceneReference, LoadSceneMode.Additive);
        //currentLoadedScene = firstLoadScene;
        //currentLoadedScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
        
    }
    
    private void Start()
    {
        //NewGame();
        loadEventSO.RaiseLoadRequestEvent(menuLoadScene, menuPosition, true);
    }

    private void OnEnable()
    {
        loadEventSO.LoadRequestEvent += OnLoadRequestEvent;
        newGameEvent.OnEventRaised += NewGame;
        backToMenuEvnet.OnEventRaised += OnbackToMenuEvnet;

        ISaveable saveable = this;
        saveable.RegisterSaveData();

    }

    private void OnDisable()
    {
        loadEventSO.LoadRequestEvent -= OnLoadRequestEvent;
        newGameEvent.OnEventRaised -= NewGame;
        backToMenuEvnet.OnEventRaised -= OnbackToMenuEvnet;
        ISaveable saveable = this;
        saveable.UnRegisterSaveData();
    }

    private void OnbackToMenuEvnet()
    {
        sceneToLoad = menuLoadScene;
        loadEventSO.RaiseLoadRequestEvent(sceneToLoad, menuPosition, true);
    }

    private void NewGame()
    {
        sceneToLoad = firstLoadScene;
        //OnLoadRequestEvent(sceneToLoad, firstPosition, true);
        loadEventSO.RaiseLoadRequestEvent(sceneToLoad, firstPosition, true);
    }
    /// <summary>
    /// ���������¼�����
    /// </summary>
    /// <param name="locationToLoad"></param>
    /// <param name="posToGo"></param>
    /// <param name="fadeScreen"></param>
    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScreen)
    {
        if (isLoading)
        {
            return;
        }
        isLoading = true;
        sceneToLoad = locationToLoad;
        positionToGo = posToGo;
        this.fadeScreen = fadeScreen;


        if (currentLoadedScene != null)
        {
            StartCoroutine(UnLoadPreviousScene());
        }
        else
        {
            LoadNewScene();
        }
    }
    private IEnumerator UnLoadPreviousScene()
    {
        if(fadeScreen)
        {

            fadeEvnet.FadeIn(fadeDuration);
        }
        yield return new WaitForSeconds(fadeDuration);

        unloadedeSceneEvent.RaiseLoadRequestEvent(sceneToLoad,positionToGo,true);

        yield return currentLoadedScene.sceneReference.UnLoadScene();
        
        playerTrans.gameObject.SetActive(false);


        LoadNewScene();
    }
    private void LoadNewScene()
    {
     var loadingOption =sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadingOption.Completed += OnLoadCompleted;
    }
    /// <summary>
    /// ����������ɺ�
    /// </summary>
    /// <param name="handle"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        currentLoadedScene = sceneToLoad;

        playerTrans.position = positionToGo;

        playerTrans.gameObject.SetActive(true);
        if (fadeScreen)
        {
            fadeEvnet.FadeOut(fadeDuration);
        }
        isLoading = false;
        
        if(currentLoadedScene.sceneType != SceneType.Menu)
        {
            //����������ɺ��¼�
            afterSceneLoadedEvent.RaiseEvent();
        }
        
    }

    public DataDefination GetDataID()
    {
        return GetComponent<DataDefination>();
    }

    public void GetSaveData(Data data)
    {
        data.SaveGameScene(currentLoadedScene);
    }

    public void LoadData(Data data)
    {
        var playerID = playerTrans.GetComponent<DataDefination>().ID;
        if (data.characterPosDict.ContainsKey(playerID))
        {
            positionToGo = data.characterPosDict[playerID];
            sceneToLoad = data.GetSavedScene();

            OnLoadRequestEvent(sceneToLoad, positionToGo,true);
        }
    }
}
