using System;
using UnityEngine;

[Serializable]
public class Audio
{
    public string Name;

    public bool Loop = false;

    public AudioClip Clip;

    [Range(0f, 1f)]
    public float Volume = 1f;

    [HideInInspector]
    public AudioSource Source;
}