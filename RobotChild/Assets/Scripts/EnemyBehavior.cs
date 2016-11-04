using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    public float patrolSpeed;
    public float closeEnoughToWP = 0.5f;
    public Transform[] points;
    public LayerMask layerMask;

    private int destPoint = 0;
	float remainingDistance;
    GameObject player;
    Vector3 direction;
    Vector3 playerDirection;
    NavMeshAgent navAgent;






	// TODO: also support turning around at last WP?

	void Awake() {
		navAgent = GetComponent<NavMeshAgent>();
		destPoint = points.Length - 1;
		NextWp();		
	}

    void Start() {
        player = GameObject.Find("robotChild");
        layerMask = ~layerMask;                                                                     //We want our raycasts to ignore selected layers
    }

	void Update() {
        playerDirection = player.transform.position - transform.position;
        direction = points[destPoint].position - transform.position;
		remainingDistance = direction.magnitude;

        //Detection behavior
        if (IsPlayerDetected()) {
            navAgent.SetDestination(player.transform.position);
        }

        //Patrol logic
        if (remainingDistance < closeEnoughToWP) {
			NextWp();
		}

		// transform.position += direction.normalized * patrolSpeed * Time.deltaTime;
	}





    void NextWp() {
        if (points.Length == 0)
            return;

        destPoint = (destPoint + 1) % points.Length;
        navAgent.SetDestination(points[destPoint].position);
    }

    bool IsPlayerDetected() {
        if (!Physics.Raycast(transform.position, playerDirection, playerDirection.magnitude, layerMask)) {
            return true;
        } else {
            return false;
        }
    }
}
