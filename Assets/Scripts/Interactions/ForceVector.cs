using UnityEngine;

public class ForceVector : MonoBehaviour {
	public Transform target;
	public Color impactColor = Color.white;
	public float forceMultiplier = 1f;
	public float rechargeTime = .5f;
	ColorStack colorStack;
	bool isChangedColor;
	
	float lastTimeForceApplied = -999999f;

	void Start () {
		colorStack = gameObject.GetComponent<ColorStack>();
	}
	
	void OnTriggerEnter (Collider collider) {
		// If the thing we hit was the player, send it flying!
		if (target != null && collider.gameObject.CompareTag("Player")) {
			if ((Time.time - lastTimeForceApplied) > rechargeTime) {
				Rigidbody body = collider.gameObject.GetComponent<Rigidbody>();
				body.AddForce(target.localPosition * forceMultiplier, ForceMode.Impulse);
				lastTimeForceApplied = Time.time;
				
				colorStack.Add(this, impactColor);
				isChangedColor = true;
			}
		}
	}
	
	void Update () {
		if (isChangedColor && (Time.time - lastTimeForceApplied) > rechargeTime) {
			colorStack.Remove(this);
			isChangedColor = false;
		}
	}
	
	void OnDrawGizmos () {
		if (target != null) {
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(transform.position, target.transform.position);
		}
	}
	
	void OnDestroy () {
		if (target != null) {
			Destroy(target.gameObject);
		}
	}
}

