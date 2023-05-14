// Written By: Damien Carlson
// Created On: 03/16/2023

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>//, IObserver
{
    // Variables
    [SerializeField] private HUDManager hudManager;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject stopWatchCalculator;
    [SerializeField] private SkeletonFactory skeletonFactory;
    [SerializeField] private GameObject gem;
    [SerializeField] private GameObject tree1;
    [SerializeField] private GameObject tree2;
    [SerializeField] private GameObject tree3;
    [SerializeField] private GameObject tree4;
    [SerializeField] private GameObject rock1;
    [SerializeField] private GameObject rock2;
    [SerializeField] private GameObject rock3;
    [SerializeField] private GameObject rock4;
    [SerializeField] private GameObject rock5;

    //[SerializeField] Subject playerSubject;

    public static GameManager gameManager;
    public GameState currentGameState;
    public static event Action<GameState> OnUpdateGameState;
    //public Vector3 origin = Vector3.zero;

    private int gemsCollected = 0;
    private int gemsNeeded = 5;
    private int maxPlayerHealth = 3;
    private int playerHealth = 0;
    private int gemsSpawned = 15;
    private int enemiesSpawned = 20;
    private int obstacleDensity = 250;
    private Vector3 gemSpawnMinPosition = new Vector3(-30, 0, -30);
    private Vector3 gemSpawnMaxPosition = new Vector3(30, 0, 30);

    private Vector3 obstacleSpawnMinPosition = new Vector3(-250, 0, -250);
    private Vector3 obstacleSpawnMaxPosition = new Vector3(250, 0, 250);


    // Methods
    void Start()
    {
        gameManager = this;
        playerHealth = maxPlayerHealth;

        SpawnGems();
        SpawnTrees();
        SpawnRocks();
        SpawnEnemies();
        SetGameState(GameState.Start);
        ResetGame();
    }

    //private void OnEnable()
    //{
    //    Subscribe to the player controller.
    //   playerSubject.AddObserver(this);
    //}

    //private void OnDisable()
    //{
    //    unSubscribe from the player controller.
    //   playerSubject.RemoveObserver(this);
    //}

    //public void OnNotify(PlayerEvents _event)
    //{
    //    switch (_event)
    //    {
    //        case (PlayerEvents.CollectedGem):
    //            UpdateGemCount();
    //            break;
    //        case (PlayerEvents.ReceivedDamage):
    //            UpdateHealth();
    //            break;
    //        default:
    //            return;
    //    }
    //}

    public void PlayGame()
    {
        SetGameState(GameState.Play);
    }

    public void ResetGame()
    {
        // SpawnGems();
        SetGameState(GameState.Start);
        playerHealth = maxPlayerHealth;
        gemsCollected = 0;
    }

    public void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;

        switch (newGameState)
        {
            case GameState.Start:
                menuManager.ActivateStartMenu();
                stopWatchCalculator.SetActive(false);
                break;
            case GameState.Play:
                playerController.LockCursor();
                stopWatchCalculator.SetActive(true);
                hudManager.UpdateGemCounter(gemsCollected, gemsNeeded);
                hudManager.UpdateHealthLabel(playerHealth);
                break;
            case GameState.Win:
                stopWatchCalculator.SetActive(false);
                menuManager.ActivateWinMenu();
                playerController.UnlockCursor();
                break;
            case GameState.Lose:
                stopWatchCalculator.SetActive(false);
                menuManager.ActivateLoseMenu();
                playerController.UnlockCursor();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
        }

        OnUpdateGameState?.Invoke(newGameState);
    }

    public void UpdateGemCount()
    {
        // Updates and sends the gem count to the HUD manager.
        gemsCollected++;
        hudManager.UpdateGemCounter(gemsCollected, gemsNeeded);
    }

    public void UpdateHealth()
    {
        // Updates and sends the player's health to the HUD manager.
        playerHealth--;
        hudManager.UpdateHealthLabel(playerHealth);
    }

    public void CheckWinCondition()
    {
        if (gemsCollected >= gemsNeeded)
        {
            SetGameState(GameState.Win);
        }
    }

    public void CheckLoseCondition()
    {
        if (playerHealth <= 0)
        {
            SetGameState(GameState.Lose);
        }
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < enemiesSpawned; i++)
        {
            skeletonFactory.CreateSkeleton();
        }
    }

    public void SpawnGems()
    {
        for (int i = 0; i < gemsSpawned; i++)
        {
            Vector3 randomPosition = new Vector3(
                    Random.Range(gemSpawnMinPosition.x, gemSpawnMaxPosition.x),
                    0,
                    Random.Range(gemSpawnMinPosition.z, gemSpawnMaxPosition.z));

            Instantiate(gem, randomPosition, gem.transform.rotation);
        }
    }

    public void SpawnTrees()
    {
        for (int i = 0; i < obstacleDensity; i++)
        {
            int treeType = Random.Range(1, 4);
            //float randomRotation = Random.Range(0, 1);
            

            Vector3 randomPosition = new Vector3(
                    Random.Range(obstacleSpawnMinPosition.x, obstacleSpawnMaxPosition.x),
                    -3,
                    Random.Range(obstacleSpawnMinPosition.z, obstacleSpawnMaxPosition.z));
            switch (treeType)
            {
                case 1:
                    //Quaternion direction = tree1.transform.rotation;
                    //direction.y = randomRotation;
                    Instantiate(tree1, randomPosition, tree1.transform.rotation);
                    break;
                case 2:
                    Instantiate(tree2, randomPosition, tree2.transform.rotation);
                    break;
                case 3:
                    Instantiate(tree3, randomPosition, tree3.transform.rotation);
                    break;
                case 4:
                    Instantiate(tree4, randomPosition, tree4.transform.rotation);
                    break;
            }                     
        }
    }

    public void SpawnRocks()
    {
        for (int i = 0; i < obstacleDensity; i++)
        {
            int rockType = Random.Range(1, 5);

            Vector3 randomPosition = new Vector3(
                    Random.Range(obstacleSpawnMinPosition.x, obstacleSpawnMaxPosition.x),
                    -2,
                    Random.Range(obstacleSpawnMinPosition.z, obstacleSpawnMaxPosition.z));
            switch (rockType)
            {
                case 1:
                    Instantiate(rock1, randomPosition, rock1.transform.rotation);
                    break;
                case 2:
                    Instantiate(rock2, randomPosition, rock2.transform.rotation);
                    break;
                case 3:
                    Instantiate(rock3, randomPosition, rock3.transform.rotation);
                    break;
                case 4:
                    Instantiate(rock4, randomPosition, rock4.transform.rotation);
                    break;
                case 5:
                    Instantiate(rock5, randomPosition, rock5.transform.rotation);
                    break;
            }
        }
    }

}

public enum GameState
{
    Start,
    Play,
    Win,
    Lose
}