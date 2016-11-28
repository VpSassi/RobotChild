using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class TriggeriScripti : MonoBehaviour
{

    public AudioMixerSnapshot mix1;
    public AudioMixerSnapshot mix2;
    public AudioMixerSnapshot mix3;
    public float transitiontime;
    EnemyBehavior eb; 
    // Use this for initialization
    void Start()
    {
        eb = GameObject.Find("Cannibal").GetComponent<EnemyBehavior>();
        Fabric.EventManager.Instance.PostEvent("BasicMusic"); // kaikki träkit ruoeavat soimaan startissa
    }
    void Update()
    {
        if (eb.isChasing == true) {
        mix1.TransitionTo(transitiontime);
       // print("triggasi");
        }
   
        }

    }

