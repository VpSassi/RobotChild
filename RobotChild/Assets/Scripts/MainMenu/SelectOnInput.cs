using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selectedObject;

    bool buttonSelected;





	void Start () {
	
	}
	
	void Update () {
        if (Input.GetAxis("Vertical") != 0 && buttonSelected == false) {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
	
	}

    void OnDisable() {
        buttonSelected = false;
    }
}
