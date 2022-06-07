using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The audio manager
/// </summary>
public static class AudioManager
{
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();

    /// <summary>
    /// Initializes the audio manager
    /// </summary>
    /// <param name="source">audio source</param>
    public static void Initialize(AudioSource source)
    {
        audioSource = source;
        audioClips.Add(AudioClipName.AR_Fire,
            Resources.Load<AudioClip>("AR_Fire"));
        audioClips.Add(AudioClipName.PistolEmpty, 
            Resources.Load<AudioClip>("pistol empty"));
        audioClips.Add(AudioClipName.PlayerDeath,
            Resources.Load<AudioClip>("die"));
        audioClips.Add(AudioClipName.PistolShot,
            Resources.Load<AudioClip>("pistol fire"));
        audioClips.Add(AudioClipName.PistolStartReload,
            Resources.Load<AudioClip>("Pistol_Start_Reload"));
        audioClips.Add(AudioClipName.PistolStopReload,
            Resources.Load<AudioClip>("Pistol_Finish_Reload"));
    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }
}
