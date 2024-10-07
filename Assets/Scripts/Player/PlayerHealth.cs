using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // this script manages the player's health

    public delegate void OnTakingDamage(int damage);
    public static OnTakingDamage onTakingDamage;


    private int playerHealth = 100;

    [SerializeField] private Slider healthBar;


    void OnEnable()
    {
        onTakingDamage += TakeDamage;
        healthBar.value = playerHealth;
    }

    void OnDisable()
    {
        onTakingDamage -= TakeDamage;
    }

    private void TakeDamage(int damage)
    {
        playerHealth -= damage;
        healthBar.value = playerHealth;

        if (playerHealth <= 0)
        {
            Debug.Log("You're dead :(");
        }
    }

}
