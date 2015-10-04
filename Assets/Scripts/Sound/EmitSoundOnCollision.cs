using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class EmitSoundOnCollision : MonoBehaviour {
	public float volumeMax = 6f;
	public float volumeMin = 1f;
	public AudioClip[] audioClips;

	void OnCollisionEnter2D (Collision2D collision) {
		Vector3 collisionNormal = collision.contacts[0].normal;
		Vector3 relativeVelocity = collision.relativeVelocity;
		Vector3 projected = Vector3.Project(relativeVelocity, collisionNormal);
		Vector2 projected2 = new Vector2(projected.x, projected.y);
		float impact = projected2.magnitude;
		if (impact > volumeMin) {
			impact -= volumeMin;
			float maxImpact = volumeMax - volumeMin;
			float volumeScale = Mathf.Max(1.0f, impact / maxImpact);
			GetComponent<AudioSource>().PlayOneShot(randomAudioClip(), volumeScale);
		}
	}

	void OnCollisionEnter (Collision collision) {
		/*
		Vector3 collisionNormal = collision.contacts[0].normal;
		Vector3 relativeVelocity = collision.relativeVelocity;
		Vector3 projected = Vector3.Project(relativeVelocity, collisionNormal);
		if (projected.magnitude > enterVelocityThreshold) {
			GetComponent<AudioSource>().PlayOneShot(randomAudioClip());
		}
		*/
	}

	private AudioClip randomAudioClip () {
		return audioClips[Random.Range(0, audioClips.Length)];
	}

	void OnCollisionStay () {

	}
}
