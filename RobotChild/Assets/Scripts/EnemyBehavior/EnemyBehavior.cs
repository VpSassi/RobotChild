using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    public float closeEnoughToWP = 1f;
    public float aggroTime;
    public Transform[] points;
    public LayerMask layerMask;

    private int destPoint = 0;
    float remainingDistance;
    float countDown;
    float aggroTimer;
    bool lookingForPlayer;
    bool playerLastPosReached;
    enum AIState { Neutral, Alarmed, Chasing};
    GameObject player;
    NavMeshAgent navAgent;
    Renderer rend;
    Vector3 direction;
    Vector3 playerDirection;
    Vector3 playerLastPos;

    Search search;
    PlayerAbilities pa;
    MoveToLastPos mtlp;






    // TODO: also support turning around at last WP?

    void Awake() {
		navAgent = GetComponent<NavMeshAgent>();
		destPoint = points.Length - 1;
        NextWp();
    }

    void Start() {
        player = GameObject.Find("robotChild");
        search = GetComponent<Search>();
        pa = player.GetComponent<PlayerAbilities>();
        mtlp = GetComponent<MoveToLastPos>();
        rend = GetComponent<Renderer>();
        layerMask = ~layerMask;                                                                     //We want our raycasts to ignore selected layers  
    }

	void Update() {
        countDown -= Time.deltaTime;
        aggroTimer -= Time.deltaTime;
        playerDirection = player.transform.position - transform.position;
        if (points.Length > 0) {
            direction = points[destPoint].position - transform.position;
        }
        remainingDistance = direction.magnitude;

        //Detection behavior
        if (IsPlayerInSight()) {
            playerLastPos = player.transform.position;
            if (lookingForPlayer) {
                navAgent.SetDestination(player.transform.position);
                aggroTimer = aggroTime;
            }
            else {
                lookingForPlayer = true;
                navAgent.SetDestination(player.transform.position);
                rend.material.color = Color.red;
                aggroTimer = aggroTime;
            }
        }
        else if (lookingForPlayer == true && aggroTimer > 0) {
            if (playerLastPosReached == false) {
                navAgent.SetDestination(playerLastPos);
                if ((playerLastPos - transform.position).magnitude < closeEnoughToWP) {
                    playerLastPosReached = true;
                }
            }
            if (playerLastPosReached == true) {
                search.SearchInRadius(navAgent, 15f);
            }
        }
        else if (aggroTimer < 0) {
            rend.material.color = Color.blue;
            lookingForPlayer = false;
            playerLastPosReached = false;
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

    bool IsPlayerInSight() {
        if (!Physics.Raycast(transform.position, playerDirection, playerDirection.magnitude, layerMask) && playerDirection.magnitude < pa.lightValue) {
            return true;
        } else {
            return false;
        }
    }
}
