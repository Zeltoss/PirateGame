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

    public delegate void OnUsingSpecialAttacks(GameObject enemy);
    public static OnUsingSpecialAttacks onUsingAttackOne;
    public static OnUsingSpecialAttacks onUsingAttackTwo;


    public delegate void OnChangingWeapon(GameObject weapon);
    public static OnChangingWeapon onChangingWeapon;

    [SerializeField] private GameObject currentWeapon;

    [SerializeField] private AudioClip[] swingSoundClips;
    [SerializeField] private AudioClip crossbowSoundClip;

    public List<GameObject> allWeapons;
    private bool meleeWeapon;

    private PlayerControls _playerControls;
    private InputAction basicAttack;
    private InputAction attackOne;
    private InputAction attackTwo;


    private bool canAttackNormally = true;
    private bool canUseSkillOne = true;
    private bool canUseSkillTwo = true;



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
        if (canAttackNormally)
        {
            if (meleeWeapon)
            {
                StartCoroutine(AttackCooldown(0.5f, 0));
                currentWeapon.GetComponent<BoxCollider>().enabled = true;
                currentWeapon.GetComponent<WeaponBase>().attackIndex = 0;
                SoundFXManager.instance.PlayRandomSoundFXClip(swingSoundClips, transform, 1.3f);
            }
            else
            {
                StartCoroutine(AttackCooldown(1.5f, 0));
                CrossbowScript.shootingArrow?.Invoke(false);
                SoundFXManager.instance.PlaySoundFXClip(crossbowSoundClip, transform, 0.3f);
            }
        }
    }



    private void UseAttackOne(InputAction.CallbackContext context)
    {
        if (canUseSkillOne && currentWeapon.GetComponent<WeaponBase>().unlockedSkillOne)
        {
            Debug.Log("using attack one");
            StartCoroutine(AttackCooldown(4.5f, 1));
            if (meleeWeapon)
            {
                currentWeapon.GetComponent<BoxCollider>().enabled = true;
                currentWeapon.GetComponent<WeaponBase>().attackIndex = 1;
            }
            else
            {
                currentWeapon.GetComponent<CrossbowScript>().isFireArrow = true;
            }
        }
    }


    private void UseAttackTwo(InputAction.CallbackContext context)
    {
        if (canUseSkillTwo && currentWeapon.GetComponent<WeaponBase>().unlockedSkillTwo)
        {
            Debug.Log("using attack two");
            currentWeapon.GetComponent<BoxCollider>().enabled = true;
            currentWeapon.GetComponent<WeaponBase>().attackIndex = 2;
            if (meleeWeapon)
            {
                RapierScript.onUsingWhirlwind?.Invoke();
                StartCoroutine(WhirlingAttackCooldown());
            }
            else
            {
                CrossbowScript.shootingArrow?.Invoke(true);
                StartCoroutine(AttackCooldown(6, 2));
            }
        }
    }



    private IEnumerator AttackCooldown(float time, int index)
    {
        if (index == 0)
        {
            canAttackNormally = false;
        }
        else if (index == 1)
        {
            canUseSkillOne = false;
        }
        else if (index == 2)
        {
            canUseSkillTwo = false;
        }

        if (meleeWeapon)
        {
            yield return new WaitForSeconds(0.1f);
            currentWeapon.GetComponent<BoxCollider>().enabled = false;
        }

        yield return new WaitForSeconds(time);
        if (index == 0)
        {
            canAttackNormally = true;
        }
        else if (index == 1)
        {
            canUseSkillOne = true;
        }
        else if (index == 2)
        {
            canUseSkillTwo = true;
        }
    }


    private IEnumerator WhirlingAttackCooldown()
    {
        canUseSkillTwo = false;
        yield return new WaitForSeconds(1f);
        currentWeapon.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(6f);
        canUseSkillTwo = true;
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
        meleeWeapon = currentWeapon.GetComponent<WeaponBase>().isMeleeWeapon;
        onChangingWeapon?.Invoke(currentWeapon);
    }

}
