using UnityEngine;
using System.Collections;

public class TriggerSound : MonoBehaviour {

    public void Play(string s)
    {
        Fabric.EventManager.Instance.PostEvent(s);
    }
	
	
}
