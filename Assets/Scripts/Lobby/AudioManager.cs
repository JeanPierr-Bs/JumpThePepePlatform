using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource[] music;
    [SerializeField] private AudioSource[] sfx;

    public AudioMixerGroup musicMixer, sfxMixer;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        PlayMusic(0);
    }

    public void PlayMusic(int musicToPlay)
    {
        music[musicToPlay].Play();
    }
    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Play();
    }
    public void SetMusicLevel()
    {
        musicMixer.audioMixer.SetFloat("MusicVol", UIManager.Instance.musicVolSlider.value);
    }
    public void SetSFXLevel()
    {
        sfxMixer.audioMixer.SetFloat("SfxVol", UIManager.Instance.SfxVolSlider.value);
    }
}
