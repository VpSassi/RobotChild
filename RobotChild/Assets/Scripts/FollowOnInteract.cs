using UnityEngine;
using System.Collections;

public class FollowOnInteract : MonoBehaviour {

    [HideInInspector]
    public bool readyToFollow;

    public float stoppingDistance;

    GameObject player;
    NavMeshAgent navAgent;





	void Start () {
        player = GameObject.Find("robotChild");
        navAgent = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
        if (readyToFollow == true) {
            if ((navAgent.transform.position - player.transform.position).magnitude > stoppingDistance) {
                navAgent.Resume();
                navAgent.SetDestination(player.transform.position);
            }
            if ((navAgent.transform.position - player.transform.position).magnitude < stoppingDistance) {
                navAgent.Stop();
            }
        }
        print(readyToFollow);
    }

    //"readyToFollow" is used in PlayerAbilities script to get the clones to follow the player
    void OnTriggerEnter (Collider other) {
        if (other.tag == "Player") {
            readyToFollow = true;
        }
    }

    //void OnTriggerExit(Collider other) {
    //    if (other.tag == "Player") {
    //        readyToFollow = false;
    //    }
    //}

}
