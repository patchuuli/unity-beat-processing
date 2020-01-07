using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Band64Instantiate : MonoBehaviour
{
    public GameObject sampleCubePrefab;
    public float bandGap = 1.0f;
	public float baseScale = 1000;
	public float maxScale = 100000;

    private GameObject[] bands = new GameObject[64];
	AudioProcessor audioProcessor;

    void Start()
    {
		audioProcessor = FindObjectOfType<AudioProcessor>();
		InstantiateBands64();
    }

    void Update()
    {
		//ScaleBandHeight();
    }

	void InstantiateBands64()
	{
		for (int i = 0; i < 64; i++) {
			GameObject bandInstance = (GameObject) Instantiate(sampleCubePrefab);
			bandInstance.transform.position = this.transform.position;
			bandInstance.transform.parent = this.transform;
            bandInstance.name = "Band " + i;
            bandInstance.transform.position = new Vector3 (
                this.transform.position.x,
                this.transform.position.y,
                this.transform.position.z + i*bandGap
            );
		}
	}

	void ScaleBandHeight()
	{
		/*
		for (int i = 0; i < 64; i++) {
			bands[i].transform.localScale = new Vector3 (
				baseScale,
				baseScale + (audioProcessor.samplesLeft[i] + audioProcessor.samplesRight[i]) * maxScale,
				baseScale
			);
		}
		*/
	}
}
