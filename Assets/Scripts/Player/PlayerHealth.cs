using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // this script manages the player's health

    public delegate void OnTakingDamage(int damage);
    public static OnTakingDamage onTakingDamage;

    [SerializeField] private AudioClip damageSoundClip;


    [SerializeField] private int playerHealth = 100;
    private int currentHealth;

    [SerializeField] private Slider healthBar;


    void OnEnable()
    {
        onTakingDamage += TakeDamage;
        currentHealth = playerHealth;
        healthBar.value = playerHealth;
    }

    void OnDisable()
    {
        onTakingDamage -= TakeDamage;
    }

    // function with OnTriggerEnter instead?

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = 100 / playerHealth * currentHealth;
        SoundFXManager.instance.PlaySoundFXClip(damageSoundClip, transform, 0.3f);

        if (currentHealth <= 0)
        {
            Debug.Log("You're dead :(");
            GameManager.onGameOver?.Invoke();
        }
    }
}
