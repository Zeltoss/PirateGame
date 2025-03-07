using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    // this script is a base for all weapons which holds all the attack specific values, names and descriptions

    public bool isMeleeWeapon;

    public float baseDamage;
    public float critDamage = 10;
    public float baseCritChance = 0.1f;
    public float currentDamage = 10;

    public string[] skillNames;
    public string[] skillDescriptions;

    public float[] skillOne;
    public float[] skillTwo;
    public float[] passiveSkill;

    public Sprite[] icons_milestoneOne;
    public Sprite[] icons_milestoneTwo;
    public Sprite[] icons_milestoneThree;

    public int skillOneIndex;
    public int skillTwoIndex;
    public int passiveSkillIndex;

    public bool unlockedSkillOne;
    public bool unlockedSkillTwo;
    public bool unlockedPassiveSkill;

    public int attackIndex;



    public void UnlockSkill(int index)
    {
        if (index == 0)
        {
            unlockedSkillOne = !unlockedSkillOne;
        }
        if (index == 1)
        {
            unlockedSkillTwo = !unlockedSkillTwo;
        }
        if (index == 2)
        {
            unlockedPassiveSkill = !unlockedPassiveSkill;
        }
    }

}
