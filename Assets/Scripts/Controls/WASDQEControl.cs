using UnityEngine;
using System.Collections;

/**
 * Three dimensional control via WASDQE keys
 */
namespace AssemblyCSharp {
public class WASDQEControl : MonoBehaviour {
	public Rigidbody body;
	public float forceMultiplier = 1f;
	public bool ignorePhysics = false;
	public bool xAxis = true;
	public bool yAxis = true;
	public bool zAxis = false;
	static IDictionary keyMappings = new Hashtable();
	
	// Use this for initialization
	void Start () {
		if (xAxis) {
			keyMappings["Horizontal"] = Vector3.right;
			//keyMappings[KeyCode.A] = Vector3.left;
			//keyMappings[KeyCode.D] = Vector3.right;
		}
		
		if (yAxis) {
			keyMappings["Vertical"] = Vector3.up;
			//keyMappings[KeyCode.W] = Vector3.up;
			//keyMappings[KeyCode.S] = Vector3.down;
		}
		
		if (zAxis) {
			keyMappings[KeyCode.Q] = Vector3.forward;
			keyMappings[KeyCode.E] = Vector3.back;
		}
	}
	
	// Update is called once per frame
	void Update () {
		foreach (string axisName in keyMappings.Keys) {
			float axisValue = Input.GetAxis(axisName);
			if (axisValue != 0.0f) {
				Debug.Log(axisName + " " + axisValue);
				Vector3 vec = (Vector3)keyMappings[axisName] * axisValue;
								
				if (ignorePhysics) {
					body.velocity = Vector3.zero;
					body.angularVelocity = Vector3.zero;
					body.MovePosition(body.position + vec);
				} else {
					body.AddForce(vec * Time.deltaTime * forceMultiplier);	
				}
			}
		}
						
		/*
						foreach (KeyCode key in keyMappings.Keys) {
								if (Input.GetKey (key)) {
										Vector3 vec = (Vector3)keyMappings [key] * forceMultiplier * timeElapsed;
				
										if (ignorePhysics) {
												body.velocity = Vector3.zero;
												body.angularVelocity = Vector3.zero;
												body.MovePosition (body.position + vec);
										} else {
												body.AddForce (vec);	
										}
								}
						}
						*/
	}
}
}
