using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    private float baseDamage;

    private float[] skillOne;
    private float[] skillTwo;
    private float[] skillThree;

    public float skillOneIndex;
    public float skillTwoIndex;
    public float skillThreeIndex;

    public List<GameObject> enemiesInRange = new List<GameObject>();


    void OnEnable()
    {
        PlayerAttack.onPlayerAttack += SendEnemyList;
        SkillTreeManager.onKillingEnemy += RemoveKilledEnemy;
    }

    void OnDisable()
    {
        PlayerAttack.onPlayerAttack -= SendEnemyList;
        SkillTreeManager.onKillingEnemy -= RemoveKilledEnemy;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemiesInRange.Add(other.gameObject);
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }



    private void SendEnemyList()
    {
        if (enemiesInRange.Count != 0)
        {
            PlayerAttack.onHittingEnemy?.Invoke(enemiesInRange);
        }
        else
        {
            Debug.Log("no enemies to hit");
        }
    }



    private void RemoveKilledEnemy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }
}
