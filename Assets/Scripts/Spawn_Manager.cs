using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject[] Items;
    private float zBound = 8f;
    float zBound_item = 5f;
    private float xBound = 11f;
    public int WaveCount = 0;
    private int FindEnemy;
    GameObject[] FindItem;
    bool callItem = false;
    GameManager GameManager;

    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        Debug.Log("hello");

    }

    void Update()
    {
        FindEnemy = FindObjectsOfType<Enemy>().Length;
        if (FindEnemy == 0 && GameManager.GameActive)
        {
            if (callItem == false)
            {
                callItem = true;
                InvokeRepeating("Spawn_Item_random", GameManager.difficultSpawnItemRate, GameManager.difficultSpawnItemRate);
            }
            WaveCount += GameManager.difficultSpawnRate;
            spwan_enemy_wave(WaveCount);
            Debug.Log(WaveCount);
            DestroyItem();
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

    void spwan_enemy_wave(int EnemyWaveCount)
    {

        for (int i = 0; i < EnemyWaveCount; i++)
        {
            Spawn_enemies_random();
        }
    }
}
