using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapierScript : MonoBehaviour
{
    // this script manages the rapier specific skills

    private WeaponBase _weaponBase;

    //private bool isCurrentWeapon;
    // -> in case script reacts when weapon is not active

    public delegate void OnUsingWhirlwind();
    public static OnUsingWhirlwind onUsingWhirlwind;



    void Start()
    {
        _weaponBase = GetComponent<WeaponBase>();
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
        Debug.Log("whirling");
        enemy.GetComponent<EnemyAI>().TakeDamage(_weaponBase.skillTwo[_weaponBase.skillTwoIndex]);
    }

}
