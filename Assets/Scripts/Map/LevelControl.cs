using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp {
public class LevelControl : MonoBehaviour {
	private bool gravityStarted = false;
	private GridBuilder gridBuilder = null;
	private PNGImporter pngImporter = null;
	private int currentMap = 0;
	public int startingMap = 0;
	
	public string dictionary;
	public string[] maps;
	
	public string[] prefabNames;
	public GameObject[] prefabs;
	
	// Mappings from map block type to Prefabs
	public Dictionary<string, GameObject> prefabMappings;
	
	// Use this for initialization
	void Start () {
		setGravityControllersActive(false);
		
		gameObject.AddComponent<GridBuilder>();
		gameObject.AddComponent<PNGImporter>();
		gridBuilder = gameObject.GetComponent<GridBuilder>();
		pngImporter = gameObject.GetComponent<PNGImporter>();
		initPrefabMap(this.prefabNames, this.prefabs);
		
		pngImporter.dictionaryFile = dictionary;
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
		gridBuilder.generate(map);
	}
		
	private void setGravityControllersActive (bool active) {
		Object[] gravityControllers = FindObjectsOfType(typeof(GravityController));
		foreach (Object o in gravityControllers) {
			GravityController gc = (GravityController)o;
			gc.enabled = active;
		}
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
