using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    // this script manages everything related to the skill tree and experience points 

    public delegate void OnKillingEnemy(string enemyType);
    public static OnKillingEnemy onKillingEnemy;
    // needs to know the type of enemy -> list or something?



    void OnEnable()
    {
        onKillingEnemy += GainXP;
    }

    void OnDisable()
    {
        onKillingEnemy -= GainXP;
    }



    private void GainXP(string enemyType)
    {
        // add to XP
        // check if new skill point
        // update HUD
    }
}
