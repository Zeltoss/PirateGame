using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{
    // this script manages everything related to the skill tree and experience points 


    public delegate void OnKillingEnemy(GameObject enemy);
    public static OnKillingEnemy onKillingEnemy;

    [SerializeField] private Slider xpBar;
    [SerializeField] private GameObject skillpointsDisplay;

    private int totalXP;
    private int totalSkillPoints;

    private int currentLevelXP;
    private int currentSkillPoints;

    [SerializeField] private int neededLevelXP = 100;

    [SerializeField] private int meleeEnemyXP;

    // GameObject for currentWeapon, that tells the player when the weapon gets swapped?

    [SerializeField] private GameObject currentWeapon;

    [SerializeField] private GameObject skillPointsOne;
    [SerializeField] private GameObject skillPointsTwo;
    [SerializeField] private GameObject skillPointsThree;

    [SerializeField] private Slider[] skillBarSliders;





    void OnEnable()
    {
        onKillingEnemy += GainXP;

        xpBar.value = 0;
        skillpointsDisplay.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
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

        xpBar.value = 100 / neededLevelXP * currentLevelXP;
        skillpointsDisplay.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
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

        skillpointsDisplay.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
    }


    public void SubtractSkillPoints(int skill)
    {
        if (skill == 0 && currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex > 0)
        {
            currentSkillPoints++;
            currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex--;
            skillPointsOne.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex.ToString();
            skillBarSliders[skill].value = currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex;
            if (currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex == 0)
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
            if (currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex == 0)
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

        skillpointsDisplay.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
    }

}
