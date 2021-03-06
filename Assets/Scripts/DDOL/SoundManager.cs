﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Singletone manager class to handle sounds
public class SoundManager : Singleton<SoundManager>
{
    public GameObject musicBox;
    
    // array of sound clips for music
    public AudioClip menuMusic;
    public AudioClip ghostGridBgMusic;
    public AudioClip playerGridBgMusic;
    public AudioClip awakenGridBgMusic;

    // music volume
    [Range(0,1)]
    public float musicVolume = 0.5f;

    // sound effects volume
    [Range(0,1)]
    public float fxVolume = 1.0f;

    // boundaries for random variation in pitch
    public float lowPitch = 0.95f;
    public float highPitch = 1.05f;

    protected List<AudioSource> _loopingSounds = new List<AudioSource>();
    
	void Start () 
    {
        PlayMenuMusic();
        DontDestroyOnLoad(gameObject);
	}
	
    // this replaces the native PlayClipAtPoint to play an AudioClip at a world space position
    // this allows a third volume parameter to specify the volume unlike the native version
    // and allows for some random variation so the sound is less monotonous
    public AudioSource PlayClipAtPoint(AudioClip clip, Vector3 position, float volume = 1f,bool  randomizePitch = true,bool loop = false)
    {
        if (clip != null)
        {
            // create a new GameObject at the specified world space position
            GameObject go = new GameObject("SoundFX" + clip.name);
            go.transform.position = position;
            go.transform.parent = musicBox.transform;
            // add an AudioSource component and set the AudioClip
            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = loop;
            if(randomizePitch){
            // change the pitch of the sound within some variation
            float randomPitch = Random.Range(lowPitch, highPitch);
            source.pitch = randomPitch;
            }
            // set the volume
            source.volume = volume;

            // play the sound
            source.Play();

            if (loop)
            {
                _loopingSounds.Add(source);
            }
            else
            {
                // destroy the AudioSource after the clip is done playing
                Destroy(go, clip.length);   
            }

            // return our AudioSource out of the method
            return source;
        }

        return null;
    }

    // play a random sound from an array of sounds
    public AudioSource PlayRandom(AudioClip[] clips, Vector3 position, float volume = 1f)
    {
        if (clips != null)
        {
            if (clips.Length != 0)
            {
                int randomIndex = Random.Range(0, clips.Length);

                if (clips[randomIndex] != null)
                {
                    AudioSource source = PlayClipAtPoint(clips[randomIndex], position, volume);
                    return source;
                }
            }
        }
        return null;
    }


    // play a random win sound
    public void PlayMenuMusic()
    {
        StopLoopingSounds();
        PlayClipAtPoint(menuMusic, Vector3.zero, musicVolume,false,true);
    }
    public void PlayGhostGridBgMusic()
    {
        StopLoopingSounds();
        PlayClipAtPoint(ghostGridBgMusic, Vector3.zero, musicVolume,false,true);
    }
    public void PlayPlayerGridBgMusic()
    {
        StopLoopingSounds();
        PlayClipAtPoint(playerGridBgMusic, Vector3.zero, musicVolume,false,true);
    }
    public void PlayAwakenGridBgMusic()
    {
        StopLoopingSounds();
        PlayClipAtPoint(awakenGridBgMusic, Vector3.zero, musicVolume,false,true);
    }
    public void StopLoopingSounds()
    {
        foreach (AudioSource loopingSound in _loopingSounds)
        {
            if (loopingSound != null)
            {
                loopingSound.Stop();
                Destroy(loopingSound.gameObject);
            }
        }
        
        _loopingSounds = new List<AudioSource>();

    }


}
