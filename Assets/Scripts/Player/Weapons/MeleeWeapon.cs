using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{

    public float damage;
    // damage for the different abilities?

    private float[] skillOne;
    private float[] skillTwo;
    private float[] passiveSkill;

    public int skillOneIndex;
    public int skillTwoIndex;
    public int passiveSkillIndex;

    public bool unlockedSkillOne;
    public bool unlockedSkillTwo;
    public bool unlockedPassiveSkill;

    public bool[] unlockedSkills;

    [SerializeField] private GameObject[] skillIcons;


    void Start()
    {
        foreach (GameObject skill in skillIcons)
        {
            skill.SetActive(false);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyAI>().TakeDamage(damage);
            Debug.Log("STRIKE");
        }
    }


    public void UnlockSkill(int index)
    {
        unlockedSkills[index] = !unlockedSkills[index];
        skillIcons[index].SetActive(!skillIcons[index].activeSelf);
    }
}
