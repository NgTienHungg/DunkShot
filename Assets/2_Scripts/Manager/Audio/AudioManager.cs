using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private Audio[] Sounds;
    [SerializeField] private Audio[] Musics;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (var sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.loop = sound.Loop;
            sound.Source.volume = sound.Volume;
        }
    }

    public void PlaySound(string soundName)
    {
        if (SaveSystem.GetInt(SaveKey.ON_SOUND) == 0)
        {
            return;
        }

        Audio sound = Array.Find(Sounds, sound => sound.Name == soundName);

        if (sound == null)
        {
            Debug.LogWarning("Can't find sound with name: " + soundName);
            return;
        }

        Debug.Log(soundName);
        sound.Source.Play();
    }

    public void PlayVibrate()
    {
        if (SaveSystem.GetInt(SaveKey.ON_VIBRATE) == 1)
        {
            Handheld.Vibrate();
        }
    }
}