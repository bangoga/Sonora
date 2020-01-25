using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class SpectrumScript : MonoBehaviour
{
    // I have an empty object of audiolistenr that is then linked to the current speuctrum script 
    public AudioSource audio;

    public float[] spectrum = new float[512];
    // Start is called before the first frame update
    void Start()
    {
        audio.clip = Microphone.Start(null, true, 100, 44100);
        audio.loop = true;
        while(!(Microphone.GetPosition(null) > 0)) {}
        Debug.Log("Start playing... position is: " + Microphone.GetPosition(null));
        UpdateMicrophone ();
    }

    void Update()
    {
        //debugVisualizer();
    }

    void debugVisualizer()
    {
        audio.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
        }
    }

    void UpdateMicrophone()
    {
        audio.Stop();
        //Start recording to audioclip from the mic
        audio.clip = Microphone.Start(null, true, 10, 44100);
        audio.loop = true;
        // Mute the sound with an Audio Mixer group becuase we don't want the player to hear it
        Debug.Log(Microphone.IsRecording(null).ToString());

        if (Microphone.IsRecording(null))
        {
            //check that the mic is recording, otherwise you'll get stuck in an infinite loop waiting for it to start
            while (!(Microphone.GetPosition(null) > 0))
            {
            } // Wait until the recording has started. 

            Debug.Log("recording started with " + Microphone.GetPosition(null));

            // Start playing the audio source
            audio.Play();
        }
        else
        {
            //microphone doesn't work for some reason

            Debug.Log(Microphone.GetPosition(null) + " doesn't work!");
        }
    }
}