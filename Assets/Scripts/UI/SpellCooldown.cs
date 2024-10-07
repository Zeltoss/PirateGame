using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellCooldown : MonoBehaviour
{   
    [SerializeField]
    private Image imageCooldown;
    [SerializeField]
    private TMP_Text textCooldown;
    [SerializeField]
    private KeyCode activationKey = KeyCode.Q;

    // Variablen fuer Cooldown Timer
    private bool isCooldown = false;
    [SerializeField]
    private float cooldownTime = 1.0f;
    private float cooldownTimer = 0.0f;

    void Start()
    {
        textCooldown.gameObject.SetActive(false);
        imageCooldown.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(activationKey))
        {
            UseSpell();
        }
        if(isCooldown)
        {
            ApplyCooldown();
        }
    }


    void ApplyCooldown()
    {
        // Zeit seit dem letzten Aufruf
        cooldownTimer -= Time.deltaTime;

        if(cooldownTimer < 0.0f)
        {
            isCooldown = false;
            textCooldown.gameObject.SetActive(false);
            imageCooldown.fillAmount = 0.0f;
        }
        else
        {
            textCooldown.text = Mathf.RoundToInt(cooldownTimer).ToString();
            imageCooldown.fillAmount = cooldownTimer / cooldownTime;
        }
    }

    public void UseSpell()
    {
        if(isCooldown)
        {
            // Spieler hat waehrend der Verwendung des Skills, den Skill gedrueckt (Sound kann hier platziert werden)
        }
        else
        {
            isCooldown = true;
            textCooldown.gameObject.SetActive(true);
            cooldownTimer = cooldownTime;
        }
    }
}
