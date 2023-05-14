// Written By: Damien Carlson
// Created On: 03/16/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    // Variables
    [SerializeField] private TextMeshProUGUI hudTimer;
    [SerializeField] private TextMeshProUGUI hudHealth;
    [SerializeField] private TextMeshProUGUI hudGemCount;


    // Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGemCounter(int gemCount, int maxGems)
    {
        // Adds 1 to the gem counter on the HUD
        hudGemCount.text = "Gems Collected: " + gemCount + "/" + maxGems;
    }

    public void UpdateHealthLabel(int currentHealth)
    {
        hudHealth.text = "Lives: " + currentHealth;
    }

    public void UpdateTimer(float minutes, float seconds, float milliseconds)
    {
        hudTimer.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
    }
}
