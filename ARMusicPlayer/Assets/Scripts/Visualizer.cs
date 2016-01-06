using UnityEngine;
using System.Collections;

public class Visualizer : MonoBehaviour {
	// The below are about the physical display.
	public float radius = 5.0f;
	public int numberOfObjects = 64;
	public GameObject prefab;
	GameObject[] cylinders;
	public Material mat;


	// This part is for the spectrum
	public float amplitude = 20f;
	AudioSource audio;
	private float[] sampleSpectrum;

	public float interval = 0f;
	float nextTime = 0; 

	void Awake() {
		sampleSpectrum = new float[numberOfObjects];
		audio = transform.GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {

		for (int i = 0; i < numberOfObjects; i++) {
			float angle = i * Mathf.PI * 2 / (numberOfObjects * 1.25f);
			Vector3 pos = new Vector3 (Mathf.Cos (angle), 0, Mathf.Sin (angle)) * radius + transform.position;
			GameObject clone = Instantiate (prefab, pos, Quaternion.identity) as GameObject;
			clone.transform.parent = transform;
		}
		cylinders = GameObject.FindGameObjectsWithTag ("Cylinder");

	}
	
	// Update is called once per frame
	void Update () {
		audio.GetSpectrumData (sampleSpectrum, 0, FFTWindow.BlackmanHarris);
		if (Time.time >= nextTime) {
			for (int i = 0; i < cylinders.Length; i++) {
				Vector3 previousScale = cylinders [i].transform.localScale;
				previousScale.y = sampleSpectrum [i] * amplitude;
				cylinders [i].transform.localScale = previousScale;
			}
			nextTime += interval;
		}
	}
}
