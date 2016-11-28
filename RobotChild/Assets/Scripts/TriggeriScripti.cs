using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class TriggeriScripti : MonoBehaviour
{

    public AudioMixerSnapshot mix1;
    public AudioMixerSnapshot mix2;
    public AudioMixerSnapshot mix3;
    public float transitiontime;

    // Use this for initialization
    void Start()
    {
        Fabric.EventManager.Instance.PostEvent("BasicMusic"); // kaikki träkit ruoeavat soimaan startissa
    }
   // void OnTriggerEnter(Collider c) 

    {
    
        mix1.TransitionTo(transitiontime);
        print("triggasi");
   
        }

    }

