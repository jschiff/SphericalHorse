using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class GravityProducer : MonoBehaviour
	{
		public float Range;
		public void actOn(GravityConsumer consumer) {
			var difference = GetComponent<Rigidbody>().position - consumer.GetComponent<Rigidbody>().position;
			float sqrDistance = difference.sqrMagnitude;
			var gravityForce = GetComponent<Rigidbody>().mass * consumer.GetComponent<Rigidbody>().mass * GravityController.G * difference * (1/sqrDistance);
			consumer.GetComponent<Rigidbody>().AddForce(gravityForce);
		}
	}
}

