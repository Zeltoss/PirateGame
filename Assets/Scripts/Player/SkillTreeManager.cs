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
    [SerializeField] private GameObject[] milestoneOne;
    [SerializeField] private GameObject[] milestoneTwo;
    [SerializeField] private GameObject[] milestoneThree;

    [SerializeField] private GameObject[] skillNameObjects;
    [SerializeField] private GameObject[] skillStatsBasic;
    [SerializeField] private GameObject[] skillStatsAdded;
    //[SerializeField] private GameObject[] skillDescriptions;

    [SerializeField] private GameObject[] skillsUIButtons;



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


    void Start()
    {
        foreach (GameObject button in skillsUIButtons)
        {
            button.SetActive(false);
        }
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
            if (currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex == 0)
            {
                currentWeapon.GetComponent<MeleeWeapon>().UnlockSkill(skill);
                skillsUIButtons[skill].SetActive(true);
                milestoneOne[skill].SetActive(false);
            }
            currentSkillPoints--;
            currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex++;
            skillPointsOne.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex.ToString();
            skillBarSliders[skill].value = currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex;
            if (currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex == 4)
            {
                milestoneTwo[skill].SetActive(false);
            }
            if (currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex == 7)
            {
                milestoneThree[skill].SetActive(false);
            }
        }

        if (skill == 1 && currentSkillPoints > 0)
        {
            if (currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex == 0)
            {
                currentWeapon.GetComponent<MeleeWeapon>().UnlockSkill(skill);
                skillsUIButtons[skill].SetActive(true);
                milestoneOne[skill].SetActive(false);
            }
            currentSkillPoints--;
            currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex++;
            skillPointsTwo.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex.ToString();
            skillBarSliders[skill].value = currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex;
            if (currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex == 4)
            {
                milestoneTwo[skill].SetActive(false);
            }
            if (currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex == 7)
            {
                milestoneThree[skill].SetActive(false);
            }
        }

        if (skill == 2 && currentSkillPoints > 0)
        {
            if (currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex == 0)
            {
                currentWeapon.GetComponent<MeleeWeapon>().UnlockSkill(skill);
                skillsUIButtons[skill].SetActive(true);
                milestoneOne[skill].SetActive(false);
            }
            currentSkillPoints--;
            currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex++;
            skillPointsThree.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex.ToString();
            skillBarSliders[skill].value = currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex;
            if (currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex == 4)
            {
                milestoneTwo[skill].SetActive(false);
            }
            if (currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex == 7)
            {
                milestoneThree[skill].SetActive(false);
            }
        }

        skillpointsMainDisplay.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
        skillpointsSkillMenu.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
    }


    public void SubtractSkillPoints(int skill)
    {
        if (skill == 0 && currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex > 0)
        {
            if (currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex == 4)
            {
                milestoneTwo[skill].SetActive(true);
            }
            if (currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex == 7)
            {
                milestoneThree[skill].SetActive(true);
            }
            currentSkillPoints++;
            currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex--;
            skillPointsOne.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex.ToString();
            skillBarSliders[skill].value = currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex;
            if (currentWeapon.GetComponent<MeleeWeapon>().skillOneIndex == 0)
            {
                currentWeapon.GetComponent<MeleeWeapon>().UnlockSkill(skill);
                skillsUIButtons[skill].SetActive(false);
                milestoneOne[skill].SetActive(true);
            }
        }

        if (skill == 1 && currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex > 0)
        {
            if (currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex == 4)
            {
                milestoneTwo[skill].SetActive(true);
            }
            if (currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex == 7)
            {
                milestoneThree[skill].SetActive(true);
            }
            currentSkillPoints++;
            currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex--;
            skillPointsTwo.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex.ToString();
            skillBarSliders[skill].value = currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex;
            if (currentWeapon.GetComponent<MeleeWeapon>().skillTwoIndex == 0)
            {
                currentWeapon.GetComponent<MeleeWeapon>().UnlockSkill(skill);
                skillsUIButtons[skill].SetActive(false);
                milestoneOne[skill].SetActive(true);
            }
        }

        if (skill == 2 && currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex > 0)
        {
            if (currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex == 4)
            {
                milestoneTwo[skill].SetActive(true);
            }
            if (currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex == 7)
            {
                milestoneThree[skill].SetActive(true);
            }
            currentSkillPoints++;
            currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex--;
            skillPointsThree.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex.ToString();
            skillBarSliders[skill].value = currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex;
            if (currentWeapon.GetComponent<MeleeWeapon>().passiveSkillIndex == 0)
            {
                currentWeapon.GetComponent<MeleeWeapon>().UnlockSkill(skill);
                skillsUIButtons[skill].SetActive(false);
                milestoneOne[skill].SetActive(true);
            }
        }

        skillpointsMainDisplay.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
        skillpointsSkillMenu.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
    }



    private void DisplayCorrectWeaponStats(GameObject weapon)
    {
        currentWeapon = weapon;
        Debug.Log(currentWeapon);
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

        skillStatsBasic[0].GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().baseDamage.ToString();
        skillStatsBasic[1].GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<MeleeWeapon>().critDamage.ToString();

        CountRemainingPoints();
        skillpointsMainDisplay.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
        skillpointsSkillMenu.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();

        for (int i = 0; i < skillBarSliders.Length; i++)
        {
            if (skillBarSliders[i].value == 0)
            {
                milestoneOne[i].SetActive(true);
                milestoneTwo[i].SetActive(true);
                milestoneThree[i].SetActive(true);
                skillsUIButtons[i].SetActive(false);
            }
            else
            {
                skillsUIButtons[i].SetActive(true);
            }
            if (skillBarSliders[i].value > 0)
            {
                milestoneOne[i].SetActive(false);
                milestoneTwo[i].SetActive(true);
                milestoneThree[i].SetActive(true);
            }
            else if (skillBarSliders[i].value >= 4)
            {
                milestoneTwo[i].SetActive(false);
                milestoneThree[i].SetActive(true);
            }
            else if (skillBarSliders[i].value == 7)
            {
                milestoneThree[i].SetActive(false);
            }
        }
        // change descriptions
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
