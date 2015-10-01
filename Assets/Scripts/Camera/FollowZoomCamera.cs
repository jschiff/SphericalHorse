using UnityEngine;
using System.Collections;

public class FollowZoomCamera : MonoBehaviour
{
	Quaternion initialRotation;

	// Use this for initialization
	void Start ()
	{
        GameObject tempcam = GameObject.FindGameObjectWithTag("TempCamera");
        tempcam.SetActive(false);
        //Destroy(tempcam);

        this.gameObject.SetActive(true);
		initialRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.rotation = initialRotation;
	}
}
