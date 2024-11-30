using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // this script manages the spawning of the enemies

    [SerializeField] private GameObject player;

    public GameObject enemyPrefab;

    //[SerializeField] private int minEnemySpawns;
    [SerializeField] private int maxEnemySpawns;

    private List<GameObject> spawnAreas;

    private UnityEngine.Vector3 spawnPoint;
    private float spawnPointZ;
    private bool spawnTooClose;

    private bool firstSpawn = true;

    private List<UnityEngine.Vector3> usedSpawnPoints = new List<UnityEngine.Vector3>();

    private int spawnCount;
    private int killCount;


    public delegate void OnLeavingSafeZone();
    public static OnLeavingSafeZone onLeavingSafeZone;

    [SerializeField] private GameObject droppedCrossbow;



    void OnEnable()
    {
        onLeavingSafeZone += SpawnEnemies;
        SkillTreeManager.onKillingEnemy += CountDefeatedEnemies;
    }

    void OnDisable()
    {
        onLeavingSafeZone -= SpawnEnemies;
        SkillTreeManager.onKillingEnemy -= CountDefeatedEnemies;
    }



    void Start()
    {
        spawnAreas = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spawn Area"));

        droppedCrossbow.SetActive(false);
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
        spawnPoint = new UnityEngine.Vector3(spawnPointX.x, 4f, spawnPointZ);

        // this prevents the enemies from spawning too close to each other
        if (!firstSpawn)
        {
            foreach (UnityEngine.Vector3 point in usedSpawnPoints)
            {
                float distance = UnityEngine.Vector3.Distance(point, spawnPoint);
                if (distance < 10)
                {
                    spawnTooClose = true;
                }
            }
            // remove from list after certain amount of time?
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
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint, UnityEngine.Quaternion.Euler(new UnityEngine.Vector3(90, 180, 0)));
        usedSpawnPoints.Add(enemyInstance.transform.position);
        enemyInstance.GetComponent<EnemyAI>().player = player;
        spawnCount++;
        firstSpawn = false;

        float pause = Random.Range(12f, 16f);
        yield return new WaitForSeconds(pause);

        if (spawnCount < maxEnemySpawns)
        {
            SpawnEnemies();
        }
    }


    private void CountDefeatedEnemies(GameObject lastEnemy)
    {
        killCount++;
        if ((killCount - 1) == maxEnemySpawns)
        {
            droppedCrossbow.transform.position = new UnityEngine.Vector3(lastEnemy.transform.position.x, droppedCrossbow.transform.position.y, lastEnemy.transform.position.z);
            StartCoroutine(DropCrossbow());
        }
    }


    private IEnumerator DropCrossbow()
    {
        droppedCrossbow.SetActive(true);
        droppedCrossbow.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(2);
        droppedCrossbow.GetComponent<BoxCollider>().enabled = true;
    }
}
