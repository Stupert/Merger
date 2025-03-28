using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{

    string dateTime;
    double epoch;

    [SerializeField]TMP_Text timeUI;

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GetTime();
        }
    }

    public double GetTime()
    {
        epoch = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        return epoch;
    }

    public void UpdateTime(double savedTime)
    {
        double diff = GetTime() - savedTime;
        int timeTemp = (int)diff;
        timeUI.text = timeTemp.ToString();
    }
}
