﻿using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    public Transform audiosource;
    public float closeEnough = 1f;
    public float aggroTime;
    public Transform[] points;
    public LayerMask layerMask;

    private int destPoint = 0;
    float remainingDistance;
    float lookTimer;
    float aggroTimer;
    float lookAngle;
    bool lookingForPlayer;
    bool playerLastPosReached;
    GameObject player;
    GameObject robotChild;
    NavMeshAgent navAgent;
    Renderer rend;
    Vector3 direction;
    Vector3 robotChildLastPos;
    Quaternion lookRotation;

    Search search;
    PlayerAbilities pa;
    IsItInSight iiis;
    AcquireTarget at;


	public bool isChasing;


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
        iiis = GetComponent<IsItInSight>();
        at = GetComponent<AcquireTarget>();
        rend = GetComponent<Renderer>();
        layerMask = ~layerMask;         //We want our raycasts to ignore the selected layers  
    }

	void Update() {
        robotChild = at.ClosestChild();

        //audiosource.position = transform.position;      //kuljettaa fabricin audiosourcea
        lookTimer += Time.deltaTime;
        aggroTimer -= Time.deltaTime;

        //Detection behavior
        if (iiis.IsPlayerInSight(gameObject, layerMask)) {
            rend.material.color = Color.red;
            robotChildLastPos = robotChild.transform.position;
            navAgent.SetDestination(robotChild.transform.position);
			isChasing = true;
            if ((robotChild.transform.position - transform.position).magnitude < closeEnough) {
                rend.material.color = Color.black;
                //Fabric.EventManager.Instance.PostEvent("AttackMusic");
            }
            if (lookingForPlayer) {               
                aggroTimer = aggroTime;
            }
            else {
                lookingForPlayer = true;               
                aggroTimer = aggroTime;
				
            }
        }
        else if (lookingForPlayer == true && aggroTimer > 0) {
            if (playerLastPosReached == false) {
                navAgent.SetDestination(robotChildLastPos);
                if ((robotChildLastPos - transform.position).magnitude < closeEnough) {
                    playerLastPosReached = true;
                }
            }
            if (playerLastPosReached == true) {
                rend.material.color = Color.yellow;
                LookAround();
            }
        }
        else if (aggroTimer < 0) {
            rend.material.color = Color.blue;
            lookingForPlayer = false;
            playerLastPosReached = false;
            navAgent.SetDestination(points[destPoint].position);
            isChasing = false;
        }

        //Patrol behavior
        direction = points[destPoint].position - transform.position;
        if (direction.magnitude < closeEnough) {
            NextWp();
        }
    }





    void NextWp() {
        if (points.Length == 0)
            return;

        destPoint = (destPoint + 1) % points.Length;
        navAgent.SetDestination(points[destPoint].position);
    }

    void LookAround() {
        float lookTime = 1.5f;

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.1f);

        if (lookTimer > lookTime) {
            lookAngle = Random.Range(0, 360);
            lookRotation = Quaternion.Euler(0, lookAngle, 0);
            lookTimer = 0;
        }
    }
}
