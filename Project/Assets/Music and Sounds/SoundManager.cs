using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public List<NamedClip> musicClips;
    public List<NamedClip> sfxClips;

    private Dictionary<string, AudioClip> musicDict;
    private Dictionary<string, AudioClip> sfxDict;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicDict = LoadClipDictionary(musicClips);
        sfxDict = LoadClipDictionary(sfxClips);
    }

    private Dictionary<string, AudioClip> LoadClipDictionary(List<NamedClip> clips)
    {
        var dict = new Dictionary<string, AudioClip>();
        foreach (var clip in clips)
            if (!dict.ContainsKey(clip.name))
                dict.Add(clip.name, clip.clip);
        return dict;
    }
    // for background music etc
    public void PlayMusic(string name, bool loop = true)
    {
        if (musicDict.TryGetValue(name, out var clip))
        {
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }
    public void ResumeMusic()
    {
        musicSource.UnPause();
    }
    // more like for effects
    public void PlaySoundClipFromArray(AudioClip[] audioClips, Vector3 spawnPos, float volume = 1f)
    {
        if (audioClips[UnityEngine.Random.Range(0, audioClips.Length - 1)])
        {
            AudioClip clipToPlay = audioClips[UnityEngine.Random.Range(0, audioClips.Length - 1)];
            AudioSource audioSource = Instantiate(sfxSource, spawnPos, Quaternion.identity);
            audioSource.clip = clipToPlay;
            audioSource.volume = volume;
            audioSource.Play();
            Destroy(audioSource.gameObject, audioSource.clip.length);
        }
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void MuteAll(bool mute)
    {
        AudioListener.pause = mute;
    }
}

[System.Serializable]
public class NamedClip
{
    public string name;
    public AudioClip clip;
}
