using UnityEngine;
using System.Collections;

public class IsItInSight : MonoBehaviour {

    public float detectionAngle;
	public float maxDistance;

    GameObject robotChild;

    PlayerAbilities pa;
    AcquireTarget at;





    void Start() {
        robotChild = GameObject.Find("robotChild");
        pa = GameObject.Find("robotChild").GetComponent<PlayerAbilities>();
        at = GetComponent<AcquireTarget>();
    }

    void Update() {
        robotChild = at.ClosestChild();
    }





    public bool IsPlayerInSight(GameObject observer, LayerMask rayMask) {
        Vector3 playerDirection = robotChild.transform.position - observer.transform.position;
        Debug.DrawRay(transform.position, playerDirection);

        if (!Physics.Raycast(transform.position, playerDirection, playerDirection.magnitude, rayMask) && (playerDirection.magnitude < pa.lightValue * maxDistance) && (Vector3.Angle(transform.forward, robotChild.transform.position - transform.position) < detectionAngle)) {
            return true;
        }
        else {
            return false;
        }
    }
}
