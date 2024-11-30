using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    // this script checks for attacks from the player

    // invoked when the player hits an enemy to trigger the visual cooldown
    public delegate void OnPlayerAttack(int index);
    public static OnPlayerAttack onPlayerAttack;

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



    void Awake()
    {
        _playerControls = new PlayerControls();
        currentWeapon.GetComponent<BoxCollider>().enabled = false;
        allWeapons = new List<GameObject>(GameObject.FindGameObjectsWithTag("Weapon"));
        SwitchWeapon("Rapier");

        GameManager.onPausingGame += DisableAttacks;
        GameManager.onResumingGame += EnableAttacks;
    }


    private void OnEnable()
    {
        basicAttack = _playerControls.Player.BasicAttack;
        basicAttack.performed += AttackEnemy;

        attackOne = _playerControls.Player.SpecialAttackOne;
        attackOne.performed += UseAttackOne;

        attackTwo = _playerControls.Player.SpecialAttackTwo;
        attackTwo.performed += UseAttackTwo;
        EnableAttacks();
    }


    private void OnDisable()
    {
        GameManager.onPausingGame -= DisableAttacks;
        GameManager.onResumingGame -= EnableAttacks;
        DisableAttacks();
    }



    private void EnableAttacks()
    {
        basicAttack.Enable();
        attackOne.Enable();
        attackTwo.Enable();
    }

    private void DisableAttacks()
    {
        basicAttack.Disable();
        attackOne.Disable();
        attackTwo.Disable();
    }



    private void AttackEnemy(InputAction.CallbackContext context)
    {
        //Debug.Log("trying to hit");
        if (canAttack)
        {
            StartCoroutine(AttackCooldown(0.5f));
            currentWeapon.GetComponent<BoxCollider>().enabled = true;
            currentWeapon.GetComponent<WeaponBase>().attackIndex = 0;
        }
    }



    private void UseAttackOne(InputAction.CallbackContext context)
    {
        if (canAttack && currentWeapon.GetComponent<WeaponBase>().unlockedSkillOne)
        {
            Debug.Log("using attack one");
            StartCoroutine(AttackCooldown(5));
            currentWeapon.GetComponent<BoxCollider>().enabled = true;
            currentWeapon.GetComponent<WeaponBase>().attackIndex = 1;
        }
    }


    private void UseAttackTwo(InputAction.CallbackContext context)
    {
        if (canAttack && currentWeapon.GetComponent<WeaponBase>().unlockedSkillTwo)
        {
            Debug.Log("using attack two");
            StartCoroutine(AttackCooldown(5));
            currentWeapon.GetComponent<BoxCollider>().enabled = true;
            currentWeapon.GetComponent<WeaponBase>().attackIndex = 2;
        }
    }



    private IEnumerator AttackCooldown(float time)
    {
        canAttack = false;
        yield return new WaitForSeconds(0.1f);
        currentWeapon.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(time);
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
