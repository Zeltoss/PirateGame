using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    // this script checks for attacks from the player

    public delegate void OnChangingWeapon(GameObject weapon);
    public static OnChangingWeapon onChangingWeapon;

    [SerializeField] private GameObject currentWeapon;

    public List<GameObject> allWeapons;
    //private int weaponIndex;

    private PlayerControls _playerControls;
    private InputAction basicAttack;
    private InputAction attackOne;
    private InputAction attackTwo;


    private bool canAttack = true;

    private float testDamage = 10f;



    void Awake()
    {
        _playerControls = new PlayerControls();
        currentWeapon.GetComponent<BoxCollider>().enabled = false;
        allWeapons = new List<GameObject>(GameObject.FindGameObjectsWithTag("Weapon"));
    }


    private void OnEnable()
    {
        basicAttack = _playerControls.Player.BasicAttack;
        basicAttack.Enable();
        basicAttack.performed += AttackEnemy;

        attackOne = _playerControls.Player.SpecialAttackOne;
        attackOne.Enable();
        attackOne.performed += UseAttackOne;

        attackTwo = _playerControls.Player.SpecialAttackTwo;
        attackTwo.Enable();
        attackTwo.performed += UseAttackTwo;
    }


    private void OnDisable()
    {
        basicAttack.Disable();
        attackOne.Disable();
        attackTwo.Disable();
    }



    private void AttackEnemy(InputAction.CallbackContext context)
    {
        Debug.Log("trying to hit");
        if (canAttack)
        {
            StartCoroutine(AttackCooldown());
            currentWeapon.GetComponent<BoxCollider>().enabled = true;
            currentWeapon.GetComponent<MeleeWeapon>().currentDamage = testDamage;
        }
    }



    private void UseAttackOne(InputAction.CallbackContext context)
    {
        if (canAttack && currentWeapon.GetComponent<MeleeWeapon>().unlockedSkillOne)
        {
            StartCoroutine(AttackCooldown());
            currentWeapon.GetComponent<BoxCollider>().enabled = true;
            // get damage
        }
    }


    private void UseAttackTwo(InputAction.CallbackContext context)
    {
        if (canAttack && currentWeapon.GetComponent<MeleeWeapon>().unlockedSkillTwo)
        {
            StartCoroutine(AttackCooldown());
            currentWeapon.GetComponent<BoxCollider>().enabled = true;
            // get damage
        }
    }



    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.1f);
        currentWeapon.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1);
        canAttack = true;
    }



    public void SwitchWeapon(string weaponName)
    {
        foreach (GameObject weapon in allWeapons)
        {
            if (weapon.name == weaponName)
            {
                weapon.SetActive(true);
                currentWeapon = weapon;
            }
            else
            {
                weapon.SetActive(false);
            }
        }
        onChangingWeapon?.Invoke(currentWeapon);
    }

}
