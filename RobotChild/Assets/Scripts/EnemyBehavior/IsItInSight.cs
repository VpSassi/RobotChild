using UnityEngine;
using System.Collections;

public class IsItInSight : MonoBehaviour {

    public float detectionAngle;

    GameObject player;
    PlayerAbilities pa;





    void Start() {
        player = GameObject.Find("robotChild");
        pa = player.GetComponent<PlayerAbilities>();
    }

    void Update() {
        
    }





    public bool IsPlayerInSight(GameObject observer, LayerMask rayMask) {
        Vector3 playerDirection = player.transform.position - observer.transform.position;
        Debug.DrawRay(transform.position, playerDirection);

        if (!Physics.Raycast(transform.position, playerDirection, playerDirection.magnitude, rayMask) && (playerDirection.magnitude < pa.lightValue) && (Vector3.Angle(transform.forward, player.transform.position - transform.position) < detectionAngle)) {
            return true;
        }
        else {
            return false;
        }
    }
}
