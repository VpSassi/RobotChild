using UnityEngine;
using System.Collections;

public class FollowOnInteract : MonoBehaviour {

    public float stoppingDistance;

    GameObject player;
    NavMeshAgent navAgent;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("robotChild");
        navAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if ((navAgent.transform.position - player.transform.position).magnitude > stoppingDistance) {
            navAgent.Resume();
            navAgent.SetDestination(player.transform.position);
        }
        if ((navAgent.transform.position - player.transform.position).magnitude < stoppingDistance) {
            navAgent.Stop();
        }
        print((navAgent.transform.position - player.transform.position).magnitude);
    }
}
