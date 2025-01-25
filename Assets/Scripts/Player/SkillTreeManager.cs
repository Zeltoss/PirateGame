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
    [SerializeField] private GameObject skillpointsMainDisplay;
    [SerializeField] private GameObject skillpointsSkillMenu;

    private int totalXP;
    private int totalSkillPoints;

    private int currentLevelXP;
    private int currentSkillPoints;

    [SerializeField] private int neededLevelXP = 100;

    [SerializeField] private int meleeEnemyXP;
    [SerializeField] private int rapierEnemyXP;

    [SerializeField] private GameObject currentWeapon;

    [SerializeField] private GameObject skillPointsOne;
    [SerializeField] private GameObject skillPointsTwo;
    [SerializeField] private GameObject skillPointsThree;

    [SerializeField] private Slider[] skillBarSliders;
    [SerializeField] private GameObject[] milestoneOne_inactive;
    [SerializeField] private GameObject[] milestoneTwo_inactive;
    [SerializeField] private GameObject[] milestoneThree_inactive;
    [SerializeField] private GameObject[] milestoneOne;
    [SerializeField] private GameObject[] milestoneTwo;
    [SerializeField] private GameObject[] milestoneThree;

    [SerializeField] private GameObject[] skillNameObjects;
    [SerializeField] private GameObject[] skillStatsBasic;
    [SerializeField] private GameObject[] skillStatsAdded;
    [SerializeField] private GameObject thirdStat;
    [SerializeField] private GameObject[] skillDescriptionTitles1;
    [SerializeField] private GameObject[] skillDescriptionTitles2;
    [SerializeField] private GameObject[] skillDescriptionTitles3;
    [SerializeField] private GameObject[] skillDescriptions1;
    [SerializeField] private GameObject[] skillDescriptions2;
    [SerializeField] private GameObject[] skillDescriptions3;

    [SerializeField] private GameObject[] skillsUIButtons;
    [SerializeField] private GameObject[] UIButtons_icons;

    [SerializeField] private GameObject skillpointMessage;



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
        skillpointMessage.SetActive(false);
    }


    private void GainXP(GameObject enemy)
    {
        if (enemy.GetComponent<EnemyAI>().enemyType.ToString() == "melee")
        {
            totalXP += meleeEnemyXP;
            currentLevelXP += meleeEnemyXP;
        }
        if (enemy.GetComponent<EnemyAI>().enemyType.ToString() == "rapier")
        {
            totalXP += rapierEnemyXP;
            currentLevelXP += rapierEnemyXP;
        }

        while (currentLevelXP >= neededLevelXP)
        {
            totalSkillPoints++;
            currentSkillPoints++;
            currentLevelXP -= neededLevelXP;
            skillpointMessage.SetActive(true);
        }

        xpBar.value = 100 / neededLevelXP * currentLevelXP;
        skillpointsMainDisplay.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
        skillpointsSkillMenu.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
    }



    public void AddSkillPoint(int skill)
    {
        if (currentSkillPoints > 0)
        {
            if (skill == 0 && currentWeapon.GetComponent<WeaponBase>().skillOneIndex < (currentWeapon.GetComponent<WeaponBase>().skillOne.Length - 1))
            {
                if (currentWeapon.GetComponent<WeaponBase>().skillOneIndex == 0)
                {
                    currentWeapon.GetComponent<WeaponBase>().UnlockSkill(skill);
                    skillsUIButtons[skill].SetActive(true);
                    milestoneOne_inactive[skill].SetActive(false);
                }
                currentWeapon.GetComponent<WeaponBase>().skillOneIndex++;
                currentSkillPoints--;
                skillPointsOne.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().skillOneIndex.ToString();
                skillBarSliders[skill].value = currentWeapon.GetComponent<WeaponBase>().skillOneIndex;
                if (currentWeapon.GetComponent<WeaponBase>().skillOneIndex == 4)
                {
                    milestoneTwo_inactive[skill].SetActive(false);
                }
                if (currentWeapon.GetComponent<WeaponBase>().skillOneIndex == 7)
                {
                    milestoneThree_inactive[skill].SetActive(false);
                }
            }

            if (skill == 1 && currentWeapon.GetComponent<WeaponBase>().skillTwoIndex < (currentWeapon.GetComponent<WeaponBase>().skillTwo.Length - 1))
            {
                if (currentWeapon.GetComponent<WeaponBase>().skillTwoIndex == 0)
                {
                    currentWeapon.GetComponent<WeaponBase>().UnlockSkill(skill);
                    skillsUIButtons[skill].SetActive(true);
                    milestoneOne_inactive[skill].SetActive(false);
                }
                currentWeapon.GetComponent<WeaponBase>().skillTwoIndex++;
                currentSkillPoints--;
                skillPointsTwo.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().skillTwoIndex.ToString();
                skillBarSliders[skill].value = currentWeapon.GetComponent<WeaponBase>().skillTwoIndex;
                if (currentWeapon.GetComponent<WeaponBase>().skillTwoIndex == 4)
                {
                    milestoneTwo_inactive[skill].SetActive(false);
                }
                if (currentWeapon.GetComponent<WeaponBase>().skillTwoIndex == 7)
                {
                    milestoneThree_inactive[skill].SetActive(false);
                }
            }

            if (skill == 2 && currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex < (currentWeapon.GetComponent<WeaponBase>().passiveSkill.Length - 1))
            {
                if (currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex == 0)
                {
                    currentWeapon.GetComponent<WeaponBase>().UnlockSkill(skill);
                    skillsUIButtons[skill].SetActive(true);
                    milestoneOne_inactive[skill].SetActive(false);
                }
                currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex++;
                currentSkillPoints--;
                skillPointsThree.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex.ToString();
                skillBarSliders[skill].value = currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex;
                if (currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex == 4)
                {
                    milestoneTwo_inactive[skill].SetActive(false);
                }
                if (currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex == 7)
                {
                    milestoneThree_inactive[skill].SetActive(false);
                }
            }
        }

        skillpointsMainDisplay.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
        skillpointsSkillMenu.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();

        DisplayCorrectWeaponStats();
        ChangeIconsInMainUI();

        if (currentSkillPoints == 0)
        {
            skillpointMessage.SetActive(false);
        }
        else
        {
            skillpointMessage.SetActive(true);
        }
    }


    public void SubtractSkillPoints(int skill)
    {
        if (skill == 0)
        {
            if (currentWeapon.GetComponent<WeaponBase>().skillOneIndex > 0)
            {
                if (currentWeapon.GetComponent<WeaponBase>().skillOneIndex == 4)
                {
                    milestoneTwo_inactive[skill].SetActive(true);
                }
                if (currentWeapon.GetComponent<WeaponBase>().skillOneIndex == 7)
                {
                    milestoneThree_inactive[skill].SetActive(true);
                }
                currentSkillPoints++;
                currentWeapon.GetComponent<WeaponBase>().skillOneIndex--;
                skillPointsOne.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().skillOneIndex.ToString();
                skillBarSliders[skill].value = currentWeapon.GetComponent<WeaponBase>().skillOneIndex;
                if (currentWeapon.GetComponent<WeaponBase>().skillOneIndex == 0)
                {
                    currentWeapon.GetComponent<WeaponBase>().UnlockSkill(skill);
                    skillsUIButtons[skill].SetActive(false);
                    milestoneOne_inactive[skill].SetActive(true);
                }
            }
        }

        if (skill == 1)
        {
            if (currentWeapon.GetComponent<WeaponBase>().skillTwoIndex > 0)
            {
                if (currentWeapon.GetComponent<WeaponBase>().skillTwoIndex == 4)
                {
                    milestoneTwo_inactive[skill].SetActive(true);
                }
                if (currentWeapon.GetComponent<WeaponBase>().skillTwoIndex == 7)
                {
                    milestoneThree_inactive[skill].SetActive(true);
                }
                currentSkillPoints++;
                currentWeapon.GetComponent<WeaponBase>().skillTwoIndex--;
                skillPointsTwo.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().skillTwoIndex.ToString();
                skillBarSliders[skill].value = currentWeapon.GetComponent<WeaponBase>().skillTwoIndex;
                if (currentWeapon.GetComponent<WeaponBase>().skillTwoIndex == 0)
                {
                    currentWeapon.GetComponent<WeaponBase>().UnlockSkill(skill);
                    skillsUIButtons[skill].SetActive(false);
                    milestoneOne_inactive[skill].SetActive(true);
                }
            }
        }

        if (skill == 2)
        {
            if (currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex > 0)
            {
                if (currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex == 4)
                {
                    milestoneTwo_inactive[skill].SetActive(true);
                }
                if (currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex == 7)
                {
                    milestoneThree_inactive[skill].SetActive(true);
                }
                currentSkillPoints++;
                currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex--;
                skillPointsThree.GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex.ToString();
                skillBarSliders[skill].value = currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex;
                if (currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex == 0)
                {
                    currentWeapon.GetComponent<WeaponBase>().UnlockSkill(skill);
                    skillsUIButtons[skill].SetActive(false);
                    milestoneOne_inactive[skill].SetActive(true);
                }
            }
        }

        skillpointsMainDisplay.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
        skillpointsSkillMenu.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();

        DisplayCorrectWeaponStats();
        ChangeIconsInMainUI();

        if (currentSkillPoints > 0)
        {
            skillpointMessage.SetActive(true);
        }
        else
        {
            skillpointMessage.SetActive(false);
        }
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
                milestoneOne_inactive[i].SetActive(true);
                milestoneTwo_inactive[i].SetActive(true);
                milestoneThree_inactive[i].SetActive(true);
                skillsUIButtons[i].SetActive(false);
            }
            else
            {
                milestoneOne_inactive[i].SetActive(false);
                skillsUIButtons[i].SetActive(true);
            }
            if (skillBarSliders[i].value >= 4)
            {
                milestoneTwo_inactive[i].SetActive(false);
            }
            if (skillBarSliders[i].value == 7)
            {
                milestoneThree_inactive[i].SetActive(false);
            }
        }

        // skill points
        CountRemainingPoints();
        skillpointsMainDisplay.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();
        skillpointsSkillMenu.GetComponent<TextMeshProUGUI>().text = currentSkillPoints.ToString();

        // skill names
        for (int i = 0; i < skillNameObjects.Length; i++)
        {
            skillNameObjects[i].GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().skillNames[i];
        }
        // skill titles and descriptions
        for (int i = 0; i < skillDescriptionTitles1.Length; i++)
        {
            skillDescriptionTitles1[i].GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().skillNames[0];
            skillDescriptionTitles2[i].GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().skillNames[1];
            skillDescriptionTitles3[i].GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().skillNames[2];
            skillDescriptions1[i].GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().skillDescriptions[0];
            skillDescriptions2[i].GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().skillDescriptions[1];
            skillDescriptions3[i].GetComponent<TextMeshProUGUI>().text = currentWeapon.GetComponent<WeaponBase>().skillDescriptions[2];
        }
        // skill icons 
        for (int i = 0; i < milestoneOne.Length; i++)
        {
            milestoneOne[i].GetComponent<Image>().sprite = currentWeapon.GetComponent<WeaponBase>().icons_milestoneOne[i];
            milestoneTwo[i].GetComponent<Image>().sprite = currentWeapon.GetComponent<WeaponBase>().icons_milestoneTwo[i];
            milestoneThree[i].GetComponent<Image>().sprite = currentWeapon.GetComponent<WeaponBase>().icons_milestoneThree[i];
        }

        DisplayCorrectWeaponStats();
        ChangeIconsInMainUI();

        if (currentSkillPoints > 0)
        {
            skillpointMessage.SetActive(true);
        }
        else
        {
            skillpointMessage.SetActive(false);
        }
    }


    private void ChangeIconsInMainUI()
    {
        // skill icons in the main UI
        UIButtons_icons[0].GetComponent<Image>().sprite = currentWeapon.GetComponent<WeaponBase>().icons_milestoneOne[0];
        if (currentWeapon.GetComponent<WeaponBase>().skillOneIndex >= 4)
        {
            UIButtons_icons[0].GetComponent<Image>().sprite = currentWeapon.GetComponent<WeaponBase>().icons_milestoneTwo[0];
        }
        if (currentWeapon.GetComponent<WeaponBase>().skillOneIndex == 7)
        {
            UIButtons_icons[0].GetComponent<Image>().sprite = currentWeapon.GetComponent<WeaponBase>().icons_milestoneThree[0];
        }

        UIButtons_icons[1].GetComponent<Image>().sprite = currentWeapon.GetComponent<WeaponBase>().icons_milestoneOne[1];
        if (currentWeapon.GetComponent<WeaponBase>().skillTwoIndex >= 4)
        {
            UIButtons_icons[1].GetComponent<Image>().sprite = currentWeapon.GetComponent<WeaponBase>().icons_milestoneTwo[1];
        }
        if (currentWeapon.GetComponent<WeaponBase>().skillTwoIndex == 7)
        {
            UIButtons_icons[1].GetComponent<Image>().sprite = currentWeapon.GetComponent<WeaponBase>().icons_milestoneThree[1];
        }

        UIButtons_icons[2].GetComponent<Image>().sprite = currentWeapon.GetComponent<WeaponBase>().icons_milestoneOne[2];
        if (currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex >= 4)
        {
            UIButtons_icons[2].GetComponent<Image>().sprite = currentWeapon.GetComponent<WeaponBase>().icons_milestoneTwo[2];
        }
        if (currentWeapon.GetComponent<WeaponBase>().passiveSkillIndex == 7)
        {
            UIButtons_icons[2].GetComponent<Image>().sprite = currentWeapon.GetComponent<WeaponBase>().icons_milestoneThree[2];
        }

        if (currentWeapon.name == "Crossbow")
        {
            UIButtons_icons[3].SetActive(true);
        }
        else
        {
            UIButtons_icons[3].SetActive(false);
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

        // stat for headshots (crossbow specific)
        if (currentWeapon.name == "Crossbow" && currentWeapon.GetComponent<WeaponBase>().unlockedSkillTwo)
        {
            thirdStat.SetActive(true);
            skillStatsAdded[2].GetComponent<TextMeshProUGUI>().text = (currentWeapon.GetComponent<WeaponBase>().skillTwo[currentWeapon.GetComponent<WeaponBase>().skillTwoIndex] * 100).ToString() + "%";
        }
        else
        {
            thirdStat.SetActive(false);
            skillStatsAdded[2].GetComponent<TextMeshProUGUI>().text = "";
        }
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
