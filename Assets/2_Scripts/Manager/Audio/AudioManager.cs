using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance { get => _instance; }

    [SerializeField] private Audio[] Sounds;
    [SerializeField] private Audio[] Musics;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (var sound in Musics)
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

        Audio sound = Array.Find(Musics, sound => sound.Name == soundName);

        if (sound == null)
        {
            Debug.LogWarning("Can't find sound with name: " + soundName);
            return;
        }

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