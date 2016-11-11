using UnityEngine;
using System.Collections;

public class IsItInSight : MonoBehaviour {

    GameObject player;
    PlayerAbilities pa;





    void Start() {
        player = GameObject.Find("robotChild");
        pa = player.GetComponent<PlayerAbilities>();
    }





    public bool IsPlayerInSight(GameObject observer, LayerMask rayMask) {
        Vector3 playerDirection = player.transform.position - observer.transform.position;

        if (!Physics.Raycast(transform.position, playerDirection, playerDirection.magnitude, rayMask) && playerDirection.magnitude < pa.lightValue) {
            return true;
        }
        else {
            return false;
        }
    }
}
