using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    public float closeEnoughToWP = 0.5f;
    public Transform[] points;
    public LayerMask layerMask;

    private int destPoint = 0;
    float remainingDistance;
    float countDown;
    float aggroTimer;
    bool hasPlayerBeenDetected;
    //enum AIState { Neutral, Alarmed, Chasing};
    //AIState aiState;
    GameObject player;
    NavMeshAgent navAgent;
    Renderer rend;
    Vector3 direction;
    Vector3 playerDirection;
    Vector3 playerLastPos;






	// TODO: also support turning around at last WP?

	void Awake() {
		navAgent = GetComponent<NavMeshAgent>();
		destPoint = points.Length - 1;
		NextWp();
	}

    void Start() {
        player = GameObject.Find("robotChild");
        rend = GetComponent<Renderer>();
        layerMask = ~layerMask;                                                                     //We want our raycasts to ignore selected layers  
        //aiState = AIState.Neutral;
    }

	void Update() {
        countDown -= Time.deltaTime;
        aggroTimer -= Time.deltaTime;
        playerDirection = player.transform.position - transform.position;
        direction = points[destPoint].position - transform.position;
		remainingDistance = direction.magnitude;

        //Detection behavior
        if (IsPlayerInLos()) {
            playerLastPos = player.transform.position;
            if (hasPlayerBeenDetected == false) {
                hasPlayerBeenDetected = true;
                navAgent.SetDestination(player.transform.position);
                rend.material.color = Color.red;
                aggroTimer = 5.0f;
            }
            if (hasPlayerBeenDetected == true) {
                navAgent.SetDestination(player.transform.position);
                aggroTimer = 5;
            }
        } else if (hasPlayerBeenDetected == true && aggroTimer > 0) {
            navAgent.SetDestination(playerLastPos);
        } else if (aggroTimer < 0) {
            rend.material.color = Color.blue;
            hasPlayerBeenDetected = false;
            navAgent.SetDestination(points[destPoint].position);
        }

        //Patrol behavior
        if (remainingDistance < closeEnoughToWP) {
			NextWp();
		}
	}





    void NextWp() {
        if (points.Length == 0)
            return;

        destPoint = (destPoint + 1) % points.Length;
        navAgent.SetDestination(points[destPoint].position);
    }

    bool IsPlayerInLos() {
        if (!Physics.Raycast(transform.position, playerDirection, playerDirection.magnitude, layerMask)) {
            return true;
        } else {
            return false;
        }
    }
}
