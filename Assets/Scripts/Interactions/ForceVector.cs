using UnityEngine;

public class ForceVector : MonoBehaviour {
	public Transform target;
	public Color impactColor = Color.white;
	public float forceMultiplier = 2f;
	public float rechargeTime = .5f;
	ColorStack colorStack;
	bool isChangedColor;
	
	float lastTimeForceApplied = -999999f;

	void Start () {
		colorStack = gameObject.GetComponent<ColorStack>();
	}
	
	void OnCollisionEnter2D (Collision2D coll) {
		Collider2D collider = coll.collider;
	
		// If the thing we hit was the player, send it flying!
		if (target != null && collider.gameObject.CompareTag("Player")) {
			if ((Time.time - lastTimeForceApplied) > rechargeTime) {
				Rigidbody2D body = collider.gameObject.GetComponent<Rigidbody2D>();
				Vector2 force = new Vector2(target.transform.localPosition.x, target.transform.localPosition.y);
				force *= forceMultiplier;
				body.AddForce(force, ForceMode2D.Impulse);
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
		colorStack.Remove(this);
	
		if (target != null) {
			Destroy(target.gameObject);
		}
	}
}

