using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public int bandNum;
    public float startScale = 4.0f;
	public float scaleMultiplier = 96.0f;
    public bool useBuffer = true;
	Material material;
	AudioPeer audioPeer;

    void Start()
    {
		material = GetComponent<MeshRenderer>().materials[0];
		audioPeer = FindObjectOfType<AudioPeer>();
    }

    void Update()
    {
		if (float.IsNaN(audioPeer.audioBandBuffer[bandNum])) {
			return;
		}
    	if (useBuffer) {
			transform.localScale = new Vector3(
				transform.localScale.x,
				(audioPeer.audioBandBuffer[bandNum] * scaleMultiplier) + startScale, 
				transform.localScale.z
			);
			Color color = new Color(
				audioPeer.audioBandBuffer[bandNum], 
				audioPeer.audioBandBuffer[bandNum], 
				audioPeer.audioBandBuffer[bandNum] 
			);
			material.SetColor("_EmissionColor", color);
    	}
		else {
			transform.localScale = new Vector3(
				transform.localScale.x,
				(audioPeer.audioBand[bandNum] * scaleMultiplier) + startScale, 
				transform.localScale.z
			);
			Color color = new Color(
				audioPeer.audioBand[bandNum], 
				audioPeer.audioBand[bandNum], 
				audioPeer.audioBand[bandNum] 
			);
			material.SetColor("_EmissionColor", color);
		}
    }
}
