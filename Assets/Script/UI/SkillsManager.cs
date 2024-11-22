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
    public GameObject[] RightuiSkills;
    public GameObject[] LeftuiSkills;
    private int UpIndex = 0;
    private int DownIndex = 0;
    private int RightIndex = 0;
    private int LeftIndex = 0;

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

        foreach (GameObject ui in RightuiSkills)
        {
            ui.SetActive(false);
        }
        RightuiSkills[RightIndex].SetActive(true);

        foreach (GameObject ui in LeftuiSkills)
        {
            ui.SetActive(false);
        }
        LeftuiSkills[LeftIndex].SetActive(true);
    }

    public void UpChangeSkill()
    {
        
        foreach (GameObject ui in UpuiSkills)
        {
            ui.SetActive(false);
        }
        UpIndex = (UpIndex + 1) % UpuiSkills.Length;
        UpuiSkills[UpIndex].SetActive(true);
    }
    public void DownChangeSkill()
    {
       
        foreach (GameObject ui in DownuiSkills)
        {
            ui.SetActive(false);
        }
        DownIndex = (DownIndex + 1) % DownuiSkills.Length;
        DownuiSkills[DownIndex].SetActive(true);
    }

    public void LeftChangeSkill()
    {
        foreach (GameObject ui in LeftuiSkills)
        {
            ui.SetActive(false);
        }
        LeftIndex = (LeftIndex + 1) % LeftuiSkills.Length;
        LeftuiSkills[LeftIndex].SetActive(true);
    }

    public void RightChangeSkill()
    {
        foreach (GameObject ui in RightuiSkills)
        {
            ui.SetActive(false);
        }
        RightIndex = (RightIndex + 1) % RightuiSkills.Length;
        RightuiSkills[RightIndex].SetActive(true);
    }
}
