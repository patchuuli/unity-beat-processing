using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class ParamCube : MonoBehaviour
{
    public int bandNum;
    public float startScale = 4.0f;
	public float scaleMultiplier = 96.0f;
    public bool useBuffer = true;
	Material material;
	AudioProcessor audioProcessor;

    void Start()
    {
		material = GetComponent<MeshRenderer>().materials[0];
		audioProcessor = FindObjectOfType<AudioProcessor>();
    }

    void Update()
    {
		if (float.IsNaN(audioProcessor.audioBandBuffer[bandNum])) {
			return;
		}
    	if (useBuffer) {
			transform.localScale = new Vector3(
				transform.localScale.x,
				(audioProcessor.audioBandBuffer[bandNum] * scaleMultiplier) + startScale, 
				transform.localScale.z
			);
			Color color = new Color(
				audioProcessor.audioBandBuffer[bandNum], 
				audioProcessor.audioBandBuffer[bandNum], 
				audioProcessor.audioBandBuffer[bandNum] 
			);
			material.SetColor("_EmissionColor", color);
    	}
		else {
			transform.localScale = new Vector3(
				transform.localScale.x,
				(audioProcessor.audioBand[bandNum] * scaleMultiplier) + startScale, 
				transform.localScale.z
			);
			Color color = new Color(
				audioProcessor.audioBand[bandNum], 
				audioProcessor.audioBand[bandNum], 
				audioProcessor.audioBand[bandNum] 
			);
			material.SetColor("_EmissionColor", color);
		}
    }
}
*/