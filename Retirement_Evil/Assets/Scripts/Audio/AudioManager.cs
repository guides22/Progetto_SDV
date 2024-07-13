using Unity.Audio;
using System;
using UnityEngine;
using System.Diagnostics;

public class AudioManager : MonoBehaviour {
    
    public static AudioManager Instance;
    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        
    }

    public void Start() {
        Play("Theme");
    }

    public void Play(String name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void ToggleMusic() {
        foreach (Sound s in sounds) {
            s.source.mute = ! s.source.mute;
        }
    }

    public void Volume(float volume) {
        foreach (Sound s in sounds) {
            s.source.volume = volume;
        }
    }
}
