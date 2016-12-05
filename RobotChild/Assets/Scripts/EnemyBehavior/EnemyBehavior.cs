using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    public float closeEnough = 1f;
    public float aggroTime;
    public float attackDelay;
    public float chasingSpeed;
    public float patrolSpeed;
    public bool isChasing;
    public Transform[] points;
    public Transform[] audiosource;
    public LayerMask layerMask;
    public Animator cannibalAnim;

	public Color baseColor, chaseColor, alertColor;
	SkinnedMeshRenderer[] renderers;

    private int destPoint = 0;
    float remainingDistance;
    float lookTimer;
    float aggroTimer;
    float attackTimer;
    float lookAngle;
    bool lookingForPlayer;
    bool playerLastPosReached;
    bool isAttackTimerOn;
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
		renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

	void Update() {
        robotChild = at.ClosestChild();

        foreach (Transform t in audiosource)
        {
            t.position = transform.position;      //kuljettaa fabricin audiosourcea
        }

        lookTimer += Time.deltaTime;
        aggroTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;

        //Behavior for if player is currently in sight
        if (iiis.IsPlayerInSight(gameObject, layerMask)) {
		
			cannibalAnim.SetBool("chasing", true);           
            playerLastPosReached = false;
            isChasing = true;
            robotChildLastPos = robotChild.transform.position;
            navAgent.SetDestination(robotChild.transform.position);
            navAgent.speed = chasingSpeed;
			//change color	
			changeColor(chaseColor);
            if ((robotChild.transform.position - transform.position).magnitude < closeEnough) {     //Attack behavior here
                if (isAttackTimerOn == false) {
                    attackTimer = attackDelay;
                    isAttackTimerOn = true;
                }                   
                if (attackTimer < 0) {
                    cannibalAnim.SetBool("Kill", true);
                    //Fabric.EventManager.Instance.PostEvent("AttackMusic");
                }
            }
            if (lookingForPlayer) {               
                aggroTimer = aggroTime;
            }
            else {
                lookingForPlayer = true;               
                aggroTimer = aggroTime;				
            }
        }
        //Behavior for if player is no longer in sight but aggro timer is still on
        else if (lookingForPlayer == true && aggroTimer > 0) {
			if (playerLastPosReached == false) {
                navAgent.SetDestination(robotChildLastPos);
                if ((robotChildLastPos - transform.position).magnitude < closeEnough) {
                    playerLastPosReached = true;
                }
            }
            if (playerLastPosReached == true) {
				cannibalAnim.SetBool("chasing", false);
			    cannibalAnim.SetBool("playerPlaysDead", true);
                LookAround();
				changeColor(alertColor);
            }
        }
        //Behavior for if player is no longer in sight and agrro timer is off
        else if (aggroTimer < 0) {
			cannibalAnim.SetBool("playerPlaysDead", false);
			lookingForPlayer = false;
            playerLastPosReached = false;           
            isChasing = false;
            navAgent.SetDestination(points[destPoint].position);
            navAgent.speed = patrolSpeed;
			changeColor(baseColor);
        }

        //Patrol behavior
        direction = points[destPoint].position - transform.position;
        if (direction.magnitude < closeEnough) {
            NextWp();
        }
    }

	void changeColor(Color c) {
		for (int i = 0; i < renderers.Length; i++) {
			renderers[i].material.SetColor("_EmissionColor", c);
		}
	}



    void NextWp() {
        if (points.Length == 0)
            return;

        destPoint = (destPoint + 1) % points.Length;
        navAgent.SetDestination(points[destPoint].position);
        navAgent.speed = patrolSpeed;
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
