using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAmplitude : MonoBehaviour
{
    public float startScale = 0.5f;
    public float maxScale = 4.0f;
    public bool useBuffer = true;

    private Material material;
    private AudioPeer audioPeer;

    void Start()
    {
        material = GetComponent<MeshRenderer>().materials[0];
        audioPeer = FindObjectOfType<AudioPeer>();
    }

    void Update()
    {
		if (float.IsNaN(audioPeer.amplitudeBuffer) || 
			float.IsNaN(audioPeer.amplitude)) {
				return;
		}
        if (useBuffer) {
            transform.localScale = new Vector3 (
                (audioPeer.amplitudeBuffer * maxScale) + startScale,
                (audioPeer.amplitudeBuffer * maxScale) + startScale,
                (audioPeer.amplitudeBuffer * maxScale) + startScale
            );
        }
        else {
            transform.localScale = new Vector3 (
                (audioPeer.amplitude * maxScale) + startScale,
                (audioPeer.amplitude * maxScale) + startScale,
                (audioPeer.amplitude * maxScale) + startScale
            );
        }
    }

}
