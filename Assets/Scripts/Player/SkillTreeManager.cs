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
    [SerializeField] private GameObject[] skillDescriptions;

    [SerializeField] private GameObject[] skillsUIButtons;



    void OnEnable()
    {
        onKillingEnemy += GainXP;
        PlayerAttack.onChangingWeapon += DisplayCorrectWeapon;

        xpBar.value = 0;
        skillpointsMainDisplay.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
        skillpointsSkillMenu.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
    }


    void OnDisable()
    {
        onKillingEnemy -= GainXP;
        PlayerAttack.onChangingWeapon -= DisplayCorrectWeapon;
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
            if (currentWeapon.GetComponent<WeaponBase>().skillOneIndex == 0)
            {
                currentWeapon.GetComponent<WeaponBase>().UnlockSkill(skill);
                skillsUIButtons[skill].SetActive(true);
                milestoneOne[skill].SetActive(false);
            }
            currentSkillPoints--;
            currentWeapon.GetComponent<WeaponBase>().skillOneIndex++;
            skillPointsOne.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().skillOneIndex.ToString();
            skillBarSliders[skill].value = currentWeapon.GetComponent<WeaponBase>().skillOneIndex;
            if (currentWeapon.GetComponent<WeaponBase>().skillOneIndex == 4)
            {
                milestoneTwo[skill].SetActive(false);
            }
            if (currentWeapon.GetComponent<WeaponBase>().skillOneIndex == 7)
            {
                milestoneThree[skill].SetActive(false);
            }
        }

        if (skill == 1 && currentSkillPoints > 0)
        {
            if (currentWeapon.GetComponent<WeaponBase>().skillTwoIndex == 0)
            {
                currentWeapon.GetComponent<WeaponBase>().UnlockSkill(skill);
                skillsUIButtons[skill].SetActive(true);
                milestoneOne[skill].SetActive(false);
            }
            currentSkillPoints--;
            currentWeapon.GetComponent<WeaponBase>().skillTwoIndex++;
            skillPointsTwo.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().skillTwoIndex.ToString();
            skillBarSliders[skill].value = currentWeapon.GetComponent<WeaponBase>().skillTwoIndex;
            if (currentWeapon.GetComponent<WeaponBase>().skillTwoIndex == 4)
            {
                milestoneTwo[skill].SetActive(false);
            }
            if (currentWeapon.GetComponent<WeaponBase>().skillTwoIndex == 7)
            {
                milestoneThree[skill].SetActive(false);
            }
        }

        if (skill == 2 && currentSkillPoints > 0)
        {
            if (currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex == 0)
            {
                currentWeapon.GetComponent<WeaponBase>().UnlockSkill(skill);
                skillsUIButtons[skill].SetActive(true);
                milestoneOne[skill].SetActive(false);
            }
            currentSkillPoints--;
            currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex++;
            skillPointsThree.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex.ToString();
            skillBarSliders[skill].value = currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex;
            if (currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex == 4)
            {
                milestoneTwo[skill].SetActive(false);
            }
            if (currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex == 7)
            {
                milestoneThree[skill].SetActive(false);
            }
        }

        skillpointsMainDisplay.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
        skillpointsSkillMenu.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();

        DisplayCorrectWeaponStats();
    }


    public void SubtractSkillPoints(int skill)
    {
        if (skill == 0 && currentWeapon.GetComponent<WeaponBase>().skillOneIndex > 0)
        {
            if (currentWeapon.GetComponent<WeaponBase>().skillOneIndex == 4)
            {
                milestoneTwo[skill].SetActive(true);
            }
            if (currentWeapon.GetComponent<WeaponBase>().skillOneIndex == 7)
            {
                milestoneThree[skill].SetActive(true);
            }
            currentSkillPoints++;
            currentWeapon.GetComponent<WeaponBase>().skillOneIndex--;
            skillPointsOne.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().skillOneIndex.ToString();
            skillBarSliders[skill].value = currentWeapon.GetComponent<WeaponBase>().skillOneIndex;
            if (currentWeapon.GetComponent<WeaponBase>().skillOneIndex == 0)
            {
                currentWeapon.GetComponent<WeaponBase>().UnlockSkill(skill);
                skillsUIButtons[skill].SetActive(false);
                milestoneOne[skill].SetActive(true);
            }
        }

        if (skill == 1 && currentWeapon.GetComponent<WeaponBase>().skillTwoIndex > 0)
        {
            if (currentWeapon.GetComponent<WeaponBase>().skillTwoIndex == 4)
            {
                milestoneTwo[skill].SetActive(true);
            }
            if (currentWeapon.GetComponent<WeaponBase>().skillTwoIndex == 7)
            {
                milestoneThree[skill].SetActive(true);
            }
            currentSkillPoints++;
            currentWeapon.GetComponent<WeaponBase>().skillTwoIndex--;
            skillPointsTwo.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().skillTwoIndex.ToString();
            skillBarSliders[skill].value = currentWeapon.GetComponent<WeaponBase>().skillTwoIndex;
            if (currentWeapon.GetComponent<WeaponBase>().skillTwoIndex == 0)
            {
                currentWeapon.GetComponent<WeaponBase>().UnlockSkill(skill);
                skillsUIButtons[skill].SetActive(false);
                milestoneOne[skill].SetActive(true);
            }
        }

        if (skill == 2 && currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex > 0)
        {
            if (currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex == 4)
            {
                milestoneTwo[skill].SetActive(true);
            }
            if (currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex == 7)
            {
                milestoneThree[skill].SetActive(true);
            }
            currentSkillPoints++;
            currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex--;
            skillPointsThree.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex.ToString();
            skillBarSliders[skill].value = currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex;
            if (currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex == 0)
            {
                currentWeapon.GetComponent<WeaponBase>().UnlockSkill(skill);
                skillsUIButtons[skill].SetActive(false);
                milestoneOne[skill].SetActive(true);
            }
        }

        skillpointsMainDisplay.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
        skillpointsSkillMenu.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();

        DisplayCorrectWeaponStats();
    }



    private void DisplayCorrectWeapon(GameObject weapon)
    {
        currentWeapon = weapon;

        // skill bars 
        skillPointsOne.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().skillOneIndex.ToString();
        skillPointsTwo.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().skillTwoIndex.ToString();
        skillPointsThree.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex.ToString();

        skillBarSliders[0].value = currentWeapon.GetComponent<WeaponBase>().skillOneIndex;
        skillBarSliders[1].value = currentWeapon.GetComponent<WeaponBase>().skillTwoIndex;
        skillBarSliders[2].value = currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex;

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

        // weapon stats
        DisplayCorrectWeaponStats();

        // skill points
        CountRemainingPoints();
        skillpointsMainDisplay.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
        skillpointsSkillMenu.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();

        // skill names
        for (int i = 0; i < skillNameObjects.Length; i++)
        {
            skillNameObjects[i].GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().skillNames[i];
        }
        // skill descriptions
        for (int i = 0; i < skillDescriptions.Length; i++)
        {
            skillDescriptions[i].GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().skillDescriptions[i];
        }
    }


    private void DisplayCorrectWeaponStats()
    {
        skillStatsBasic[0].GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().baseDamage.ToString();
        float crits = currentWeapon.GetComponent<WeaponBase>().baseCritChance;
        skillStatsBasic[1].GetComponent<TextMeshProUGUI>().text = (crits * 100).ToString() + "%";

        skillStatsAdded[0].GetComponent<TextMeshProUGUI>().text = "+ 0";
        float additionalCrit = currentWeapon.GetComponent<WeaponBase>().passiveSkill[currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex];
        skillStatsAdded[1].GetComponent<TextMeshProUGUI>().text = "+ " + (additionalCrit * 100).ToString() + "%";
        skillStatsAdded[1].GetComponent<TextMeshProUGUI>().text = "+ " + (((crits * additionalCrit) - crits) * 100).ToString() + "%";
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
