using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Sound;

public class MusicManager : MonoBehaviour
{
    public AudioClip defaultMusic;

    private void Start()
    {
        if (defaultMusic) MusicPooling.Play(defaultMusic, true);
    }

    public void ChangeMusic(AudioClip newMusic)
    {
        MusicPooling.Play(newMusic, true);
    }
}
