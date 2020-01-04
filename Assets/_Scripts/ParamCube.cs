using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public int bandNum;
    public float startScale, scaleMultiplier;
    public bool useBuffer = true;

    void Start()
    {
    }

    void Update()
    {
    	if (useBuffer) {
			transform.localScale = new Vector3(
				transform.localScale.x,
				(AudioPeer.bandBuffer[bandNum] * scaleMultiplier) + startScale, 
				transform.localScale.z
			);
    	}
		else {
			transform.localScale = new Vector3(
				transform.localScale.x,
				(AudioPeer.freqBands[bandNum] * scaleMultiplier) + startScale, 
				transform.localScale.z
			);
		}
    }
}
