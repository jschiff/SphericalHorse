using UnityEngine;
using System.Collections;

/**
 * Launches a rigidbody in the direction of a point in the world where a given plane intersects the ray cast
 * from the camera to a point on the screen clicked by the user.
 */
public class PlaneIntersectLauncher : MonoBehaviour {
	public float distance;
	public Vector3 normal;
	public int mouseButton;
	public float launchSpeed;
	public Rigidbody objectToLaunch;
	
	private Plane plane;
	
	// Use this for initialization
	void Start () {
		plane = new Plane(normal, distance);
	}
	
	// Update is called once per frame
	void Update () {
		if (objectToLaunch != null && camera != null && Input.GetMouseButtonUp(mouseButton)) {
			Ray castRay = camera.ScreenPointToRay(Input.mousePosition);
			float enter;
			
			// Find intersection
			if (plane.Raycast(castRay, out enter)) {
				// The intersection point is a point along the line at the distance the raycast returned.
				Vector3 destination = castRay.GetPoint(enter);
				Vector3 direction = destination - objectToLaunch.position;
				objectToLaunch.velocity += direction.normalized * launchSpeed;
			}
		}
	}
}
