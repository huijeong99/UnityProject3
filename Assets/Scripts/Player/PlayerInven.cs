using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInven : MonoBehaviour
{
    int money = 0;
    public TextMeshProUGUI playerMoney;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        showMoney();
    }

    private void showMoney()
    {
        playerMoney.text = " "+money;
    }
}
