using UnityEngine;
using System.Collections;

public class FollowOnInteract : MonoBehaviour {

    GameObject player;
    NavMeshAgent navAgent;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("robotChild");
        navAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        navAgent.SetDestination(player.transform.position);
	}
}
