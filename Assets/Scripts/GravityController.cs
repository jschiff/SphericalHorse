using UnityEngine;
using System.Collections;

namespace AssemblyCSharp {
public class GravityController : MonoBehaviour {
	private ArrayList gravityProducers = new ArrayList();
	private ArrayList gravityConsumers = new ArrayList();
	
	// The gravitational constant of our world.
	public const float G = 1.043f;
	
	void Start () {
		enabled = false;
		Object[] producers = FindObjectsOfType(typeof(GravityProducer));
		foreach(GravityProducer producer in producers) {
			gravityProducers.Add(producer);
		}
		
		Object[] consumers = FindObjectsOfType(typeof(GravityConsumer));
		foreach(GravityConsumer consumer in consumers) {
			gravityConsumers.Add(consumer);
		}
	}
	
	// Apply each gravity producer to each gravity consumer
	void FixedUpdate() {
		foreach(GravityProducer producer in gravityProducers) {
			foreach(GravityConsumer consumer in gravityConsumers) {
					
				float range = producer.Range;
				Vector3 difference = producer.rigidbody.position - consumer.rigidbody.position;
				float sqrRange = range * range;
					
				if (difference.sqrMagnitude < sqrRange) {
					producer.actOn(consumer);
					consumer.reactTo(producer);
				}
			}
		}
	}
}
}
