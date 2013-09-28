using UnityEngine;
using System.IO;
using System.Collections;

public class GridBuilder : MonoBehaviour {
	// Objects to replicate.
	public GameObject[] prefabs;
	// Determines direction and spacing for the x axis
	public Vector3 xAxis = new Vector3(0, 0, -1);
	// Determines direction and spacing for the y axis
	public Vector3 yAxis = Vector3.up;
	// The object at 0, 0 will be here.
	public Vector3 growFrom;
	// Map with object ids to determine which objects go in which location.  
	// Values in this map are used as indices to the objects array above.
	public long[,] map;
	
	// Use this for initialization
	void Start () {
		map = new long[,]{
			{-1, -1, 0, 0, 0, 0, 0, 0},
			{0, 0, 0, 0, 0, 0, 0, -1}
		};
		
		if (map == null || prefabs == null) return;
		int width = map.GetLength(0);
		int height = map.GetLength(1);
		
		Vector3[,] worldPositions = generatePositions(width, height, xAxis, yAxis);
		InstantiatePrefabs(worldPositions, map, prefabs);
	}
	
	private Vector3[,] generatePositions(int width, int height, Vector3 xAxis, Vector3 yAxis) {
		Vector3[,] worldPositions = new Vector3[map.GetLength(0), map.GetLength(1)];
		
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				worldPositions[i, j] = growFrom + (i * xAxis) + (j * yAxis);
			}
		}
		
		return worldPositions;
	}
	
	private void InstantiatePrefabs(Vector3[,] worldPositions, long[,] map, GameObject[] objectDictionary) {
		int width = map.GetLength(0);
		int height = map.GetLength(1);
		
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				long mapValue = map[i, j];
				if (mapValue < 0) continue;
				print(mapValue);
				print(objectDictionary);
				GameObject prefab = objectDictionary[mapValue];
				if (prefab != null) {
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
