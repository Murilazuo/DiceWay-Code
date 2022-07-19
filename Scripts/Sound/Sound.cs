using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;
    public bool loop = false;
    [Range(0f, 1f)] public float volume = 1f;
}