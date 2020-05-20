using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// game sound effect
/// </summary>

public class audioManager : MonoBehaviour
{
    public static audioManager instance { get; private set; }

    private AudioSource audioS;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        audioS = GetComponent<AudioSource>();
    }

    /// <summary>
    /// play music
    /// </summary>
    /// <param name="clip"></param>

    public void AudioPlay(AudioClip clip)
    {
        audioS.PlayOneShot(clip);
    }
}
