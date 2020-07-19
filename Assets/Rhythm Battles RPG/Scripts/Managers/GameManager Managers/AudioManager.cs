using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Manager
{
    public override void ConnectManager()
    {
        GameManager.Instance.AudioManager = this;
    }

    public AudioSource PlaySound(AudioClip audioClip, float volume = 1, bool loop = false, float pitch = 1, GameObject sourceObject = null)
    {
        float spatialBlend = 0;
        if (sourceObject == null)
        {
            sourceObject = gameObject;
        }
        else
        {
            spatialBlend = 1;
        }
        AudioSource newAudioSource = sourceObject.AddComponent<AudioSource>();
        newAudioSource.playOnAwake = false;
        newAudioSource.clip = audioClip;
        newAudioSource.volume = volume;
        newAudioSource.loop = loop;
        newAudioSource.pitch = pitch;
        newAudioSource.spatialBlend = spatialBlend;
        newAudioSource.Play();
        AudioSourceDestroyer tempAudioSourceDestroyer = sourceObject.AddComponent<AudioSourceDestroyer>();
        tempAudioSourceDestroyer.AudioSource = newAudioSource;
        return newAudioSource;
    }

    public AudioSource PlaySound(List<AudioClip> audioClipsList, float volume = 1, bool loop = false, float pitch = 1, GameObject sourceObject = null)
    {
        AudioClip randomAudioClip = audioClipsList[Random.Range(0, audioClipsList.Count)];
        return PlaySound(randomAudioClip, volume, loop, pitch, sourceObject);
    }
}