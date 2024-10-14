using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{

    public float damage;

    private float[] skillOne;
    private float[] skillTwo;
    private float[] skillThree;

    public float skillOneIndex;
    public float skillTwoIndex;
    public float skillThreeIndex;



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyAI>().TakeDamage(damage);
            Debug.Log("STRIKE");
        }
    }
}
