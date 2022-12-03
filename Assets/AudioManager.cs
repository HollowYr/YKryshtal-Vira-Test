using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null) return;
        sound.source.Play();
    }
}
[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0, 1f)]
    public float volume;
    [Range(0, 3f)]
    public float pitch;
    [HideInInspector]
    public AudioSource source;
    public bool loop;
}
