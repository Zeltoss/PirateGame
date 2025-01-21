using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public Animator animator; // Referenz zum Animator
    public float attackCooldown = 1.0f; // Zeit zwischen Angriffen
    private bool isAttacking = false; // Zustandsvariable für Angriff
    private GameObject player; // Referenz auf den Spieler

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>(); // Animator automatisch finden
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isAttacking)
        {
            player = other.gameObject;
            isAttacking = true;
            StartCoroutine(ContinuousAttack());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
            isAttacking = false;
        }
    }

    private System.Collections.IEnumerator ContinuousAttack()
    {
        while (isAttacking && player != null)
        {
            // Angriffsanimation abspielen
            animator.SetTrigger("AttackTrigger");

            // Warte die Cooldown-Zeit
            yield return new WaitForSeconds(attackCooldown);
        }
    }
}





