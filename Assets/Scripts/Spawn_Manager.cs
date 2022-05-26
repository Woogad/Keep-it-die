using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject[] Items;
    private GameObject[] FindItem;
    private GameManager GameManager;
    private GameObject[] FindEnemy;

    private float zBound = 8f;
    private float zBound_item = 5f;
    public float difficultSpawnRate;
    private float xBound = 11f;
    public float diffIncrease = 0f;
    private int WaveTime;
    private int Time = 40;
    private int CdTime = 3;
    private int Cwave;
    private bool Wave_isStart;

    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        WaveTime = Time;
        Wave_isStart = true;
        Cwave = 0;
    }

    void Update()
    {
        if (GameManager.GameActive)
        {
            if (WaveTime > 0 && Wave_isStart)
            {
                difficultSpawnRate = GameManager.difficultSpawnRate;
                difficultSpawnRate -= diffIncrease;
                WaveStart();
                Wave_isStart = false;
            }
            if (Wave_isStart == false && WaveTime < 1)
            {
                WaveEnd();
                StartCoroutine(NewWave());
            }
            GameManager.CountTime(WaveTime);
        }
    }

    void DestroyEnemy()
    {
        FindEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < FindEnemy.Length; i++)
        {
            Destroy(FindEnemy[i]);
        }
    }

    void DestroyItem()
    {
        FindItem = GameObject.FindGameObjectsWithTag("Item");
        for (int i = 0; i < FindItem.Length; i++)
        {
            Destroy(FindItem[i]);
        }
    }

    void Spawn_enemies_random()
    {
        float xRandom_position = Random.Range(-xBound, xBound);
        int Random_eneies_Index = Random.Range(0, Enemies.Length);
        Vector3 xSpawn_pos_random = new Vector3(xRandom_position, 0.55f, zBound);
        Instantiate(Enemies[Random_eneies_Index], xSpawn_pos_random, Enemies[Random_eneies_Index].transform.rotation);
    }

    void Spawn_Item_random()
    {
        float xRandom_position = Random.Range(-xBound, xBound);
        float zRandom_position = Random.Range(-zBound_item, zBound_item);
        int Random_Items_Index = Random.Range(0, Items.Length);
        Vector3 xSpawn_pos_random = new Vector3(xRandom_position, 0.55f, zRandom_position);
        Instantiate(Items[Random_Items_Index], xSpawn_pos_random, Items[Random_Items_Index].gameObject.transform.rotation);

    }

    void spwan_enemy_wave(float EnemyWaveSpawnRate)
    {
        InvokeRepeating("Spawn_enemies_random", 1f, EnemyWaveSpawnRate);
        InvokeRepeating("Spawn_Item_random", GameManager.difficultSpawnItemRate, GameManager.difficultSpawnItemRate);
    }

    void WaveStart()
    {
        Debug.Log("Wave is started");
        Cwave++;
        GameManager.CountWave(Cwave);
        spwan_enemy_wave(difficultSpawnRate);
        InvokeRepeating("TimeRemain", 1f, 1f);
    }

    void TimeRemain()
    {
        WaveTime--;
    }

    void WaveEnd()
    {
        Debug.Log("Wave End");
        CancelInvoke();
        DestroyItem();
        DestroyEnemy();
        Wave_isStart = true;
        diffIncrease += 0.05f;
    }

    IEnumerator NewWave()
    {
        Debug.Log("cd time");
        yield return new WaitForSeconds(CdTime);
        WaveTime = Time;
    }
}
