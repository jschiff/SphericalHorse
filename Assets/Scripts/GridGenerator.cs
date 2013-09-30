using UnityEngine;
using System.Collections;

/**
 * Generates a grid of prefabs based on an input matrix.
 * USE MINECRAFT AS A LEVEL EDITOR?
 **/
public class GridGenerator : MonoBehaviour {
	public int?[,,] matrix;
	public GameObject prefab;
	
	// Use this for initialization
	void Start () {
		if (prefab == null) return;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
