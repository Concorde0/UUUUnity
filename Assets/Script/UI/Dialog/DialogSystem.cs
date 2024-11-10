//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class DialogSystem : MonoBehaviour,IInteractable
//{
//    [Header("UI×é¼þ")]
//    public TextMeshProUGUI textLable;

//    [Header("Text")]
//    public TextAsset textFile;
//    public int index;

//    List<string> textList = new List<string>();
//    private void Start()
//    {
//        GetTextFromFile(textFile);
//        index = 0;
//    }
//    private void Update()
//    {
        
//    }

//    private void GetTextFromFile(TextAsset file)
//    {
//        textList.Clear();
//        index = 0;
//       var lineData = file.text.Split('\n');

//       foreach (var line in lineData)
//       {
//            textList.Add(line); 
//       }
//    }

//    public void TriggerAction()
//    {
//        textLable.text = textList[index];
//        index++;
//    }
//}
