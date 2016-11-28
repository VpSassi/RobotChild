using UnityEngine;
using System.Collections;
public enum CAMP_AUDIO
{
    INVALID             =       0,
    THROWING_SOUND      =       1,
}

public class PlayAudio : MonoBehaviour {
    public AudioSource throwingSound;

    public void PlaySoundByName(CAMP_AUDIO name)
    {
        switch (name)
        {
            case CAMP_AUDIO.THROWING_SOUND:
                Play(throwingSound);
                break;

            default:
                print("INVALID");
                break;
        }
    }

    public void Play(AudioSource source)
    {
        if(source)
        {
            source.Play();
        }
    }
}
