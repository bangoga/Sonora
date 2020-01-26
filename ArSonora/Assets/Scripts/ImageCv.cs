using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ImageCv : MonoBehaviour
{

    public GameObject prefabToCreate;
    public List<GameObject> allBars;
    public GameObject boomBox;
    public GameObject boomBox2;

    // Start is called before the first frame update
    void Start()
    {
        //instantiate the bars in group of 64
        // Want to also rotate it just a bit?

        
        for (int i = 0; i < 64; i++)
        {
            string name = "bar_" + i;
            GameObject newBar = Instantiate(prefabToCreate, new Vector3(i * 0.2F, 0, 0), Quaternion.identity);
            newBar.name = name;
            allBars.Add(newBar);
        }

        StartCoroutine(changeMatColor());
    }

    void Update()
    {
        pingPongboomBox();
    }

    IEnumerator changeMatColor()
    {
        // Wrap it in a small wait
        while (true)
        {
            yield return new WaitForSeconds(1);
            //Randomly generate a hexcode:
            Color barColor = new Color(
             UnityEngine.Random.Range(0f, 1f),
             UnityEngine.Random.Range(0f, 1f),
             UnityEngine.Random.Range(0f, 1f)
         );
            // get the list of all the cubes and make their colors change
            foreach (GameObject bar in allBars)
            {
                bar.GetComponent<Renderer>().material.SetColor("_Color", barColor);
            }
        }
    }


    void pingPongboomBox()
    {
        // The max range in which it can pingpong between? Do i control the change of the scale
        Transform[] spheres_1 = boomBox.GetComponentsInChildren<Transform>();
        Transform[] spheres_2 = boomBox2.GetComponentsInChildren<Transform>();


        float scale = Mathf.PingPong(Time.time , 4);

        // Use 2 and 3 for the s
        spheres_1[3].localScale = new Vector3(scale + 0.2f, scale+ 0.2f, scale+ 0.2f);
    }
}
