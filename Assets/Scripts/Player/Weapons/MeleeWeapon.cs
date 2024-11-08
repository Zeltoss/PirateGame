using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{

    public float baseDamage;
    public float critDamage;

    private float baseDamageAdded;
    private float critDamageAdded;


    public string[] skillNames;
    public string[] skillDescriptions;

    [SerializeField] private GameObject[] skillIcons;


    [Header("Values for scripts (just ignore)")]

    public float currentDamage;

    // bleeding
    private float[] skillOne;
    // whirling
    private float[] skillTwo;
    // precision (raise crit chance)
    private float[] passiveSkill;

    public int skillOneIndex;
    public int skillTwoIndex;
    public int passiveSkillIndex;

    public bool unlockedSkillOne;
    public bool unlockedSkillTwo;
    public bool unlockedPassiveSkill;

    private bool[] unlockedSkills = new bool[3];



    void Start()
    {
        foreach (GameObject skill in skillIcons)
        {
            skill.SetActive(false);
        }

        unlockedSkills[0] = unlockedSkillOne;
        unlockedSkills[1] = unlockedSkillTwo;
        unlockedSkills[2] = unlockedPassiveSkill;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyAI>().TakeDamage(currentDamage);
            Debug.Log("STRIKE");
        }
    }



    public void UnlockSkill(int index)
    {
        unlockedSkills[index] = !unlockedSkills[index];
        //skillIcons[index].SetActive(!skillIcons[index].activeSelf);
    }
}
