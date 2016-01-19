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
	public float amplitude;
	AudioSource audio;
	private float[] sampleSpectrum;

	float nextTime = 0; 

	float r,g,b;
	//public GameObject part;

	void Awake() {
		sampleSpectrum = new float[numberOfObjects];
		audio = transform.GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		r = PlayerPrefs.GetFloat("R");
		g = PlayerPrefs.GetFloat("G");
		b = PlayerPrefs.GetFloat("B");

		for (int i = 0; i < numberOfObjects; i++) {
			float angle = i * Mathf.PI * 2 / (numberOfObjects * 1.25f) - 90;
			Vector3 pos = new Vector3 (Mathf.Cos (angle), 0, Mathf.Sin (angle)) * radius + transform.position;
			GameObject clone = Instantiate (prefab, pos, Quaternion.identity) as GameObject;
			clone.transform.parent = transform;
		}
		cylinders = GameObject.FindGameObjectsWithTag ("Cylinder");

		Transform particlePos = transform.FindChild("ParticleLocation");
		//part.Play ();
		//part.transform.position = particlePos.position;
		//GameObject partClone =Instantiate(part,particlePos.position,Quaternion.identity) as GameObject;
		//partClone.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {
		audio.GetSpectrumData (sampleSpectrum, 0, FFTWindow.BlackmanHarris);
			for (int i = 0; i < cylinders.Length; i++) {
				Vector3 previousScale = cylinders [i].transform.localScale;
				previousScale.y = sampleSpectrum [i] * amplitude;
				cylinders [i].transform.localScale = Vector3.Lerp(cylinders [i].transform.localScale, previousScale, Time.deltaTime * 10);

			}
	}

	void OnGUI(){
		r = GUI.HorizontalSlider (new Rect (20, 10, Screen.width - 40, 20), r, 0.0f, 1.0f);
		g = GUI.HorizontalSlider (new Rect (20, 30, Screen.width - 40, 20), g, 0.0f, 1.0f);
		b = GUI.HorizontalSlider (new Rect (20, 50, Screen.width - 40, 20), b, 0.0f, 1.0f);

		PlayerPrefs.SetFloat ("R", r);
		PlayerPrefs.SetFloat ("G", g);
		PlayerPrefs.SetFloat ("B", b);

		mat.color = new Color (r, g, b);
	}
}
