using UnityEngine;

[CreateAssetMenu(menuName = "Sound", fileName = "Some Sound")]
public class Sound : ScriptableObject
{
    public AudioClip clip;
    public bool loop;
    [Range(0f, 1f)] public float volume;
    [Range(0f, 3f)] public float pitch;

    public SoundType soundType;
}
