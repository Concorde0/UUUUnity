using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsManager : MonoBehaviour
{
    public static SkillsManager instance;
    
    public GameObject[] UpuiSkills;
    public GameObject[] DownuiSkills;
    private int UpIndex = 0;
    private int DownIndex = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        foreach (GameObject ui in UpuiSkills)
        {
            ui.SetActive(false);
        }
        UpuiSkills[UpIndex].SetActive(true);
        
        
        foreach (GameObject ui in DownuiSkills)
        {
            ui.SetActive(false);
        }
        DownuiSkills[DownIndex].SetActive(true);
    }

    void Update()
    {
        
    }

    public  void UpChangeSkill()
    {
        Debug.Log("up change");
        foreach (GameObject ui in UpuiSkills)
        {
            ui.SetActive(false);
        }
        UpIndex = (UpIndex + 1) % UpuiSkills.Length;
        UpuiSkills[UpIndex].SetActive(true);
    }
    public void DownChangeSkill()
    {
        Debug.Log("down change");
        foreach (GameObject ui in DownuiSkills)
        {
            ui.SetActive(false);
        }
        DownIndex = (DownIndex + 1) % DownuiSkills.Length;
        DownuiSkills[DownIndex].SetActive(true);
    }
}
