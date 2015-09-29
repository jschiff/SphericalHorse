using UnityEngine;
using System.Collections;

public class ClickAndHoldToSpawnForce : MonoBehaviour
{
	public GameObject prefabToSpawn;
	public float maxSpawnDistance = 50f;
	public float forceMultiplier = 1f;
	public float rechargeTime = .5f;
	GameObject previewTarget = null;
	GameObject spawnedVectorTarget = null;
	bool isPlacingorceVector = false;
	float lastTimeForceApplied = -999999f;
	
	// Update is called once per frame
	void Update ()
	{
		if (isPlacingorceVector && previewTarget != null) {
			previewTarget.transform.position = mousePositionInWorldOnZPlane ();
			Debug.Log ("Update at " + previewTarget.transform.position);
		}
	}
	
	void OnCollisionEnter (Collision collision)
	{
		// If the thing we hit was the player, send it flying!
		if (spawnedVectorTarget != null && collision.gameObject.CompareTag ("Player")) {
			if ((Time.time - lastTimeForceApplied) > rechargeTime) {
				Rigidbody body = collision.gameObject.rigidbody;
				body.AddForce (spawnedVectorTarget.transform.localPosition * forceMultiplier);
				lastTimeForceApplied = Time.time;
			}
		}
	}
	
	void OnDrawGizmos ()
	{
		if (spawnedVectorTarget != null) {
			Gizmos.color = Color.blue;
			Gizmos.DrawLine (transform.position, spawnedVectorTarget.transform.position);
		}
	}
	
	void OnMouseDown ()
	{
		// Destroy existing target if exists.
		if (spawnedVectorTarget != null) {
			Destroy (spawnedVectorTarget);
			spawnedVectorTarget = null;
		}
		
		if (!Input.GetKey (KeyCode.LeftControl)) {
			// Spawn new target.
			isPlacingorceVector = true;
			previewTarget = (GameObject)Instantiate (prefabToSpawn);
			previewTarget.transform.position = mousePositionInWorldOnZPlane ();
			previewTarget.transform.parent = transform;
			Debug.Log ("Spawned at" + previewTarget.transform.position);
		}
	}
	
	void OnMouseUp ()
	{
		if (isPlacingorceVector) {
			spawnedVectorTarget = previewTarget;
			previewTarget = null;
			isPlacingorceVector = false;
			Debug.Log ("Dropped target at " + spawnedVectorTarget.transform.position);
		}
	}
	
	Vector3 mousePositionInWorldOnZPlane ()
	{
		Camera camera = Camera.main;
		// Z axis normal plane
		Plane plane = new Plane (new Vector3 (0, 0, -1), 0f);
		Ray ray = camera.ScreenPointToRay (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane));
		float distanceOfIntersection;
		plane.Raycast (ray, out distanceOfIntersection);
		Vector3 mousepos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distanceOfIntersection);
		return camera.ScreenToWorldPoint (mousepos);
	}
}
