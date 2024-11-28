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
    public int index = 10;

    private ITalk targetItem;
    public List<string> textList = new List<string>();
    private void Awake()
    {
        GetTextFromFile(textFile);
    }

    

    private void OnEnable()
    {
        textLabel.text = textList[index];
        index++;
        StartTalk();
    }
    
    
    private void Update()
    {
        if (index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
        }
        
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

    private void StartTalk()
    {
        StartCoroutine(abc());
    }
    IEnumerator abc()
    {
        StartCoroutine(SetText());
        yield return new WaitForSeconds(3f);
        StartCoroutine(SetText());
        yield return new WaitForSeconds(3f);
        StartCoroutine(SetText());
        yield return new WaitForSeconds(4f);
        StartCoroutine(SetText());
        yield return new WaitForSeconds(2f);
        StartCoroutine(SetText());
    }

    IEnumerator SetText()
    {
        textLabel.text = "";
        for (int i = 0; i < textList[index].Length; i++)
        {
            textLabel.text += textList[index][i];
            yield return new WaitForSeconds(0.1f);
        }
        
        index++;
    }
    
    

    
}
