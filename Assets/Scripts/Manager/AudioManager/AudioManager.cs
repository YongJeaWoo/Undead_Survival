using SingletonComponent.Component;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AudioData
{
    public AudioClip clip;
    public bool isBgm;
}

public class AudioManager : SingletonComponent<AudioManager>
{
    #region Singleton

    protected override void AwakeInstance()
    {
    }

    protected override bool InitInstance()
    {
        return true;
    }

    protected override void ReleaseInstance()
    {

    }

    #endregion
    
    public List<AudioData> audioDatas;
    private Dictionary<string, AudioData> audioDataDictionary;

    private AudioSource bgmSource;
    private Dictionary<string, AudioSource> sfxSources;

    public void SoundInit()
    {
        GameObject bgmObject = new GameObject("BGMPlayer");
        bgmObject.transform.SetParent(transform);
        bgmSource = bgmObject.AddComponent<AudioSource>();
        bgmSource.playOnAwake = false;
        bgmSource.loop = true;

        sfxSources = new Dictionary<string, AudioSource>();

        audioDataDictionary = new Dictionary<string, AudioData>();
        foreach (AudioData data in audioDatas)
        {
            audioDataDictionary[data.clip.name] = data;
            if (!data.isBgm)
            {
                AddSFXSource(data.clip.name);
            }
        }
    }

    private void AddSFXSource(string name)
    {
        GameObject sfxObject = new GameObject(name + "Player");
        AudioSource source = sfxObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        sfxSources[name] = source;
    }

    public void PlayAudio(string name)
    {
        if (audioDataDictionary.ContainsKey(name))
        {
            AudioData data = audioDataDictionary[name];
            if (data.isBgm)
            {
                bgmSource.clip = data.clip;
                bgmSource.Play();
            }
            else
            {
                if (!sfxSources.ContainsKey(name))
                {
                    AddSFXSource(name);
                }
                AudioSource source = sfxSources[name];
                source.clip = data.clip;
                source.Play();
            }
        }
    }

    public AudioData GetAudioData(string name)
    {
        if (audioDataDictionary.ContainsKey(name))
        {
            return audioDataDictionary[name];
        }
        else
        {
            return null;
        }
    }
    public List<AudioData> GetAudioDataList() => audioDatas;

    public AudioSource GetBGMSource() => bgmSource;
    public AudioSource GetSFXSource(string name)
    {
        if (sfxSources.ContainsKey(name))
        {
            return sfxSources[name];
        }
        return null;
    }
}