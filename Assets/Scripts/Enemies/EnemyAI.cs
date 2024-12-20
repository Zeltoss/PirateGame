using System.Collections;
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

    [SerializeField] private int health = 100;
    private int currentHealth;
    [SerializeField] private int damage = 5;
    [SerializeField] private float speed = 3f;
    private UnityEngine.Vector3 movement;

    private Camera _camera;

    private bool isMoving = false;
    private bool facingLeft = true;
    private bool canMove = false;
    private bool canAttack = true;
    private bool inAttackDistance = false;

    public GameObject player;
    private float distance;

    private Rigidbody rb;
    private NavMeshAgent agent;

    [SerializeField] private GameObject enemySprite;



    void OnEnable()
    {
        _camera = Camera.main;

        rb = GetComponent<Rigidbody>();
        agent = GetComponentInChildren<NavMeshAgent>();

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
                inAttackDistance = true;
            }
            isMoving = false;
            movement = UnityEngine.Vector3.zero;
        }
        else
        {
            inAttackDistance = false;
            isMoving = true;
        }


        if (isMoving && canMove)
        {
            movement = player.transform.position - transform.position;
            movement.Normalize();
            rb.velocity = movement * speed;
        }

        if (movement.x < 0 && !facingLeft)
        {
            FlipSprite();
        }
        else if (movement.x > 0 && facingLeft)
        {
            FlipSprite();
        }

        GetComponentInChildren<Canvas>().transform.rotation = new Quaternion(_camera.transform.rotation.x - this.transform.rotation.x, 0, 0, 0);
    }



    private void FlipSprite()
    {
        UnityEngine.Vector3 currentScale = enemySprite.transform.localScale;
        currentScale.x *= -1;
        enemySprite.transform.localScale = currentScale;

        facingLeft = !facingLeft;
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


    public void TakeBleedingDamage(float damage)
    {
        StartCoroutine(Bleeding(damage));
    }


    private IEnumerator Bleeding(float damage)
    {
        TakeDamage(damage);
        Debug.Log("bleeding damage");
        yield return new WaitForSeconds(1);
        TakeDamage(damage);
        Debug.Log("bleeding damage");
        yield return new WaitForSeconds(1);
        TakeDamage(damage);
        Debug.Log("bleeding damage");
        yield return new WaitForSeconds(1);
        TakeDamage(damage);
        Debug.Log("bleeding damage");
        yield return new WaitForSeconds(1);
        TakeDamage(damage);
        Debug.Log("bleeding damage");
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
        yield return new WaitForSeconds(0.5f);
        if (inAttackDistance)
        {
            PlayerHealth.onTakingDamage?.Invoke(damage);
            yield return new WaitForSeconds(2);
            canAttack = true;
        }
        else
        {
            canAttack = true;
        }
    }


    private IEnumerator KillAnimation()
    {
        Debug.Log("enemy killed");
        //gameObject.GetComponent<Material>().color = Color.white;
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
