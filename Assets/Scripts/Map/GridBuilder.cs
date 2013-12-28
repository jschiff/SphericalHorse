using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp {
public class GridBuilder : MonoBehaviour {
	// Determines direction and spacing for the x axis
	public Vector3 xAxis = Vector3.right;
	// Determines direction and spacing for the y axis
	public Vector3 yAxis = Vector3.down;
	// The object at 0, 0 will be here.
	public Vector3 growFrom = Vector3.zero;
	
	public Dictionary<string, GameObject> prefabs;
	
	// Use this for initialization
	void Start () {
	}
	
	public void generate (string[,] map) {
		if (map == null || prefabs == null) {
			return;
		}
			
		Vector3[,] worldPositions = generatePositions(map);
		InstantiatePrefabs(worldPositions, map, prefabs);
	}
	
	private Vector3[,] generatePositions (string[,] map) {
		int width = map.GetLength(0), height = map.GetLength(1);
		Vector3[,] worldPositions = new Vector3[width, height];
		
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				worldPositions[i, j] = worldPosition(i, j);
			}
		}
		
		return worldPositions;
	}

	// Calculate the world position of a grid position on the fly
	public static Vector3 worldPosition (int x, int y, Vector3 xAxis, Vector3 yAxis, Vector3 growFrom) {
		return growFrom + (x * xAxis) + (y * yAxis);
	}

	// Calculate the world position of a grid position on the fly.
	private Vector3 worldPosition (int x, int y) {
		return worldPosition(x, y, this.xAxis, this.yAxis, this.growFrom);
	}
	
	private void InstantiatePrefabs (Vector3[,] worldPositions, string[,] map, Dictionary<string, GameObject>objectDictionary) {
		int width = map.GetLength(0);
		int height = map.GetLength(1);
		
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				string mapValue = map[i, j];
				
				GameObject prefab;
				if (mapValue != null && objectDictionary.TryGetValue(mapValue, out prefab)) {
					GameObject instance = (GameObject)Instantiate(prefab);
					instance.transform.position = worldPositions[i, j];
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
}