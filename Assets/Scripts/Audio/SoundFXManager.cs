using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //spawn in gameObject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        //assign the audioClip
        audioSource.clip = audioClip;

        //assign volume
        audioSource.volume = volume;

        //play sound
        audioSource.Play();

        //get lenght of sound FX clip
        float clipLength = audioSource.clip.length;

        //destroy the clip ater it is done playing
        Destroy(audioSource.gameObject, clipLength);
    }



    //Method for Random SoundFXClips
    public void PlayRandomSoundFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
        //assign a random index
        int rand = Random.Range(0, audioClip.Length);

        //spawn in gameObject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        //assign the audioClip
        audioSource.clip = audioClip[rand];

        //assign volume
        audioSource.volume = volume;

        //play sound
        audioSource.Play();

        //get lenght of sound FX clip
        float clipLength = audioSource.clip.length;

        //destroy the clip ater it is done playing
        Destroy(audioSource.gameObject, clipLength);
    }


    //Method for looping sounds
    public void PlayLoopingSoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //Spawn in gameObject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        //Assign the audioClip
        audioSource.clip = audioClip;

        //Assign volume
        audioSource.volume = volume;

        //Enable looping
        audioSource.loop = true;

        //Play sound
        audioSource.Play();

        // The audio source won't be destroyed automatically since it's looping
        // You need to stop and manually destroy it when necessary
    }

    // Method to stop looping sound
    public void StopLoopingSound(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            Destroy(audioSource.gameObject);
        }
    }
}