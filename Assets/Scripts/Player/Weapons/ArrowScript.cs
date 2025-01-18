using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    // this script checks if the arrow hits anything

    private float damage = 10f;
    private float shootingForce = 20f;

    public Vector3 shootingDirection;
    public bool shootingRight;
    public bool shootingFireArrow;
    public bool shootingHeadshot;
    public bool hittingHead;
    public bool flyingThroughEnemy;

    private Rigidbody rb;



    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(ShootingDelay());
    }


    private IEnumerator ShootingDelay()
    {
        yield return new WaitForSeconds(0.001f);
        if (shootingRight)
        {
            rb.AddRelativeForce(new Vector3(-1f, 0, 0) * shootingForce, ForceMode.Impulse);
        }
        else
        {
            rb.AddRelativeForce(new Vector3(1f, 0, 0) * shootingForce, ForceMode.Impulse);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (shootingFireArrow && !shootingHeadshot)
            {
                other.GetComponent<EnemyAI>().TakeBleedingDamage(damage);
            }
            if (shootingHeadshot && !shootingFireArrow)
            {
                if (hittingHead)
                {
                    other.GetComponent<EnemyAI>().OneHitKill();
                }
                else
                {
                    other.GetComponent<EnemyAI>().TakeDamage(damage * 2);
                }
            }
            if (!shootingFireArrow && !shootingHeadshot)
            {
                other.GetComponent<EnemyAI>().TakeDamage(damage);
            }

            // checks to see if the arrow hits more than one enemy (from passive skill)
            if (!flyingThroughEnemy)
            {
                this.gameObject.transform.SetParent(other.gameObject.transform);
                rb.isKinematic = true;
                rb.detectCollisions = false;
                GetComponent<BoxCollider>().isTrigger = false;
            }
            else
            {
                flyingThroughEnemy = false;
            }
        }

        StartCoroutine(DespawnArrow(2f));
    }



    private IEnumerator DespawnArrow(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

}
