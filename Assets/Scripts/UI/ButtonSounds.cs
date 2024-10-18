using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ButtonSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] hoverSounds;

    public void OnPointerEnter()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(hoverSounds, transform, 1f);
    }
}