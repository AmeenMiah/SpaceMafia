﻿using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    //public static AudioManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        //if (instance == null)
            //instance = this;
        //else
        //{
            //Destroy(gameObject);
            //return;
        //}
        //DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.group;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;

        /*
        if (PauseMenu.GameIsPaused)
        {
            s.source.volume = 0f;
        }
        else if (PauseMenu.GameIsPaused == false)
        {
            s.source.volume = s.volume;
        }
        */


        s.source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
