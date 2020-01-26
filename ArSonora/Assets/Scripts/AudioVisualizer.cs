﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioVisualizer : MonoBehaviour
{
	public GameObject prefab;
	public GameObject parent;
	public int numberOfObjects = 64;
	public GameObject[] cubes;
	public AudioSource src;
	public float radius = 5f;
	public float scale = 100f;
	public int range = 1024;
	public int updateSpeed = 480;
	public int rotateSpeed = 1000;

	void Start ()
	{
		cubes = new GameObject[numberOfObjects];
		for (int i = 0; i < numberOfObjects; i++) {
			float angle = i * Mathf.PI * 2 / numberOfObjects;
			Vector3 pos = new Vector3 (Mathf.Cos (angle), 0, Mathf.Sin (angle)) * radius;
			GameObject cube = Instantiate(prefab, pos, Quaternion.identity);
			cube.transform.parent = parent.transform;
			cubes[i] = cube;
		}
	}

	void Update () {
		float[] spectrum = new float[range];
		src.GetSpectrumData (spectrum, 0, FFTWindow.Hanning);
		for (int i = 0; i < cubes.Length; i++) {
			Vector3 previousScale = cubes [i].transform.localScale;
			previousScale.y = Mathf.Lerp (previousScale.y, spectrum [i] * scale, Time.deltaTime * updateSpeed);
			cubes[i].transform.localScale = previousScale;
			cubes[i].transform.position = new Vector3(cubes[i].transform.position.x, previousScale.y/2f,cubes[i].transform.position.z);
			cubes[i].GetComponent<Renderer>().material.SetColor("_Color", Color.HSVToRGB(0.67f-(spectrum [i]*3.5f), 1, 1));
		}
		for (int i = 0; i < numberOfObjects; i++) {
			cubes [i].transform.Rotate (0, rotateSpeed, 0);		
		}
	}
}