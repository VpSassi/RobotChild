using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadSceneOnClick : MonoBehaviour {

	public void LoaByIndex(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }
}
