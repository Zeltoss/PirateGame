using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // this script manages the spawning of the enemies

    public GameObject enemyPrefab;

    //[SerializeField] private int minEnemySpawns;
    [SerializeField] private int maxEnemySpawns;

    private List<GameObject> spawnAreas;

    private UnityEngine.Vector3 spawnPoint;
    private float spawnPointZ;
    private bool spawnTooClose;

    private bool firstSpawn = true;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private int spawnCount;


    public delegate void OnLeavingSafeZone();
    public static OnLeavingSafeZone onLeavingSafeZone;



    void OnEnable()
    {
        onLeavingSafeZone += SpawnEnemies;
    }

    void OnDisable()
    {
        onLeavingSafeZone -= SpawnEnemies;
    }



    void Start()
    {
        spawnAreas = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spawn Area"));
    }




    private void SpawnEnemies()
    {
        spawnTooClose = false;

        // randomly selects a potential spawn point for an enemy
        int i = Random.Range(0, spawnAreas.Count);
        GameObject chosenArea = spawnAreas[i];

        if (chosenArea.transform.position.z < 0)
        {
            spawnPointZ = -5f;
        }
        else
        {
            spawnPointZ = 5f;
        }

        UnityEngine.Vector2 spawnPointX = Random.insideUnitCircle * 40;
        spawnPoint = new UnityEngine.Vector3(spawnPointX.x, 2.4f, spawnPointZ);

        // this prevents the enemies from spawning too close to each other
        if (!firstSpawn)
        {
            foreach (GameObject enemy in spawnedEnemies)
            {
                float distance = UnityEngine.Vector3.Distance(enemy.transform.position, spawnPoint);
                if (distance < 10)
                {
                    spawnTooClose = true;
                }
            }
        }

        if (spawnTooClose)
        {
            SpawnEnemies();
        }
        else
        {
            StartCoroutine(SpawningCooldown());
        }
    }


    private IEnumerator SpawningCooldown()
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint, UnityEngine.Quaternion.identity);
        spawnedEnemies.Add(enemyInstance);
        spawnCount++;
        firstSpawn = false;

        float pause = Random.Range(2f, 5f);
        yield return new WaitForSeconds(pause);

        if (spawnCount < maxEnemySpawns)
        {
            SpawnEnemies();
        }
    }
}
