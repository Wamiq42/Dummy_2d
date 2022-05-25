using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneInput : MonoBehaviour
{
    private int sampleRate = 256;
    float sensitivity = 100;
    private float loudness = 0;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, true, 5, 44100);
        audioSource.loop = true;
        audioSource.mute = true;
        while (!(Microphone.GetPosition(null) > 0)) 
        {

        }
        audioSource.Play();
    }


    private void Update()
    {
        loudness = GetLoudnessFromClip() * sensitivity;
        Debug.Log(loudness);
        if (loudness > 1)
        {
            loudness = Mathf.Clamp(loudness, 0f, 4f);
            transform.Translate(Vector2.up * loudness *Time.deltaTime);
        }

    }

    float GetLoudnessFromClip()
    {
        float[] wavePosition = new float[sampleRate];

        float a = 0;
        audioSource.GetOutputData(wavePosition, 0);

        foreach (var item in wavePosition)
        {
            a += Mathf.Abs(item);
        }

        return a / sampleRate;
    }
}
