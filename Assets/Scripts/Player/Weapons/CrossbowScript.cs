using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrossbowScript : MonoBehaviour
{
    // this script manages the crossbow specific skills and spawns the arrows

    private WeaponBase _weaponBase;

    [SerializeField] private GameObject arrowSpawnPoint;
    [SerializeField] private GameObject arrowPrefab;

    public bool isFireArrow;
    private int fireArrowCount;

    public delegate void ShootingArrow(bool isHeadshot);
    public static ShootingArrow shootingArrow;



    void OnEnable()
    {
        shootingArrow += SpawnArrow;
    }

    void OnDisable()
    {
        shootingArrow -= SpawnArrow;
    }


    void Start()
    {
        _weaponBase = GetComponent<WeaponBase>();
    }



    private void SpawnArrow(bool isHeadshot)
    {
        Vector3 spawnPoint = arrowSpawnPoint.transform.position;
        GameObject arrowInstance = Instantiate(arrowPrefab, spawnPoint, this.transform.rotation);
        arrowInstance.GetComponent<ArrowScript>().baseDamage = _weaponBase.baseDamage;
        if (transform.localScale.x > 0)
        {
            Debug.Log("facing left");
            arrowInstance.GetComponent<ArrowScript>().shootingRight = false;
        }
        else
        {
            Debug.Log("facing right");
            Vector3 spawnScale = arrowInstance.transform.localScale;
            spawnScale.x *= -1;
            arrowInstance.transform.localScale = spawnScale;
            arrowInstance.GetComponent<ArrowScript>().shootingRight = true;
        }

        if (_weaponBase.unlockedPassiveSkill)
        {
            if (Random.value < (_weaponBase.baseCritChance * _weaponBase.passiveSkill[_weaponBase.passiveSkillIndex]))
            {
                arrowInstance.GetComponent<ArrowScript>().flyingThroughEnemy = true;
                PlayerAttack.onPlayerAttack?.Invoke(2);
            }
        }

        if (isFireArrow)
        {
            if (fireArrowCount < 4)
            {
                arrowInstance.GetComponent<ArrowScript>().shootingFireArrow = true;
                arrowInstance.GetComponent<ArrowScript>().bleedingDamage = _weaponBase.skillOne[_weaponBase.skillOneIndex];
                fireArrowCount++;
            }
            else
            {
                fireArrowCount = 0;
                isFireArrow = false;
            }
        }

        if (isHeadshot)
        {
            arrowInstance.GetComponent<ArrowScript>().shootingHeadshot = true;
            if (Random.value < _weaponBase.skillTwo[_weaponBase.skillTwoIndex])
            {
                arrowInstance.GetComponent<ArrowScript>().hittingHead = true;
            }
        }
    }

}
