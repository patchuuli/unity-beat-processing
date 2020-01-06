using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAmplitude : MonoBehaviour
{
    public float startScale = 0.5f;
    public float maxScale = 4.0f;
    public bool useBuffer = true;

    private Material material;
    private AudioProcessor audioProcessor;

    void Start()
    {
        material = GetComponent<MeshRenderer>().materials[0];
        audioProcessor = FindObjectOfType<AudioProcessor>();
    }

    void Update()
    {
		if (float.IsNaN(audioProcessor.amplitudeBuffer) || 
			float.IsNaN(audioProcessor.amplitude)) {
				return;
		}
        if (useBuffer) {
            transform.localScale = new Vector3 (
                (audioProcessor.amplitudeBuffer * maxScale) + startScale,
                (audioProcessor.amplitudeBuffer * maxScale) + startScale,
                (audioProcessor.amplitudeBuffer * maxScale) + startScale
            );
        }
        else {
            transform.localScale = new Vector3 (
                (audioProcessor.amplitude * maxScale) + startScale,
                (audioProcessor.amplitude * maxScale) + startScale,
                (audioProcessor.amplitude * maxScale) + startScale
            );
        }
    }

}
