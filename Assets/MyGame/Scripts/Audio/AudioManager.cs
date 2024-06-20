using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager Instance
    {
        get => instance;
    }
    private static AudioManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    #region Public
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip backgroundMusic;
    public AudioClip coinSfx;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        PlayBackgroundMusic(backgroundMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PlayBackgroundMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip); 
    }
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SetSfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
