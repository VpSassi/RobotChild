using UnityEngine;
using System.Collections;

public class ChangeVolumeInScene : MonoBehaviour {

    public float masterVolume = 1f;





    void Update() {
        AudioListener.volume = masterVolume;
    }





    public void ChangeVolume(float volume) {
        masterVolume = volume;
    }

}
