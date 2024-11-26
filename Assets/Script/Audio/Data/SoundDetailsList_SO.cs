using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sound/SoundDetailsList_SO")]
public class SoundDetailsList_SO : ScriptableObject
{
    public List<SoundDetails> soundDetailsList = new List<SoundDetails>();

    public SoundDetails GetSoundDetails(SoundName name)
    {
        return soundDetailsList.Find(soundDetails => soundDetails.soundName == name);
    }
}
[System.Serializable]
public class SoundDetails
{
    public SoundName soundName;
    public AudioClip soundClip;
    [Range(0.1f,1.5f)]
    public float soundPitchMin;
    [Range(0.1f,1.5f)]
    public float soundPitchMax;
    [Range(0.1f,1f)]
    public float soundVolume;
}