using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomAudio : MonoBehaviour {
    public List<AudioSource> source;

    public void RandomPlay()
    {
        source[Random.Range(0, source.Count)].Play();
    }
}
