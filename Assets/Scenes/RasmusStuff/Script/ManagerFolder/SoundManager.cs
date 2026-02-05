using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Jobs;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource soundObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXclip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //Spawn in a game object
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);

        //assign the audioclip
        audioSource.clip = audioClip;

        //Assign volume
        audioSource.volume = volume;

        //Play sound
        audioSource.Play();

        //get length of sound vfx clip
        float clipLength = audioSource.clip.length;

        //destroy the clip
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSoundFXclip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
        //Get int random
        int rand = Random.Range(0, audioClip.Length);

        //Spawn in a game object
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);

        //assign the audioclip
        audioSource.clip = audioClip[rand];

        //Assign volume
        audioSource.volume = volume;

        //Play sound
        audioSource.Play();

        //get length of sound vfx clip
        float clipLength = audioSource.clip.length;

        //destroy the clip
        Destroy(audioSource.gameObject, clipLength);
    }
}
