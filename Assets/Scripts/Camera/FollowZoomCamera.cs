using UnityEngine;
using System.Collections;

public class FollowZoomCamera : MonoBehaviour {
	Quaternion initialRotation;

	// Use this for initialization
	void Start () {
		initialRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = initialRotation;
	}
}
