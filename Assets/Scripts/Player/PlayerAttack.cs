using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    // this script checks for attacks from the player

    public enum weaponTypes
    {
        melee,
        ranged
    }
    public weaponTypes equippedWeapon;


    private PlayerControls _playerControls;
    private InputAction basicAttack;
    private InputAction attackOne;
    private InputAction attackTwo;

    public delegate void OnPlayerAttack();
    public static OnPlayerAttack onPlayerAttack;

    public delegate void OnHittingEnemy(List<GameObject> enemies);
    public static OnHittingEnemy onHittingEnemy;


    private bool canAttack = true;

    private float testDamage = 10f;



    void Awake()
    {
        _playerControls = new PlayerControls();
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


        onHittingEnemy += DealDamage;
    }

    private void OnDisable()
    {
        basicAttack.Disable();
        attackOne.Disable();
        attackTwo.Disable();

        onHittingEnemy -= DealDamage;
    }



    private void AttackEnemy(InputAction.CallbackContext context)
    {
        Debug.Log("trying to hit");
        if (canAttack)
        {
            StartCoroutine(AttackCooldown());
            onPlayerAttack?.Invoke();
        }
    }



    private void UseAttackOne(InputAction.CallbackContext context)
    {
        // check if attack is unlocked
    }


    private void UseAttackTwo(InputAction.CallbackContext context)
    {
        // check if attack is unlocked
    }


    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(1);
        canAttack = true;
    }


    private void DealDamage(List<GameObject> enemies)
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyAI>().TakeDamage(testDamage);
        }
        // -> causes an error cause the list is modified by the killed enemies
        // maybe move damage to OnTriggerEnter function of weapon script? 
        Debug.Log("STRIKE");
    }
}
