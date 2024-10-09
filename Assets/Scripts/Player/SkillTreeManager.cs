using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{
    // this script manages everything related to the skill tree and experience points 


    public delegate void OnKillingEnemy(GameObject enemy);
    public static OnKillingEnemy onKillingEnemy;

    //[SerializeField] private Slider XPbar;

    private int totalXP;
    private int totalSkillPoints;

    private int currentLevelXP;
    private int currentSkillPoints;

    private int neededLevelXP = 100;

    [SerializeField] private int meleeEnemyXP;


    void OnEnable()
    {
        onKillingEnemy += GainXP;
    }


    void OnDisable()
    {
        onKillingEnemy -= GainXP;
    }



    private void GainXP(GameObject enemy)
    {
        if (enemy.GetComponent<EnemyAI>().enemyType.ToString() == "melee")
        {
            totalXP += meleeEnemyXP;
            currentLevelXP += meleeEnemyXP;
        }

        while (currentLevelXP >= neededLevelXP)
        {
            totalSkillPoints++;
            currentSkillPoints++;
            currentLevelXP -= neededLevelXP;
        }

        //XPbar.value = 1 / neededLevelXP * currentLevelXP;
    }
}
