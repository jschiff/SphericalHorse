using UnityEngine;
using System.Collections;

namespace AssemblyCSharp {
public class LevelControl : MonoBehaviour {
	private bool objectsAreActive = false;
	private bool gravityStarted = false;
	
	// Use this for initialization
	void Start () {
		setGravityControllersActive(false);
	}
		
	private void setGravityControllersActive(bool active) {
		Object[] gravityControllers = FindObjectsOfType(typeof(GravityController));
		foreach (Object o in gravityControllers) {
			GravityController gc = (GravityController) o;
			gc.enabled = active;
		}
		
		objectsAreActive = active;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0)) {
			if (!gravityStarted) {
				gravityStarted = true;
				setGravityControllersActive(true);
			}
		}
	}
}
}
