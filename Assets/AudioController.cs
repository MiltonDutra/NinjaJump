using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour {
    public enum TypeSound { Effects, Theme}
    public Image buttonAudio;
    public Sprite active, desactive;
    public AudioClip buttonEffects, ninjaEffect, audioTheme, audioThemeDefaut;
    public bool playMusicThemeAwake;
    private static AudioSource audioSourceTheme, audioSourceEffects;
    private static bool stopAudio;
    public static AudioController audioController;
	void Start () {
        audioController = this;
        if (audioSourceTheme == null)
        {
            GameObject audioSourceGlobal = GameObject.Find("AudioSourceGlobal");
            audioSourceTheme = audioSourceGlobal.GetComponent<AudioSource>();
            audioSourceGlobal.name = "AudioSourceGlobalActive";
            DontDestroyOnLoad(audioSourceGlobal);
        }
        else
        {
            GameObject audioSourceGlobal = GameObject.Find("AudioSourceGlobal");
            if (audioSourceGlobal) Destroy(audioSourceGlobal);
        }
        if (audioSourceEffects == null)
        {
            GameObject audioSourceEffect = GameObject.Find("AudioSourceEffects");
            audioSourceEffects = audioSourceEffect.GetComponent<AudioSource>();
            audioSourceEffects.name = "AudioSourceEffectsActive";
            DontDestroyOnLoad(audioSourceEffect);
        }
        else
        {
            GameObject audioSourceEffects = GameObject.Find("AudioSourceEffects");
            if (audioSourceEffects) Destroy(audioSourceEffects);
        }
        stopAudio = PlayerPrefs.GetInt("stopAudio") == 0;
        if (audioTheme)
        {
            audioSourceTheme.clip = audioTheme;
            audioSourceTheme.Play();
        }
        if (stopAudio)
        {
            audioSourceTheme.Stop();
            PlayerPrefs.SetInt("stopAudio", 0);
            buttonAudio.sprite = desactive;
        }
        else
        {
            if (playMusicThemeAwake)
            {
                audioSourceTheme.UnPause();
            }
        }
        
    }
    public static void PlayOneShotAudio(AudioClip audio, TypeSound typeSound)
    {
        switch (typeSound){
            case TypeSound.Effects:
                if (!stopAudio) audioSourceEffects.PlayOneShot(audio);
                break;
            case TypeSound.Theme:
                if (!stopAudio) audioSourceTheme.PlayOneShot(audio);
                break;
        }
    }
    public static void PlayOneShotAudio(AudioClip[] audio, TypeSound typeSound)
    {
        switch (typeSound)
        {
            case TypeSound.Effects:
                if (!stopAudio)
                {
                    foreach (AudioClip clip in audio)
                    {
                        audioSourceEffects.PlayOneShot(clip);
                    }
                }
                break;
            case TypeSound.Theme:
                if (!stopAudio)
                {
                    foreach (AudioClip clip in audio)
                    {
                        audioSourceTheme.PlayOneShot(clip);
                    }
                }
                break;
        }
    }
    public void DontPlayAudios()
    {
        if (!stopAudio)
        {
            audioSourceTheme.Stop();
            audioSourceEffects.Stop();
            PlayerPrefs.SetInt("stopAudio", 0);
            buttonAudio.sprite = desactive;
        }
        else
        {
            if (playMusicThemeAwake)
            {
                audioSourceTheme.Play();
            }
            audioSourceEffects.Play();
            PlayerPrefs.SetInt("stopAudio", 1);
            buttonAudio.sprite = active;
        }
        stopAudio = !stopAudio;
    }
    public void ButtonEffects()
    {
        PlayOneShotAudio(buttonEffects, TypeSound.Effects);
    }
    public void MusicTheme(bool active)
    {
        if (!active) audioSourceTheme.Pause();
        else audioSourceTheme.UnPause();
    }
    public void NinjaPlayEffect()
    {
        if(!Ninja.desconected)
        PlayOneShotAudio(ninjaEffect, TypeSound.Effects);
    }
    public void SetAudioClipTheme()
    {
        if (audioTheme)
        {
            audioSourceTheme.clip = audioThemeDefaut;
            audioSourceTheme.Play();
            audioSourceTheme.Pause();
        }
    }
    
}
