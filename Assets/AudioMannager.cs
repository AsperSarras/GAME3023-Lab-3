using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioMannager : MonoBehaviour
{
    AudioMixer audioMixer;
    public enum TrackID
    {
        Town,
        Wild
    }

    private AudioMannager() { }

    private static AudioMannager instance = null;
    public static AudioMannager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioMannager>();
                //

            }
            return instance;
        }

        private set { instance = value; }
    }

    [Tooltip("One track for crossfading")]
    public AudioSource musicSource1;
    [Tooltip("Another track for crossfading")]
    public AudioSource musicSource2;

    AudioClip[] tracks;


    // Start is called before the first frame update
    void Start()
    {
        AudioMannager original = Instance;
        AudioMannager[] managers = FindObjectsOfType<AudioMannager>();

        foreach (AudioMannager manager in managers)
        {
            if(manager != original)
            {
                Destroy(manager);
            }
        }

        if(this == original)
        {
            DontDestroyOnLoad(gameObject);
        }

        //SceneManager.sceneLoaded += instance.OnSceneLoaded();//

        void OnSceneLoaded(Scene newScene,LoadSceneMode loadMode)
        {
            if(newScene.name == "")
            {
                CrossFadeTo(TrackID.Town);      
            }
            if(newScene.name == "")
            {
                CrossFadeTo(TrackID.Wild);
            }
        }

    }

    public void PlayTrack(TrackID whichTrackToPlay)
    {
        musicSource1.Stop();
        musicSource2.Stop();
        musicSource1.clip = tracks[(int)whichTrackToPlay];
        musicSource1.Play();
    }

    public void CrossFadeTo(TrackID goalTrack, float TransitionDurationInSeconds = 3.0f)
    {
        AudioSource oldTrack = null;
        AudioSource newTrack = null;

        if(musicSource1.isPlaying)
        {
            oldTrack = musicSource1;
            newTrack = musicSource2;
        }
        else if (musicSource2.isPlaying)
        {
            oldTrack = musicSource2;
            newTrack = musicSource1;
        }

        newTrack.clip = tracks[(int)goalTrack];
        newTrack.Play();

        StartCoroutine(CrossFadeCoroutine(oldTrack, newTrack, TransitionDurationInSeconds));

    }

    private IEnumerator CrossFadeCoroutine(AudioSource oldTrack, AudioSource newTrack, float TransitionDurationInSeconds)
    {
        float time = 0.0f;
        while (time < TransitionDurationInSeconds)
        {
            float tValue = time / TransitionDurationInSeconds;

            newTrack.volume = tValue;
            oldTrack.volume = 1.0f - tValue;

            time += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        oldTrack.Stop();
        oldTrack.volume = 1.0f;

        throw new NotImplementedException();
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("",volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("", volume);
    }
}
