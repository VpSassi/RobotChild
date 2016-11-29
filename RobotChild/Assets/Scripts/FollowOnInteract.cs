using UnityEngine;
using System.Collections;

public class FollowOnInteract : MonoBehaviour {

    public float stoppingDistance;

    GameObject player;
    NavMeshAgent navAgent;





	void Start () {
        player = GameObject.Find("robotChild");
        navAgent = GetComponent<NavMeshAgent>();
	}
	
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

    void OnTriggerEnter (Collider other) {

    }

}
