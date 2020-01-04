using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public int band;
    public float startScale, scaleMultiplier;

    void Start()
    {
    }

    void Update()
    {
		transform.localScale = new Vector3(
			transform.localScale.x,
			(AudioPeer.freqBands[band] * scaleMultiplier) + startScale, 
			transform.localScale.z
		);
    }
}
