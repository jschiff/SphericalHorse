using UnityEngine;

public class ForceVector : MonoBehaviour {
	public Transform target;
	public float forceMultiplier = 1f;
	public float rechargeTime = .5f;
	float lastTimeForceApplied = -999999f;

	public ForceVector() {
	}
	
	void OnCollisionEnter (Collision collision) {
		// If the thing we hit was the player, send it flying!
		if (target != null && collision.gameObject.CompareTag("Player")) {
			if ((Time.time - lastTimeForceApplied) > rechargeTime) {
				Rigidbody body = collision.gameObject.GetComponent<Rigidbody>();
				body.AddForce(target.localPosition * forceMultiplier, ForceMode.Impulse);
				lastTimeForceApplied = Time.time;
			}
		}
	}
	
	void OnDrawGizmos () {
		if (target != null) {
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(transform.position, target.transform.position);
		}
	}
}

