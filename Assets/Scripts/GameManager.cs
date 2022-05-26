using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameTitle;
    [SerializeField] GameObject GameOverTitle;
    [SerializeField] GameObject GameDifficultyTitle;
    [SerializeField] GameObject GUITitle;
    [SerializeField] Button Restart;
    [SerializeField] Button StartGame;
    [SerializeField] Button Easy;
    [SerializeField] Button Medium;
    [SerializeField] Button Hard;
    [SerializeField] TextMeshProUGUI WaveTitle;
    [SerializeField] TextMeshProUGUI AmmoTitle;
    [SerializeField] TextMeshProUGUI WaveTimeTitle;

    [HideInInspector] public float difficultSpawnRate;
    [HideInInspector] public float difficultSpawnItemRate = 1f;

    PlayerController PlayerController;
    Spawn_Manager Spawn_Manager;
    Shooting Shooting;

    int AmmoCount;
    int Timer;
    int WaveCount;
    float DifSpawnRateOnEasy = 1.8f;
    float DifSpawnRateOnMedium = 1.5f;
    float DifSpawnRateOnHard = 1.2f;
    float DifSpawnItemRateOnEasy = 6f;
    float DifSpawnItemRateOnMedium = 8f;
    float DifSpawnItemRateOnHard = 10f;

    public bool GameActive = false;

    private void Start()
    {
        //* Getcompoent
        PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
        Spawn_Manager = GameObject.Find("Spawn Manager").GetComponent<Spawn_Manager>();
        Shooting = PlayerController.GetComponent<Shooting>();

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
        if (GameActive)
        {
            GUITitle.SetActive(true);
        }
        if (PlayerController.isPlayerAlive == false)
        {
            GameOverTitle.SetActive(true);
        }
    }

    public void CountTime(int Timeadd)
    {
        Timer = Timeadd;
        WaveTimeTitle.text = "Time: " + Timer;
        if (Timer < 1)
        {
            WaveTimeTitle.text = "Ready to next wave";
        }
    }

    public void CountAmmo(int Ammoadd)
    {
        AmmoCount = Ammoadd;
        AmmoTitle.text = "Ammo: " + AmmoCount;
        if (Shooting.isReload)
        {
            AmmoTitle.text = "Ammo: Reloading";
            return;
        }

    }

    public void CountWave(int WaveCountadd)
    {
        WaveCount = WaveCountadd;
        WaveTitle.text = "Wave: " + WaveCount;
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
        difficultSpawnRate = DifSpawnRateOnEasy;
        difficultSpawnItemRate = DifSpawnItemRateOnEasy;
        Debug.Log("Easy");
        GameActive = true;
        GameDifficultyTitle.SetActive(false);
    }
    void OnMedium()
    {
        difficultSpawnRate = DifSpawnRateOnMedium;
        difficultSpawnItemRate = DifSpawnItemRateOnMedium;
        Debug.Log("Medium");
        GameActive = true;
        GameDifficultyTitle.SetActive(false);
    }
    void OnHard()
    {
        difficultSpawnRate = DifSpawnRateOnHard;
        difficultSpawnItemRate = DifSpawnItemRateOnHard;
        Debug.Log("Hard");
        GameActive = true;
        GameDifficultyTitle.SetActive(false);
    }
}

