using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ButtonSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] hoverSounds;
    [SerializeField] private AudioClip[] ClickSounds;

    public void OnPointerEnter()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(hoverSounds, transform, 1f);
    }

    public void OnClick()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(ClickSounds, transform, 1f);
    }
}