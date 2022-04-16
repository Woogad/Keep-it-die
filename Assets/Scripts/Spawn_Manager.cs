using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject[] Items;
    private float zBound = 8f;
    private float xBound = 11f;
    private int WaveCount = 1;
    private int FindEnemy;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        spwan_enemy_wave(WaveCount);
        player = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        FindEnemy = FindObjectsOfType<Enemy>().Length;
        if (FindEnemy == 0 && player.isGameOver == false)
        {
            WaveCount++;
            spwan_enemy_wave(WaveCount);
        }

    }

    void Spawn_enemies_random()
    {
        float xRandom_position = Random.Range(-xBound, xBound);
        int Random_eneies_Index = Random.Range(0, Enemies.Length);
        Vector3 xSpawn_pos_random = new Vector3(xRandom_position, 0.55f, zBound);
        Instantiate(Enemies[Random_eneies_Index], xSpawn_pos_random, Enemies[Random_eneies_Index].gameObject.transform.rotation);
    }
    void Spawn_Item_random()
    {
        float xRandom_position = Random.Range(-xBound, xBound);
        int Random_Items_Index = Random.Range(0, Items.Length);
        Vector3 xSpawn_pos_random = new Vector3(xRandom_position, 0.55f, 0);
        Instantiate(Items[Random_Items_Index], xSpawn_pos_random, Items[Random_Items_Index].gameObject.transform.rotation);

    }
    void spwan_enemy_wave(int EnemyWaveCount)
    {

        for (int i = 0; i < EnemyWaveCount; i++)
        {
            Spawn_enemies_random();
        }
        Spawn_Item_random();

    }
}
