using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioSource ap;

    public void PlayClip(int clip)
    {
        ap.clip = clips[clip];
        ap.Play();
    }
}
