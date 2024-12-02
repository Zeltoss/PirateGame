using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{

    public float baseDamage;
    public float critDamage = 10;
    public float baseCritChance = 0.1f;
    public float currentCritChance;
    private bool canCrit = true;


    public string[] skillNames;
    public string[] skillDescriptions;

    [SerializeField] private GameObject[] skillIcons;


    [Header("Values for scripts (just ignore)")]

    public float currentDamage = 10;

    // bleeding
    public float[] skillOne;
    // whirling
    public float[] skillTwo;
    // precision (raise crit chance)
    public float[] passiveSkill;

    public int skillOneIndex;
    public int skillTwoIndex;
    public int passiveSkillIndex;

    public bool unlockedSkillOne;
    public bool unlockedSkillTwo;
    public bool unlockedPassiveSkill;

    public int attackIndex;

    [SerializeField] private GameObject whirlingHitbox;



    void Start()
    {
        foreach (GameObject skill in skillIcons)
        {
            skill.SetActive(false);
        }

        whirlingHitbox.SetActive(false);
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("STRIKE");
            if (attackIndex == 0)
            {
                currentCritChance = baseCritChance * passiveSkill[passiveSkillIndex];
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
                Debug.Log("used attack two");
                other.GetComponent<EnemyAI>().TakeDamage(currentDamage);
                other.GetComponent<EnemyAI>().TakeBleedingDamage(skillOne[skillOneIndex]);
                PlayerAttack.onPlayerAttack?.Invoke(0);
            }
            if (attackIndex == 2)
            {
                PlayerAttack.onPlayerAttack?.Invoke(1);
                StartCoroutine(WhirlingCooldown());
                // change position of collider/activate additional box collider
            }
            // maybe call another event that the weapon specific script reacts to? (once the crossbow gets activated)
            // maybe one for each attack?
        }
    }



    public void UnlockSkill(int index)
    {
        //skillIcons[index].SetActive(!skillIcons[index].activeSelf);
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



    private IEnumerator CritCooldown()
    {
        canCrit = false;
        yield return new WaitForSeconds(5);
        canCrit = true;
    }



    private IEnumerator WhirlingCooldown()
    {
        whirlingHitbox.SetActive(true);
        whirlingHitbox.GetComponent<WhirlingAttack>().whirlingDamage = skillTwo[skillTwoIndex];
        yield return new WaitForSeconds(0.2f);
        whirlingHitbox.SetActive(false);
    }

}
