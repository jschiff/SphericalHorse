// Manages 
using UnityEngine;
using System.Collections.Generic;
public class ColorStack : MonoBehaviour {
	Dictionary<object, Color> members = new Dictionary<object, Color>();
	Color current;
	
	public Color CurrentColor {
		get { return current; }
	}

	public void Add (object owner, Color color) {
		if (members.ContainsKey(owner)) {
			members.Remove(owner);
		}
		
		members.Add(owner, color);
		computeCurrentColor();
	}
	
	void computeCurrentColor () {
		Color total = new Color(0, 0, 0, 0);
		foreach (Color color in members.Values) {
			total += color;
		}
		
		total /= members.Values.Count;
		current = total;
		MeshRenderer renderer = GetComponent<MeshRenderer>();
		renderer.material.color = CurrentColor;
	}
	
	public void Remove (object owner) {
		members.Remove(owner);
		computeCurrentColor();
	}

	void Start () {
		MeshRenderer renderer = GetComponent<MeshRenderer>();
		Add(this, renderer.material.color);
	}
}

