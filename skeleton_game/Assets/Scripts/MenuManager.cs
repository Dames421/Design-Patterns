// Written By: Damien Carlson
// Created On: 03/16/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class MenuManager : MonoBehaviour
{
    // Variables
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject loseMenu;
    [SerializeField] private GameObject playerHUD;

    [SerializeField] private TextMeshProUGUI lblFinalTime;

    [SerializeField] private Button startButton;


    // Methods
    void Awake()
    {
        GameManager.OnUpdateGameState += GameManager_OnUpdateGameState;
    }

    private void OnDestroy()
    {
        GameManager.OnUpdateGameState -= GameManager_OnUpdateGameState;
    }

    private void GameManager_OnUpdateGameState(GameState gameState)
    {
        startMenu.SetActive(gameState == GameState.Start);

        //startButton.interactable = gameState == GameState.Play;
    }

    // Triggers when the start button is pressed.
    public void StartGame()
    {
        GameManager.gameManager.SetGameState(GameState.Play);
    }

    // Pulls up the start menu and disables the win and lose menus.
    public void ActivateStartMenu()
    {
        playerController.canMove = false;
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
        playerHUD.SetActive(false);
        startMenu.SetActive(true);
    }

    // Pulls up the screen for winners :)
    public void ActivateWinMenu()
    {
        playerController.canMove = false;
        winMenu.SetActive(true);
        playerHUD.SetActive(false);
    }

    // Pulls up the screen for losers :(
    public void ActivateLoseMenu()
    {
        playerController.canMove = false;
        loseMenu.SetActive(true);
        playerHUD.SetActive(false);
    }

    public void UpdateFinalTime(float minutes, float seconds, float milliseconds)
    {
        lblFinalTime.text = "Your time - " + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
    }
    public void ResetFinalTime()
    {
        lblFinalTime.text = "Your time - 00:00:00";
    }
}
