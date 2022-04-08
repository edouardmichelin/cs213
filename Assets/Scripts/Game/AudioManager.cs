using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    
    public int position = 0;
    public int samplerate = 44100;
    public float frequency = 440;

    public float MasterVolume
    {
        set
        {
            music.volume = value;
            effect.volume = value;
        }
    }
    
    public float MusicVolume
    {
        get { return music.volume;  }
        set { music.volume = value;  }
    }
    
    public float EffectsVolume
    {
        get { return effect.volume;  }
        set { effect.volume = value;  }
    }
    
    public bool muteBackgroundMusic
    {
        get { return music.mute;  }
        set { music.mute = value;  }
    }

    private AudioSource music;
    private AudioSource effect;
    private Dictionary<String, AudioClip> m_clips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        music = gameObject.AddComponent<AudioSource>();
        effect = gameObject.AddComponent<AudioSource>();
        music.loop = true;
        
        foreach (var clip in Resources.LoadAll<AudioClip>("Audio"))
        {
            m_clips.Add(clip.name, clip);
        }
        
        PlayBackgroundMusic("StreetLove");
    }
    
    public void PlaySoundEffect(String name)
    {
        Play(name, effect);
    }
    
    public void PlayBackgroundMusic(String name)
    {
        Play(name, music);
    }

    private void Play(String name, AudioSource source)
    {
        if (m_clips.TryGetValue(name, out AudioClip clip))
        {
            source.clip = clip;
            source.Play();
        }
    }
}