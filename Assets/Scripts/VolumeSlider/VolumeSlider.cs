using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;
    private Dictionary<string, AudioSource> audioSources;

    private void Awake()
    {
        InitAwake();
    }

    private void InitAwake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(UpdateVolume);

        audioSources = new Dictionary<string, AudioSource>();
        List<AudioData> audioDataList = AudioManager.Instance.GetAudioDataList();
        foreach (AudioData data in audioDataList)
        {
            AudioSource source;
            if (data.isBgm)
            {
                source = AudioManager.Instance.GetBGMSource();
            }
            else
            {
                source = AudioManager.Instance.GetSFXSource(data.clip.name);
            }
            audioSources[data.clip.name] = source;
        }

        UpdateVolume(slider.value);
    }

    private void UpdateVolume(float volume)
    {
        foreach (KeyValuePair<string, AudioSource> entry in audioSources)
        {
            if (entry.Value != null)
            {
                entry.Value.volume = volume;
            }
        }
    }
}