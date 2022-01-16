using UnityEngine;
using System.Collections.Generic;

public enum SoundType { Shot, Collision, Theme }

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private List<AudioSource> playingSounds = new List<AudioSource>();

    void Start()
    {
        Play(SoundType.Theme);
    }

    public void Play(SoundType type)
    {
        var source = GetEmptyAudioSource();
        var sound = GetRandomSoundOfType(type);

        source.clip = sound.clip;

        source.volume = sound.volume;
        source.pitch = sound.pitch;
        source.loop = sound.loop;

        source.Play();
    }

    private Sound GetRandomSoundOfType(SoundType type)
    {
        var soundTypeList = new List<Sound>();

        foreach (Sound s in sounds)
        {
            if (s.soundType == type)
            {
                soundTypeList.Add(s);
            }
        }
        int soundIndex = Random.Range(0, soundTypeList.Count);
        return soundTypeList[soundIndex];
    }

    private AudioSource GetEmptyAudioSource()
    {
        foreach (AudioSource ps in playingSounds)
        {
            if (!ps.isPlaying)
            {
                return ps;
            }
        }

        var newSound = gameObject.AddComponent<AudioSource>();
        playingSounds.Add(newSound);
        return newSound;
    }
}



