using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // this script manages the behavior of the individual enemies

    // walks towards player
    // if close enough -> punches player, waits for a bit, punches again

    // health -> invokes event when it drops below zero; destroys itself

    public GameObject player;
    private float distance;

    private Rigidbody rb;

    [SerializeField] private int damage = 5;
    [SerializeField] private float speed = 3f;
    private UnityEngine.Vector3 movement;
    private bool isMoving = false;
    private bool canAttack = true;

    private NavMeshAgent agent;


    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(SpawnCooldown());
    }

    void FixedUpdate()
    {
        distance = UnityEngine.Vector3.Distance(this.transform.position, player.transform.position);

        if (distance < 3)
        {
            if (canAttack)
            {
                StartCoroutine(AttackingPlayer());
            }
            isMoving = false;
            movement = UnityEngine.Vector3.zero;
        }
        else
        {
            isMoving = true;
        }


        if (isMoving)
        {
            movement = player.transform.position - transform.position;
            movement.Normalize();
            rb.velocity = movement * speed;
        }


        agent.SetDestination(player.transform.position);
    }


    private IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(1);
        //isMoving = true;
        agent.SetDestination(player.transform.position);
    }

    private IEnumerator AttackingPlayer()
    {
        canAttack = false;
        PlayerHealth.onTakingDamage?.Invoke(damage);
        yield return new WaitForSeconds(1);
        canAttack = true;
    }
}
