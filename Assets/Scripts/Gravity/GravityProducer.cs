using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class GravityProducer : MonoBehaviour
	{
		public float Range;
		public void actOn(GravityConsumer consumer) {
			var difference = rigidbody.position - consumer.rigidbody.position;
			float sqrDistance = difference.sqrMagnitude;
			var gravityForce = rigidbody.mass * consumer.rigidbody.mass * GravityController.G * difference * (1/sqrDistance);
			consumer.rigidbody.AddForce(gravityForce);
		}
	}
}

