using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandGenerator : MonoBehaviour
{
	private AudioProcessor audioProcessor;
	private int numBands;
	public GameObject sampleCubePrefab;
    public float bandGap = 0.50f;
    public float bandBaseScale = 10.0f;
    //public float maxScale = 100000;
    //public float cubeHeight = 2;
	[HideInInspector]
    public GameObject[] bands;


    void Start()
    {
		audioProcessor = FindObjectOfType<AudioProcessor>();
		numBands = audioProcessor.BANDS;
		bands = new GameObject[numBands];
		Vector3 baseScale = new Vector3 (bandBaseScale, bandBaseScale, bandBaseScale);
        for(int i = 0; i < numBands; i++) {
            GameObject bandInstance = (GameObject) Instantiate(sampleCubePrefab);
            bandInstance.transform.position = this.transform.position;
            bandInstance.transform.parent = this.transform;
            bandInstance.name = "Band" + i;
            bandInstance.transform.position = new Vector3 (
				this.transform.position.x,
				this.transform.position.y,
				bandGap * i
			);
            bands[i] = bandInstance;
        }
		this.transform.position = new Vector3(0, 0, (float)( -1 * bandGap * numBands) / 2);
    }
}
