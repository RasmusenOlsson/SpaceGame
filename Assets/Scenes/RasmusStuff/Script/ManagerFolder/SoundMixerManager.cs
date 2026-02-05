using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;


    public void SetMasterVoluem(float level)
    {
        audioMixer.SetFloat("masterVolume", level);
    }

    private void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("soundFXVolume", level);
    }
    private void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("musicVolume", level);
    }
}
