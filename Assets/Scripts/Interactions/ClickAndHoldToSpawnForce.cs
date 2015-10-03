using UnityEngine;

public class ClickAndHoldToSpawnForce : MonoBehaviour {
	public GameObject prefabToSpawn;
	ForceVector forceVector;
	public float maxSpawnDistance = 50f;
	GameObject previewTarget = null;
	bool isPlacingorceVector = false;
	
	// Update is called once per frame
	void Update () {
		if (isPlacingorceVector && previewTarget != null) {
			previewTarget.transform.position = mousePositionInWorldOnZPlane();
		}
	}
	
	void OnMouseDown () {
		// Destroy existing target if exists.
		if (forceVector != null) {
			Destroy(forceVector);
			forceVector = null;
		}
		
		if (!Input.GetKey(KeyCode.LeftControl)) {
			// Spawn new target.
			isPlacingorceVector = true;
			previewTarget = (GameObject)Instantiate(prefabToSpawn);
			previewTarget.transform.position = mousePositionInWorldOnZPlane();
			previewTarget.transform.parent = transform;
		}
	}
	
	void OnMouseUp () {
		if (isPlacingorceVector) {
			forceVector = gameObject.AddComponent<ForceVector>();
			forceVector.target = previewTarget.transform;
			previewTarget = null;
			isPlacingorceVector = false;
		}
	}
	
	Vector3 mousePositionInWorldOnZPlane () {
		Camera camera = Camera.main;
		// Z axis normal plane
		Plane plane = new Plane(new Vector3(0, 0, -1), 0f);
		Ray ray = camera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane));
		float distanceOfIntersection;
		plane.Raycast(ray, out distanceOfIntersection);
		Vector3 mousepos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceOfIntersection);
		return camera.ScreenToWorldPoint(mousepos);
	}
}
