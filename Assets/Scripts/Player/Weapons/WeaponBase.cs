using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{

    public bool isMeleeWeapon;

    public float baseDamage;
    public float critDamage = 10;
    public float baseCritChance = 0.1f;
    public float currentCritChance;
    public bool canCrit = true;


    public string[] skillNames;
    public string[] skillDescriptions;


    [Header("Values for scripts (just ignore)")]

    public float currentDamage = 10;

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

    private delegate void UsingAttackOne(GameObject enemy);
    private static UsingAttackOne usingAttackOne;



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && isMeleeWeapon)
        {
            Debug.Log("STRIKE");
            if (attackIndex == 0)
            {
                if (unlockedPassiveSkill)
                {
                    currentCritChance = baseCritChance * passiveSkill[passiveSkillIndex];
                }
                else
                {
                    currentCritChance = baseCritChance;
                }
                if (Random.value < currentCritChance && canCrit)
                {
                    Debug.Log("critical attack");
                    other.GetComponent<EnemyAI>().TakeDamage(currentDamage + critDamage);
                    PlayerAttack.onPlayerAttack?.Invoke(2);
                    StartCoroutine(CritCooldown());
                }
                else
                {
                    Debug.Log("normal attack");
                    other.GetComponent<EnemyAI>().TakeDamage(currentDamage);
                }
            }
            if (attackIndex == 1)
            {
                PlayerAttack.onUsingAttackOne?.Invoke(other.gameObject);
            }
            if (attackIndex == 2)
            {
                PlayerAttack.onUsingAttackTwo?.Invoke(other.gameObject);
            }
        }
    }



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



    public IEnumerator CritCooldown()
    {
        canCrit = false;
        yield return new WaitForSeconds(5);
        canCrit = true;
    }

}
