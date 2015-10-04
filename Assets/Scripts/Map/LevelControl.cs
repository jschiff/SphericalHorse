using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp {
public class LevelControl : MonoBehaviour {
	private GridBuilder gridBuilder = null;
	private PNGImporter pngImporter = null;
	private int currentMap = 0;
	public int startingMap = 0;
	
	public string dictionary;
	public string[] maps;
	public string[] prefabNames;
	public GameObject[] prefabs;
	private Vector3 startingPosition = Vector3.zero;
	
	// Mappings from map block type to Prefabs
	public Dictionary<string, GameObject> prefabMappings;
	
	// Use this for initialization
	void Start () {
		gameObject.AddComponent<GridBuilder>();
		gameObject.AddComponent<PNGImporter>();
		gridBuilder = gameObject.GetComponent<GridBuilder>();
		pngImporter = gameObject.GetComponent<PNGImporter>();
		initPrefabMap(this.prefabNames, this.prefabs);
		
		loadMap(startingMap);
	}
	
	private void initPrefabMap (string[] prefabNames, GameObject[] prefabs) {
		prefabMappings = new Dictionary<string, GameObject>();
		for (int i = 0; i < prefabNames.Length; i++) {
			prefabMappings[prefabNames[i]] = prefabs[i];
		}
	}
	
	public void loadMap (int mapNumber) {
		if (mapNumber < 0 || mapNumber >= maps.Length) {
			return;
		}
	
		currentMap = mapNumber;
		pngImporter.dictionaryFile = this.dictionary;
		pngImporter.mapFile = maps[mapNumber];
		
		var map = pngImporter.getMatrix();
		gridBuilder.prefabs = this.prefabMappings;
		startingPosition = gridBuilder.generate(map).transform.position;
	}
	
	void pause () {
		Time.timeScale = 0;
	}
		
	void invertTimeScale () {
		Time.timeScale = Time.timeScale == 0.0f ? 1.0f : 0.0f;
	}
		
	void resetPlayerLocation () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.transform.position = startingPosition;
		Rigidbody body = player.GetComponent<Rigidbody>();
		body.velocity = Vector3.zero;
		body.angularVelocity = Vector3.zero;
	}
		
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			invertTimeScale();
		}
			
		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			//pause();
			resetPlayerLocation();
		}
	}
}
}
