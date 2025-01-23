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
    public GameObject rapierEnemyPrefab;
    private GameObject[] enemyTypes = new GameObject[2];

    private List<GameObject> spawnAreas;

    private UnityEngine.Vector3 spawnPoint;
    private float spawnPointZ;
    private bool spawnTooClose;

    private bool firstSpawn = true;

    private List<UnityEngine.Vector3> usedSpawnPoints = new List<UnityEngine.Vector3>();

    public int meleeEnemySpawns;
    public int rapierEnemySpawns;
    private int[] maxEnemySpawns = new int[2];
    private int[] enemySpawns = new int[2];
    private int totalSpawnCount;
    private int killCount;


    public delegate void OnLeavingSafeZone();
    public static OnLeavingSafeZone onLeavingSafeZone;

    [SerializeField] private GameObject droppedWeapon;

    private List<GameObject> currentEnemies;
    private int enemiesAtStart;



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
        currentEnemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        foreach (GameObject enemy in currentEnemies)
        {
            enemiesAtStart++;
        }
        enemyTypes[0] = enemyPrefab;
        enemyTypes[1] = rapierEnemyPrefab;
        maxEnemySpawns[0] = meleeEnemySpawns;
        maxEnemySpawns[1] = rapierEnemySpawns;

        droppedWeapon.SetActive(false);
    }




    private void SpawnEnemies()
    {
        spawnTooClose = false;

        // randomly selects a potential spawn point for an enemy
        int i = Random.Range(0, spawnAreas.Count);
        GameObject chosenArea = spawnAreas[i];

        if (chosenArea.transform.position.z < 0)
        {
            spawnPointZ = -4f;
        }
        else
        {
            spawnPointZ = 4f;
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
        if (firstSpawn)
        {
            yield return new WaitForSeconds(5f);
        }
        int type = Random.Range(0, enemyTypes.Length);
        if (enemySpawns[type] >= maxEnemySpawns[type])
        {
            type = (type == 0) ? 1 : 0;
        }
        GameObject enemyInstance = Instantiate(enemyTypes[type], spawnPoint, UnityEngine.Quaternion.Euler(new UnityEngine.Vector3(90, 180, 0)));
        currentEnemies.Add(enemyInstance);
        usedSpawnPoints.Add(enemyInstance.transform.position);
        enemyInstance.GetComponent<EnemyAI>().player = player;
        enemySpawns[type]++;
        totalSpawnCount++;
        firstSpawn = false;

        float pause = Random.Range(14f, 18f);
        yield return new WaitForSeconds(pause);

        if (totalSpawnCount < (maxEnemySpawns[0] + maxEnemySpawns[1]))
        {
            SpawnEnemies();
        }
    }


    private void CountDefeatedEnemies(GameObject lastEnemy)
    {
        killCount++;
        currentEnemies.Remove(lastEnemy);
        if (killCount >= (maxEnemySpawns[0] + maxEnemySpawns[1] + enemiesAtStart) && currentEnemies.Count == 0)
        {
            droppedWeapon.transform.position = new UnityEngine.Vector3(lastEnemy.transform.position.x, droppedWeapon.transform.position.y, lastEnemy.transform.position.z);
            StartCoroutine(DropCrossbow());
        }
    }


    private IEnumerator DropCrossbow()
    {
        droppedWeapon.SetActive(true);
        droppedWeapon.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(2);
        droppedWeapon.GetComponent<BoxCollider>().enabled = true;
    }
}
