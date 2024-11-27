using System;
using System.Collections;
using System.Collections.Generic;
using Script.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public TextMeshProUGUI textLabel;

    [Header("Text")]
    public TextAsset textFile;
    public int index;
    
    private PlayerInputControl playerInput;
    private ITalk targetItem;
    // List<ITalk> talkList = new List<ITalk>();
    List<string> textList = new List<string>();
    private void Awake()
    {
        GetTextFromFile(textFile);
        
    }

    private void OnEnable()
    {
        textLabel.text = textList[index];
        index++;

        playerInput.GamePlay.Interact.started += OnStarted;
    }

    private void OnDisable()
    {
        playerInput.GamePlay.Interact.started -= OnStarted;
    }
    
    private void Update()
    {
        
    }

    private void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;
       var lineData = file.text.Split('\n');

       foreach (var line in lineData)
       {
            textList.Add(line); 
       }
    }
    
    private void OnStarted(InputAction.CallbackContext context)
    {
        if (index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        textLabel.text = textList[index];
        index++;
    }
    // public void AddObserver(ITalk observer)
    // {
    //     talkList.Add(observer);
    // }
    //
    // public void RemoveObserver(ITalk observer)
    // {
    //     talkList.Remove(observer);
    // }

    
}
