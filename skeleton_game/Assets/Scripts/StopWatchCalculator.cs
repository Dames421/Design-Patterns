// Written By: Damien Carlson
// Created On: 03/16/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWatchCalculator : MonoBehaviour
{
    // Variables
    private float timer;
    private float milliseconds;
    private float seconds;
    private float minutes;

    [SerializeField] private HUDManager hudManager;
    [SerializeField] private MenuManager menuManager;

    // Start is called before the first frame update
    void OnEnable()
    {
        menuManager.ResetFinalTime();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {  
        CalculateTime();
    }

    void CalculateTime()
    {
        timer += Time.deltaTime;

        milliseconds = (int)(timer * 1000) % 1000;
        seconds = (int)(timer % 60);
        minutes = (int)(timer / 60);

        hudManager.UpdateTimer(minutes, seconds, milliseconds);
    }

    private void OnDisable()
    {
        menuManager.UpdateFinalTime(minutes, seconds, milliseconds);
    }
}
