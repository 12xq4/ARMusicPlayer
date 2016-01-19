using UnityEngine;
using System.Collections;

public class CylinderRelocate : MonoBehaviour {
	Vector3 originalPos;
	// Use this for initialization
	void Awake() {
		originalPos = transform.position;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 scale = transform.localScale;
		transform.position = originalPos;
		if (scale.y > 0) {
			transform.Translate (new Vector3 (0,transform.GetComponent<Renderer>().bounds.size.y/2,0));
		}
	}
}
