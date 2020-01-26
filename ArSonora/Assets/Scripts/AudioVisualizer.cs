using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioVisualizer : MonoBehaviour
{
    public GameObject prefab;
    public GameObject parent;
    public int numberOfObjects = 64;
    public AudioSource src;
    public float radius = 5f;
    public float scale = 100f;
    public int range = 1024;
    public int updateSpeed = 480;
    public int rotateSpeed = 0;
	
    private GameObject[] cubes;

    void Start ()
    {
        cubes = new GameObject[numberOfObjects];
        for (int i = 0; i < numberOfObjects; i++) {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            Vector3 pos = new Vector3 (Mathf.Cos (angle), 0, Mathf.Sin (angle)) * radius;
            GameObject tmp = Instantiate(prefab, pos, Quaternion.identity);
            tmp.transform.parent = parent.transform;
            cubes[i] = tmp;
        }
    }

    void Update () {
        float[] spectrum = new float[range];
        src.GetSpectrumData (spectrum, 0, FFTWindow.Hanning);
        for (int i = 0; i < cubes.Length; i++) {
            cubes [i].transform.localScale = new Vector3(cubes[i].transform.localScale.x, spectrum [i] * scale * 50, cubes[i].transform.localScale.x);;
            cubes[i].transform.position = new Vector3(cubes[i].transform.position.x, (spectrum [i] * scale)/2f, cubes[i].transform.position.z);
            cubes[i].GetComponent<Renderer>().material.SetColor("_Color", Color.HSVToRGB(0.67f-(spectrum [i]*4f), 1, 1));
        }
        for (int i = 0; i < numberOfObjects; i++) {
            cubes[i].transform.Rotate (0, rotateSpeed, 0);		
        }
    }
}