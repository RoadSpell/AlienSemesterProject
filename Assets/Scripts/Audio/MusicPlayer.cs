using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private List<AudioClip> musicList = new List<AudioClip>();
    private int _musicIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!musicSource.isPlaying)
        {
            _musicIndex = (_musicIndex + 1) % musicList.Count;
            musicSource.Play();
        }
    }
}