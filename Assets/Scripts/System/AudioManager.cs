using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioSource[] channels;
    [SerializeField] Dictionary<string, AudioClip> SFXClips;

    private void Awake()
    {
        instance = this;

        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio/SFX");

        SFXClips = new Dictionary<string, AudioClip>();

        for (int i = 0; i < clips.Length; i++)
        {
            SFXClips.Add(clips[i].name.ToUpper(), clips[i]);
        }
    }

    AudioSource GetEmptyChannel()
    {
        for (int i = 0; i < channels.Length; i++)
        {
            if (!channels[i].isPlaying)
            {
                return channels[i];
            }
        }

        return null;
    }

    public void PlaySFX(string clipName)
    {
        AudioSource channel = GetEmptyChannel();

        channel.PlayOneShot(SFXClips[clipName]);
    }
}
