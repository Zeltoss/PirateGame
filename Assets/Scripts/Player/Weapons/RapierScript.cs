using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapierScript : MonoBehaviour
{
    // this script manages the rapier specific skills

    private WeaponBase _weaponBase;

    public delegate void OnUsingWhirlwind();
    public static OnUsingWhirlwind onUsingWhirlwind;

    private float baseDamage;
    private float critDamage;
    private float baseCritChance;
    private float currentCritChance;

    private bool canCrit = true;



    void Start()
    {
        _weaponBase = GetComponent<WeaponBase>();

        baseDamage = _weaponBase.baseDamage;
        critDamage = _weaponBase.critDamage;
        baseCritChance = _weaponBase.baseCritChance;
    }


    void OnEnable()
    {
        // subscribe to attack events
        PlayerAttack.onUsingAttackOne += UseAttackOne;
        PlayerAttack.onUsingAttackTwo += UseAttackTwo;
    }


    void OnDisable()
    {
        PlayerAttack.onUsingAttackOne -= UseAttackOne;
        PlayerAttack.onUsingAttackTwo -= UseAttackTwo;
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (_weaponBase.attackIndex == 0)
            {
                if (_weaponBase.unlockedPassiveSkill)
                {
                    currentCritChance = baseCritChance * _weaponBase.passiveSkill[_weaponBase.passiveSkillIndex];
                }
                else
                {
                    currentCritChance = _weaponBase.baseCritChance;
                }
                // critical attack
                if (Random.value < currentCritChance && canCrit)
                {
                    other.GetComponent<EnemyAI>().TakeDamage(_weaponBase.currentDamage + critDamage);
                    PlayerAttack.onPlayerAttack?.Invoke(2);
                    StartCoroutine(CritCooldown());
                }
                // normal attack
                else
                {
                    other.GetComponent<EnemyAI>().TakeDamage(_weaponBase.currentDamage);
                }
            }
            if (_weaponBase.attackIndex == 1)
            {
                PlayerAttack.onUsingAttackOne?.Invoke(other.gameObject);
            }
            if (_weaponBase.attackIndex == 2)
            {
                PlayerAttack.onUsingAttackTwo?.Invoke(other.gameObject);
            }
        }
    }



    private void UseAttackOne(GameObject enemy)
    {
        enemy.GetComponent<EnemyAI>().TakeDamage(_weaponBase.currentDamage);
        enemy.GetComponent<EnemyAI>().TakeBleedingDamage(_weaponBase.skillOne[_weaponBase.skillOneIndex], true);
        PlayerAttack.onPlayerAttack?.Invoke(0);
    }


    private void UseAttackTwo(GameObject enemy)
    {
        PlayerAttack.onPlayerAttack?.Invoke(1);
        onUsingWhirlwind?.Invoke();
        enemy.GetComponent<EnemyAI>().TakeDamage(_weaponBase.skillTwo[_weaponBase.skillTwoIndex]);
    }



    public IEnumerator CritCooldown()
    {
        canCrit = false;
        yield return new WaitForSeconds(5);
        canCrit = true;
    }

}
