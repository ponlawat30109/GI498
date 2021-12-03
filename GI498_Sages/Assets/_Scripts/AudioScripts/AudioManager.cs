using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [Serializable]
    public class Sound
    {
        public Track name;
        public TrackType type;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volumn;
        [Range(0f, 3f)]
        public float pitch;

        public bool loop;

        [HideInInspector]
        public AudioSource source;
    }

    public enum TrackType
    {
        MusicBackground,
        SoundEffect
    }

    public enum Track
    {
        BGMMenu01,
        BGMMenu02,

        ClickButton01,
        NPC_Buzzer,
        NPC_CompleteOrder
    }


    public static AudioManager Instance;
    
    [SerializeField] private bool onTest = false;
    [SerializeField] private Track testTrack;

    [Header("Player Setting")]
    public float musicVolumnOption = 0.5f; //percentage [0,1]
    public float sfxVolumnOption = 0.5f; //percentage [0,1]

    [Header ("Fade Element")]
    public float timeToFade = 0.5f;
    public float timeSilentSpace = 0.5f;
    
    public Sound[] audioSounds;
    public Sound[] sfxSounds;

    //private string currentBGMName;
    //private AudioSource currentBGMSource;
    private Sound currentMusic; //for background music

    private void OnGUI()
    {
        if(onTest == true)
        {
            if(GUILayout.Button("PlaySfx"))
            {
                 PlaySfx(testTrack);
            }
            if(GUILayout.Button("PlayMusic"))
            {
                PlayMusic(testTrack);
            }
            if(GUILayout.Button("StopMusic"))
            {
                StopAudio();
            }
        }
    }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;

        foreach (Sound s in audioSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            //s.source.volume = s.volumn;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            s.source.playOnAwake = false;
        }

        foreach (Sound s in sfxSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            //s.source.volume = s.volumn;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            s.source.playOnAwake = false;
        }
    }

    public void PlaySfx(Track audioName)
    {
        Sound newSound = Array.Find(sfxSounds, sound => sound.name == audioName);
        if (newSound == null)
            return;

        newSound.source.volume = newSound.volumn * sfxVolumnOption;
        newSound.source.Play();
    }

    public void PlayMusic (Track audioName)
    {
        Sound newSound = Array.Find(audioSounds, sound => sound.name == audioName);
        
        if (newSound == null)
        {
            Debug.LogWarning("Sound: " + audioName + " not found");
            return;
        }

        if (currentMusic == null)
        {
            newSound.source.volume = newSound.volumn * musicVolumnOption;
            newSound.source.Play();
            currentMusic = newSound;
        }
        else
        {
            if (audioName == currentMusic.name)
                return;
            
            StopAllCoroutines();
            StartCoroutine(FadeSwitchTrack(newSound));
        }
    }
    public void StopAudio()
    {
        if (currentMusic != null)
        {
            StopAllCoroutines();
            StartCoroutine(FadeTrack());
        }
    }

    private IEnumerator FadeSwitchTrack(Sound newSound)
    {
        float timeElapsed = 0;

        //old Track
        var currentBGMSource = currentMusic.source;
        var oldVolumn = currentMusic.source.volume;

        while (timeElapsed < timeToFade)
        {
            currentBGMSource.volume = Mathf.Lerp(oldVolumn, 0, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        currentBGMSource.Stop();
        currentBGMSource.volume = oldVolumn;
        
        yield return new WaitForSeconds(timeSilentSpace);

        newSound.source.volume = newSound.volumn * musicVolumnOption;
        newSound.source.Play();
        currentMusic = newSound;
    }


    private IEnumerator FadeTrack()
    {
        float timeElapsed = 0;

        //old Track
        var currentBGMSource = currentMusic.source;
        var oldVolumn = currentMusic.source.volume;

        while (timeElapsed < timeToFade)
        {
            currentBGMSource.volume = Mathf.Lerp(oldVolumn, 0, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        currentBGMSource.Stop();
        currentBGMSource.volume = oldVolumn;
        currentMusic = null;
    }

    public void ChangeAudioVolumn(float percentageVolumn)
    {
        //percentageVolumn's Range = [0,1]
        musicVolumnOption = percentageVolumn;
        if (currentMusic != null)
        {
            currentMusic.source.volume = currentMusic.volumn * percentageVolumn;
        }
    }

    public void ChangeSfxVolumn(float percentageVolumn)
    {
        //percentageVolumn's Range = [0,1]
        sfxVolumnOption = percentageVolumn;
    }

}
