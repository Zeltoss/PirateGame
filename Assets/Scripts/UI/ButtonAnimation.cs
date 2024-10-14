using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonAnimation : MonoBehaviour
{
    public TMP_FontAsset newFont; // Die neue Schriftart, die bei Pointer Enter verwendet werden soll
    public TMP_FontAsset oldFont; // Die alte Schriftart, die bei Pointer Exit verwendet werden soll
    public Sprite newSprite; // Das neue Hintergrund-Sprite, das bei Pointer Enter verwendet werden soll
    public Sprite oldSprite; // Das alte Hintergrund-Sprite, das bei Pointer Exit verwendet werden soll
    private TMP_Text buttonText;
    private Image backgroundImage;

    void Start()
    {
        // Holt sich die TMP_Text-Komponente des Buttons
        buttonText = GetComponentInChildren<TMP_Text>();
        if (buttonText == null)
        {
            Debug.LogError("TMP_Text component not found. Make sure the script is attached to a GameObject with a TMP_Text child.");
        }

        // Holt sich die Image-Komponente fuer das Hintergrund-Sprite
        backgroundImage = GetComponent<Image>();
        if (backgroundImage == null)
        {
            Debug.LogError("Image component not found. Make sure the script is attached to a GameObject with an Image component.");
        }

        // Setzt das alte Hintergrund-Sprite initial
        if (backgroundImage != null && oldSprite != null)
        {
            backgroundImage.sprite = oldSprite;
        }
    }

    public void OnPointerEnter()
    {
        // Wechseln zur neuen Schriftart
        if (buttonText != null && newFont != null)
        {
            buttonText.font = newFont;
        }

        // Setzen des neuen Hintergrund-Sprites
        if (backgroundImage != null && newSprite != null)
        {
            backgroundImage.sprite = newSprite;
        }
    }

    public void OnPointerExit()
    {
        // Wechseln zur alten Schriftart
        if (buttonText != null && oldFont != null)
        {
            buttonText.font = oldFont;
        }

        // Setzen des alten Hintergrund-Sprites
        if (backgroundImage != null && oldSprite != null)
        {
            backgroundImage.sprite = oldSprite;
        }
    }
}
