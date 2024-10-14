using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    // this script manages the behavior of the individual enemies

    public enum enemyTypes
    {
        melee,
        shooter
    }
    public enemyTypes enemyType;

    [SerializeField] private int health = 20;
    private int currentHealth;
    [SerializeField] private int damage = 5;
    [SerializeField] private float speed = 3f;
    private UnityEngine.Vector3 movement;

    private Camera _camera;

    private bool isMoving = false;
    private bool canMove = false;
    private bool canAttack = true;

    public GameObject player;
    private float distance;

    private Rigidbody rb;
    private NavMeshAgent agent;



    void OnEnable()
    {
        _camera = Camera.main;

        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(SpawnCooldown());

        currentHealth = health;
    }


    void FixedUpdate()
    {
        agent.SetDestination(player.transform.position);

        distance = UnityEngine.Vector3.Distance(this.transform.position, player.transform.position);

        if (distance < 4)
        {
            if (canAttack && GetComponentInChildren<EnemyAttack>().inRangeForMelee)
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


        if (isMoving && canMove)
        {
            movement = player.transform.position - transform.position;
            movement.Normalize();
            rb.velocity = movement * speed;
        }

        GetComponentInChildren<Canvas>().transform.rotation = new Quaternion(_camera.transform.rotation.x - this.transform.rotation.x, 0, 0, 0);
    }



    // called by weapon scripts
    public void TakeDamage(float damage)
    {
        currentHealth -= Mathf.RoundToInt(damage);
        GetComponentInChildren<Slider>().value = 100 / health * currentHealth;
        if (currentHealth <= 0)
        {
            canMove = false;
            canAttack = false;
            StartCoroutine(KillAnimation());
            SkillTreeManager.onKillingEnemy(this.gameObject);
        }
    }



    private IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(1);
        canMove = true;
    }


    // makes the enemy attack the player and adds a little cooldown before they can attack again
    private IEnumerator AttackingPlayer()
    {
        canAttack = false;
        PlayerHealth.onTakingDamage?.Invoke(damage);
        yield return new WaitForSeconds(1);
        canAttack = true;
    }


    private IEnumerator KillAnimation()
    {
        Debug.Log("enemy killed");
        //gameObject.GetComponent<Material>().color = Color.white;
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
