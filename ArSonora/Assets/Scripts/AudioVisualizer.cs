using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioVisualizer : MonoBehaviour
{
	public GameObject prefab;
	public int numberOfObjects = 20;
	public GameObject[] cubes;
	public AudioSource src;
	public float radius = 5f;
	public float scale = 100f;
	public int range = 1024;
	public int updateSpeed = 480;
	public int rotateSpeed = 1000;

	void Start ()
	{
		for (int i = 0; i < numberOfObjects; i++) {
			float angle = i * Mathf.PI * 2 / numberOfObjects;
			Vector3 pos = new Vector3 (Mathf.Cos (angle), 0, Mathf.Sin (angle)) * radius;
			Instantiate(prefab, pos, Quaternion.identity);
			
		}
		cubes = GameObject.FindGameObjectsWithTag("Cube");
	}

	void Update () {
		float[] spectrum = new float[range];
		src.GetSpectrumData (spectrum, 0, FFTWindow.Hamming);
		for (int i = 0; i < cubes.Length; i++) {
			Vector3 previousScale = cubes [i].transform.localScale;
			previousScale.y = Mathf.Lerp (previousScale.y, spectrum [i] * scale, Time.deltaTime * updateSpeed);
			cubes[i].transform.localScale = previousScale;
		}
		for (int i = 0; i < numberOfObjects; i++) {
			cubes [i].transform.Rotate (0, rotateSpeed, 0);		
		}
	}
}