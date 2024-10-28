using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{
    // this script manages everything related to the skill tree and experience points 

    // TODO check if weapon is melee or ranged before accessing the script when using skill points

    public delegate void OnKillingEnemy(GameObject enemy);
    public static OnKillingEnemy onKillingEnemy;

    [SerializeField] private Slider xpBar;
    [SerializeField] private GameObject skillpointsMainDisplay;
    [SerializeField] private GameObject skillpointsSkillMenu;

    private int totalXP;
    private int totalSkillPoints;

    private int currentLevelXP;
    private int currentSkillPoints;

    [SerializeField] private int neededLevelXP = 100;

    [SerializeField] private int meleeEnemyXP;


    [SerializeField] private GameObject currentWeapon;

    [SerializeField] private GameObject skillPointsOne;
    [SerializeField] private GameObject skillPointsTwo;
    [SerializeField] private GameObject skillPointsThree;

    [SerializeField] private Slider[] skillBarSliders;
    [SerializeField] private GameObject[] skillNameObjects;
    [SerializeField] private GameObject[] skillStatsBasic;
    //[SerializeField] private GameObject[] skillDescriptions;



    void OnEnable()
    {
        onKillingEnemy += GainXP;
        PlayerAttack.onChangingWeapon += DisplayCorrectWeaponStats;

        xpBar.value = 0;
        skillpointsMainDisplay.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
        skillpointsSkillMenu.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
    }


    void OnDisable()
    {
        onKillingEnemy -= GainXP;
        PlayerAttack.onChangingWeapon -= DisplayCorrectWeaponStats;
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

        xpBar.value = 100 / neededLevelXP * currentLevelXP;
        skillpointsMainDisplay.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
        skillpointsSkillMenu.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
    }



    public void AddSkillPoint(int skill)
    {
        if (skill == 0 && currentSkillPoints > 0)
        {
            currentSkillPoints--;
            currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex++;
            skillPointsOne.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex.ToString();
            skillBarSliders[skill].value = currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex;
            if (currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex == 0)
            {
                currentWeapon.GetComponent<MeleeWeapon>().UnlockSkill(skill);
            }
        }

        if (skill == 1 && currentSkillPoints > 0)
        {
            currentSkillPoints--;
            currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex++;
            skillPointsTwo.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex.ToString();
            skillBarSliders[skill].value = currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex;
            if (currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex == 0)
            {
                currentWeapon.GetComponent<MeleeWeapon>().UnlockSkill(skill);
            }
        }

        if (skill == 2 && currentSkillPoints > 0)
        {
            currentSkillPoints--;
            currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex++;
            skillPointsThree.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex.ToString();
            skillBarSliders[skill].value = currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex;
            if (currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex == 0)
            {
                currentWeapon.GetComponent<MeleeWeapon>().UnlockSkill(skill);
            }
        }

        skillpointsMainDisplay.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
        skillpointsSkillMenu.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
    }


    public void SubtractSkillPoints(int skill)
    {
        if (skill == 0 && currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex > 0)
        {
            currentSkillPoints++;
            currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex--;
            skillPointsOne.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex.ToString();
            skillBarSliders[skill].value = currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex;
            if (currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex == 0)
            {
                currentWeapon.GetComponent<MeleeWeapon>().UnlockSkill(skill);
            }
        }

        if (skill == 1 && currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex > 0)
        {
            currentSkillPoints++;
            currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex--;
            skillPointsTwo.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex.ToString();
            skillBarSliders[skill].value = currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex;
            if (currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex == 0)
            {
                currentWeapon.GetComponent<MeleeWeapon>().UnlockSkill(skill);
            }
        }

        if (skill == 2 && currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex > 0)
        {
            currentSkillPoints++;
            currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex--;
            skillPointsThree.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex.ToString();
            skillBarSliders[skill].value = currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex;
            if (currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex == 0)
            {
                currentWeapon.GetComponent<MeleeWeapon>().UnlockSkill(skill);
            }
        }

        skillpointsMainDisplay.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
        skillpointsSkillMenu.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
    }



    private void DisplayCorrectWeaponStats(GameObject weapon)
    {
        currentWeapon = weapon;
        skillPointsOne.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex.ToString();
        skillPointsTwo.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex.ToString();
        skillPointsThree.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex.ToString();

        skillBarSliders[0].value = currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex;
        skillBarSliders[1].value = currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex;
        skillBarSliders[2].value = currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex;

        for (int i = 0; i < skillNameObjects.Length; i++)
        {
            skillNameObjects[i].GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().skillNames[i];
        }

        skillStatsBasic[0].GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().baseDamage.ToString() + " +";
        skillStatsBasic[1].GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().critDamage.ToString() + " +";

        CountRemainingPoints();
        skillpointsMainDisplay.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
        skillpointsSkillMenu.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();

        // change descriptions
        // change add-on things on the bars
    }


    private void CountRemainingPoints()
    {
        int usedSkillPoints = 0;
        foreach (Slider bar in skillBarSliders)
        {
            usedSkillPoints += (int)bar.value;
        }
        currentSkillPoints = totalSkillPoints - usedSkillPoints;
    }

}
