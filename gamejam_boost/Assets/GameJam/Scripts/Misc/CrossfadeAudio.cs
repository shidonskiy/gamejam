using System;
using System.Collections;
using System.Collections.Generic;
using GameJam.Scripts.Levels;
using GameJam.Scripts.Obstacles;
using UnityEngine;

public class CrossfadeAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _badStateAudio;
    [SerializeField] private AudioClip _goodStateAudio;

    [SerializeField] private float _audioTransitionTime;

    public void Fade(BaseObstacle.ObstacleState state)
    {
        switch (state)
        {
            case BaseObstacle.ObstacleState.Good:
                StartCoroutine(FadeIt(_goodStateAudio));
                break;
            case BaseObstacle.ObstacleState.Bad:
                StartCoroutine(FadeIt(_badStateAudio));
                break;
        }
        
    }

    IEnumerator FadeIt(AudioClip clip)
    {
        var timeElapsed = 0f;
        var currSource = GetComponent<AudioSource>();
        var defaultVolume = currSource.volume;
        
        ///Add new audiosource and set it to all parameters of original audiosource
        AudioSource fadeOutSource = gameObject.AddComponent<AudioSource>();

        fadeOutSource.clip = currSource.clip;
        fadeOutSource.time = currSource.time;
        fadeOutSource.volume = currSource.volume;
        fadeOutSource.outputAudioMixerGroup = currSource.outputAudioMixerGroup;

        //make it start playing
        fadeOutSource.Play();

        //set original audiosource volume and clip
        currSource.volume = 0f;
        currSource.clip = clip;
        float t = 0;

        currSource.Play();

        //begin fading in original audiosource with new clip as we fade out new audiosource with old clip
        while (t < _audioTransitionTime)
        {

            t = timeElapsed / _audioTransitionTime;
            fadeOutSource.volume = Mathf.Lerp(defaultVolume, 0f, t);
            currSource.volume = Mathf.Lerp(0f, defaultVolume, t);

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        GetComponent<AudioSource>().volume = defaultVolume;
        //destroy the fading audiosource
        Destroy(fadeOutSource);
        yield break;
    }
}