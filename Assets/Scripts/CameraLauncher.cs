using UnityEngine;
using System.Collections;

namespace AssemblyCSharp {
/**
 * This will be attached to the camera, and the rigidbody will be the body to launch.
 * Launches in the direction the camera is pointing.
 */
public class CameraLauncher : MonoBehaviour {
	public Rigidbody body;
	public float speedOfLaunch;
	public bool oneOff = true;
	private bool firedOnce = false;
		
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!(oneOff && firedOnce)) {
			if (body != null && Input.GetMouseButtonUp(0)) {
				body.velocity += (speedOfLaunch * transform.forward);
				firedOnce = true;
			}
		}
	}
}
}
