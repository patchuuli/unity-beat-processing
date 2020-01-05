using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnAudio : MonoBehaviour
{
    public int bandNum;
    public float minIntensity = 1.0f;
    public float maxIntensity = 5.0f;
    Light _light;
    AudioPeer audioPeer;
    void Start()
    {
		_light = GetComponent<Light>();
		audioPeer = FindObjectOfType<AudioPeer>();
    }

    void Update()
    {
		_light.intensity = (audioPeer.audioBandBuffer[bandNum] * (maxIntensity - minIntensity)) + minIntensity;
    }
}
