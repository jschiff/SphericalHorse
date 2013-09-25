﻿using UnityEngine;
using System.Collections;

public class GoalTrigger : MonoBehaviour {
	public Collider playerCollider;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
    void OnTriggerEnter(Collider other) {
		if (other == playerCollider) {
			Destroy(other.gameObject);
		}
    }
}
