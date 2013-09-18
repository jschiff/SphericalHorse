using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class GravityProducer : MonoBehaviour
	{
		public Rigidbody Rigidbody;
		public float Range;
		public void actOn(GravityConsumer consumer) {
			var difference = Rigidbody.position - consumer.Rigidbody.position;
			var gravityForce = Rigidbody.mass * consumer.Rigidbody.mass * GravityController.G * difference;
			consumer.Rigidbody.AddForce(gravityForce);
		}
	}
}

