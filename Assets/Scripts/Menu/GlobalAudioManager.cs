using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudioManager : MonoBehaviour
{
    public static GlobalAudioManager instance;

    bool isSoundMuted = false;
    bool isMusicMuted = false;
    AudioSource audio;

    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameMusic;

    public void ToggleSound()
    {
        isSoundMuted = !isSoundMuted;
    }
    public bool IsSoundMuted() => isSoundMuted;

    public void ToggleMusic()
    {
        isMusicMuted = !isMusicMuted;
        audio.mute = isMusicMuted;
    }

    public bool IsMusicMuted() => isMusicMuted;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            audio = GetComponent<AudioSource>();
            audio.Play();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            StartCoroutine(ChangeMusicCorout(menuMusic));
        }
        else
        {
            StartCoroutine(ChangeMusicCorout(gameMusic));
        }
    }
    IEnumerator ChangeMusicCorout(AudioClip clip)
    {
        float volume = 1;
        while(volume > 0)
        {
            volume -= Time.deltaTime * 2;
            audio.volume = volume; 
            yield return null;
        }
        audio.clip = clip;
        audio.Play();
        while (volume < 1)
        {
            volume += Time.deltaTime * 2;
            audio.volume = volume;
            yield return null;
        }
        audio.volume = 1;

    }
    
}
