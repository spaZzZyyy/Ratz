using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemies : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public int currWave = 1;
    public int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    public Transform spawnLocation;
    public int waveDuration = 30;
    public float waveTimer;

    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnTimer;

    [SerializeField] private TMP_Text mainText;

    private void Start()
    {
        GenerateWave();
        mainText.SetText("Wave: " + currWave);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnTimer < 0)
        {
            if (enemiesToSpawn.Count > 0)
            {
                spawnLocation = this.transform.GetChild(returnRandomSpawn()).gameObject.transform;

                GameObject enemy = (GameObject)Instantiate(enemiesToSpawn[0], spawnLocation.position, Quaternion.identity); // spawn first enemy in our list
                enemiesToSpawn.RemoveAt(0); // and remove it
                //spawnedEnemies.Add(enemy);
                spawnedEnemies.Add(enemy);
                spawnTimer = spawnInterval;
            }
            else
            {
                waveTimer = 0;
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }
        if (waveTimer <= 0 && !spawnedEnemies.Any())
        {
            currWave++;
            GenerateWave();
            mainText.SetText("Wave: " + currWave);
        }
        if (spawnedEnemies.Count > 0)
        {
            for (int i = 0; i <= spawnedEnemies.Count() - 1; i++)
            {
                if (!spawnedEnemies[i] || spawnedEnemies[0] == null)
                {
                    spawnedEnemies.RemoveAt(i);
                }
            }
        }
        if (spawnedEnemies.Any())
        {
            if (!spawnedEnemies[0] && !spawnedEnemies.Any())
            {
                waveDuration += 2;
                spawnedEnemies.Clear();
            }
        }

    }
    public void GenerateWave()
    {
        waveValue = currWave * 5;
        GenerateEnemies();
        if (enemiesToSpawn.Count > 0)
        {
            spawnInterval = 1;
            waveTimer = waveDuration;
        }
    }

    public void GenerateEnemies()
    {


        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0)
        {
            int randEnemieId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemieId].cost;

            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemieId].prefab);
                waveValue -= randEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;

    }
    private int returnRandomSpawn()
    {
        int randSpawn = Random.Range(0, this.transform.childCount);
        Debug.Log(randSpawn);
        return randSpawn;
    }
}



[System.Serializable]

public class Enemy
{
    public GameObject prefab;
    public int cost;
}