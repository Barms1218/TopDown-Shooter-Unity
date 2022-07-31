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
        audioClips.Add(AudioClipName.BulletHit,
            Resources.Load<AudioClip>("Bullet Hit"));
        audioClips.Add(AudioClipName.GetGun,
            Resources.Load<AudioClip>("GetGun"));
        audioClips.Add(AudioClipName.MeleeAttack,
            Resources.Load<AudioClip>("Melee_Attack"));
        audioClips.Add(AudioClipName.MenuButton,
            Resources.Load<AudioClip>("MenuButton"));
        audioClips.Add(AudioClipName.NoAmmo,
            Resources.Load<AudioClip>("No_Ammo_Sound"));
        audioClips.Add(AudioClipName.Pickup,
            Resources.Load<AudioClip>("Pickup"));
        audioClips.Add(AudioClipName.PlayerDeath,
            Resources.Load<AudioClip>("die"));
        audioClips.Add(AudioClipName.PistolShot,
            Resources.Load<AudioClip>("pistol fire"));
        audioClips.Add(AudioClipName.ReloadSound,
            Resources.Load<AudioClip>("Reload_Sound"));
        audioClips.Add(AudioClipName.ShotgunBlast,
            Resources.Load<AudioClip>("Shotgun_Fire"));
        audioClips.Add(AudioClipName.Gameplay_Music,
            Resources.Load<AudioClip>("Gameplay Scene Music"));
        audioClips.Add(AudioClipName.ZombieInmateDeath,
            Resources.Load<AudioClip>("ZombieInmateDeath"));
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
