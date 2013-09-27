using UnityEngine;
using System.Collections;

public class GridBuilder : MonoBehaviour {
	// Objects to replicate.
	public GameObject[] objects;
	// Determines direction and spacing for the x axis
	public Vector3 xAxis;
	// Determines direction and spacing for the y axis
	public Vector3 yAxis;
	// The object at 0, 0 will be here.
	public Vector3 growFrom;
	// Map with object ids to determine which objects go in which location.  
	// Values in this map are used as indices to the objects array above.
	public long[,] map;
	
	// Use this for initialization
	void Start () {
		int width = map.GetLength(0);
		int height = map.GetLength(1);
		Vector3[,] worldPositions = new Vector3[map.GetLength(0), map.GetLength(1)];
		
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				worldPositions[i, j] = growFrom + (i * xAxis) + (j * yAxis);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
