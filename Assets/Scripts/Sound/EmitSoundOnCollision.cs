using UnityEngine;
using System.Collections;

public class EmitSoundOnCollision : MonoBehaviour {
    public float enterVelocityThreshold = 1;
    public AudioClip[] audioClips;

	void OnCollisionEnter(Collision collision) {
        Vector3 collisionNormal = collision.contacts[0].normal;
        Vector3 relativeVelocity = collision.relativeVelocity;
        Vector3 projected = Vector3.Project(relativeVelocity, collisionNormal);
        if (projected.magnitude > enterVelocityThreshold ) {
            GetComponent<AudioSource>().PlayOneShot(randomAudioClip());
        }
    }

    private AudioClip randomAudioClip() {
        return audioClips[Random.Range(0, audioClips.Length)];
    }

    void OnCollisionStay() {

    }
}
