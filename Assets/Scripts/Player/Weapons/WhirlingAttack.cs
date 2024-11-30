using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlingAttack : MonoBehaviour
{
    // this script checks for enemies hit by the whirling attack

    public float whirlingDamage;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyAI>().TakeDamage(whirlingDamage);
        }
    }

}
