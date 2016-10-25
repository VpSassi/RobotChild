using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	public Transform[] points;
	private int destPoint = 0;
	Vector3 direction;
	float remainingDistance;
	public float patrolSpeed;
	public float closeEnoughToWP = 0.5f;
	NavMeshAgent navAgent;

	// TODO: also support turning around at last WP?

	void Awake() {
		navAgent = GetComponent<NavMeshAgent>();
		destPoint = points.Length - 1;
		nextWp();		
	}

	void nextWp() {
		if (points.Length == 0)
			return;

		destPoint = (destPoint + 1) % points.Length;
		print(destPoint);
		navAgent.SetDestination(points[destPoint].position);
	}

	void Update() {
		direction = points[destPoint].position - transform.position;
		remainingDistance = direction.magnitude;

		if (remainingDistance < closeEnoughToWP) {
			nextWp();
		}

		// transform.position += direction.normalized * patrolSpeed * Time.deltaTime;
	}
}
