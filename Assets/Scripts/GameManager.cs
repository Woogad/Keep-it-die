using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour

{
    [SerializeField] GameObject GameTitle;
    [SerializeField] GameObject GameOverTitle;
    [SerializeField] GameObject GameDifficultyTitle;
    [SerializeField] Button Restart;
    [SerializeField] Button StartGame;
    [SerializeField] Button Easy;
    [SerializeField] Button Medium;
    [SerializeField] Button Hard;
    [HideInInspector] public int difficultSpawnRate;
    [HideInInspector] public float difficultSpawnItemRate = 1f;
    PlayerController PlayerController;
    Spawn_Manager Spawn_Manager;
    public bool GameActive = false;


    private void Start()
    {
        //* Getcompoent
        PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
        Spawn_Manager = GameObject.Find("Spawn Manager").GetComponent<Spawn_Manager>();

        //* Button
        Button StartGamebt = StartGame.GetComponent<Button>();
        Button Restartbt = Restart.GetComponent<Button>();
        Button Easybt = Easy.GetComponent<Button>();
        Button Mediumbt = Medium.GetComponent<Button>();
        Button Hardbt = Hard.GetComponent<Button>();

        //* Button Listener
        StartGamebt.onClick.AddListener(OnGameStart);
        Restartbt.onClick.AddListener(OnGameRestart);
        Easybt.onClick.AddListener(OnEasy);
        Mediumbt.onClick.AddListener(OnMedium);
        Hardbt.onClick.AddListener(OnHard);
    }

    private void Update()
    {
        if (PlayerController.isPlayerAlive == false)
        {
            GameOverTitle.SetActive(true);
        }
    }

    void OnGameRestart()
    {
        SceneManager.LoadScene("Keep it die");
    }

    void OnGameStart()
    {
        GameTitle.SetActive(false);
        GameDifficultyTitle.SetActive(true);
    }
    void OnEasy()
    {
        difficultSpawnRate = 3;
        difficultSpawnItemRate = 6f;
        //int SpawnWavecount = Spawn_Manager.WaveCount * 3;
        GameActive = true;
        GameDifficultyTitle.SetActive(false);
    }
    void OnMedium()
    {
        difficultSpawnRate = 4;
        difficultSpawnItemRate = 12f;
        Debug.Log("Medium");
        GameActive = true;
        GameDifficultyTitle.SetActive(false);
    }
    void OnHard()
    {
        difficultSpawnRate = 5;
        difficultSpawnItemRate = 15f;
        Debug.Log("Hard");
        GameActive = true;
        GameDifficultyTitle.SetActive(false);
    }
}

