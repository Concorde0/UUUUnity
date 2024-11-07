using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public int originalCoinNumber;
    public static int currentCoinNumber;
    public TextMeshProUGUI CoinNumber;

    private void Start()
    {
        currentCoinNumber = originalCoinNumber;
    }
    private void Update()
    {
        CoinNumber.text = currentCoinNumber.ToString();
        
    }
}
