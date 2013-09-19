using UnityEngine;
using System.Collections;

namespace AssemblyCSharp {
public class MouseOrbitController : MonoBehaviour {
	// The thing the camera will look at
	public Transform target;
	private Vector3 oldTargetPosition;
	
	public float xSensitivity = 1f;
	public float ySensitivity = 1f;
	public float scrollSensitivity = 1f;
	
	public float maximumDistance = 100f;
	public float minimumDistance = 5f;
	public float verticalRotationLimit = 1f;	
	
	// Use this for initialization
	void Start () {
		transform.LookAt(target.transform);
		oldTargetPosition = target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			performTranslation();
			performRotation();
			performZoom();
		}
	}
	
	private void performTranslation() {
		Vector3 currentTargetPosition = target.position;
		
		transform.position += (currentTargetPosition - oldTargetPosition);
		
		oldTargetPosition = currentTargetPosition;
	}
	
	private void performZoom() {
		float maxSqrDistance = maximumDistance * maximumDistance;
		float minSqrDistance = minimumDistance * minimumDistance;
		
		Vector3 difference = transform.position - target.position;
		Vector3 direction = difference.normalized;
		
		// Move according to the scroll wheel
		Vector3 move = direction * -Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * scrollSensitivity;
		transform.position += move;
		
		// Check limits
		float sqrDistance = (transform.position - target.position).sqrMagnitude;
		if (sqrDistance < minSqrDistance) {
			transform.position = target.transform.position + difference.normalized * minimumDistance;
		}
		else if (sqrDistance > maxSqrDistance) {
			transform.position = target.transform.position + difference.normalized * maximumDistance;	
		}
	}
	
	private void performRotation() {
		Vector3 difference = target.position - transform.position;
		Vector3 skewer = Vector3.Cross(difference, Vector3.up);
		float angleToVertical = Vector3.Angle(difference, Vector3.up);
		
		float degreesToRotateY = Input.GetAxis("Mouse Y") * Time.deltaTime * ySensitivity;
		
		if (angleToVertical - degreesToRotateY < verticalRotationLimit) {
			degreesToRotateY = angleToVertical - verticalRotationLimit;	
		}
		
		if (angleToVertical - degreesToRotateY > 180 - verticalRotationLimit) {
			degreesToRotateY = angleToVertical - 180 + verticalRotationLimit;	
		}
		
		transform.RotateAround(target.position, skewer, degreesToRotateY);
		transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * xSensitivity);
	}
}
}
