using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceDestroyer : MonoBehaviour
{
    [HideInInspector] public AudioSource AudioSource = null;

    private void Update()
    {
        if (AudioSource && !AudioSource.isPlaying)
        {
            Destroy(AudioSource);
            Destroy(this);
        }
    }
}