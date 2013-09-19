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
	
	private static IDictionary keyMappings = new Hashtable();
	
	// Use this for initialization
	void Start () {
		keyMappings[KeyCode.W] = Vector3.forward;
		keyMappings[KeyCode.A] = Vector3.left;
		keyMappings[KeyCode.S] = Vector3.back;
		keyMappings[KeyCode.D] = Vector3.right;
		keyMappings[KeyCode.Q] = Vector3.up;
		keyMappings[KeyCode.E] = Vector3.down;
	}
	
	// Update is called once per frame
	void Update () {
		float timeElapsed = Time.deltaTime;
		foreach (KeyCode key in keyMappings.Keys) {
			if (Input.GetKey(key)) {
				Vector3 vec = (Vector3)keyMappings[key] * forceMultiplier * timeElapsed;
				
				if (ignorePhysics) {
					body.velocity = Vector3.zero;
					body.angularVelocity = Vector3.zero;
					body.MovePosition(body.position + vec);
				}
				else {
					body.AddForce(vec);	
				}
			}
		}
	}
}
}
