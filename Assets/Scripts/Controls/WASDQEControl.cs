using UnityEngine;
using System.Collections;

/**
 * Three dimensional control via WASDQE keys
 */
namespace AssemblyCSharp {
public class WASDQEControl : MonoBehaviour {
	public Rigidbody2D body;
	public float forceMultiplier = 1f;
	public bool ignorePhysics = false;
	public bool xAxis = true;
	public bool yAxis = true;
	public bool zAxis = false;
	static IDictionary keyMappings = new Hashtable();
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
		if (xAxis) {
			keyMappings["Horizontal"] = Vector2.right;
			//keyMappings[KeyCode.A] = Vector3.left;
			//keyMappings[KeyCode.D] = Vector3.right;
		}
		
		if (yAxis) {
			keyMappings["Vertical"] = Vector2.up;
			//keyMappings[KeyCode.W] = Vector3.up;
			//keyMappings[KeyCode.S] = Vector3.down;
		}
		
		if (zAxis) {
			keyMappings[KeyCode.Q] = Vector3.forward;
			keyMappings[KeyCode.E] = Vector3.back;
		}
	}
	
	void Update () {
		
		foreach (string axisName in keyMappings.Keys) {
			float axisValue = Input.GetAxis(axisName);
			if (axisValue != 0.0f) {
				Vector2 vec = (Vector2)keyMappings[axisName] * axisValue;
								
				if (ignorePhysics) {
					body.velocity = Vector2.zero;
					body.angularVelocity = 0f;
					body.MovePosition(body.position + vec);
				} else {
					body.AddForce(vec * Time.deltaTime * forceMultiplier);	
				}
			}
		}
	}
}
}
