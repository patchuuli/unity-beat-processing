using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandGenerator : MonoBehaviour
{
	public enum ScalingAxis {x,y,z};

	[HideInInspector]
    public GameObject[] bandArr;
	public GameObject sampleCubePrefab;
	public ScalingAxis scalingAxis = ScalingAxis.y;
	private ScalingAxis previousScalingAxis;

    public float bandGap = 0.50f;
    public float bandBaseScale = 10.0f;
	public float scaleMultiplier = 100.0f;
	public bool useBuffer = true;

	private Vector3 baseScaleVec;
	private AudioProcessor audioProcessor;
	private int numBands;
	private Vector3 scaleVec;
	private Vector3 positionVec;

    void Start()
    {
		Init();
		CreateBandObjects();
    }

	void Update()
	{
		if (IsValid()) UpdateBandData();
	}

	void Init()
	{
		audioProcessor = FindObjectOfType<AudioProcessor>();
		numBands = audioProcessor.BANDS;
		bandArr = new GameObject[numBands];

		baseScaleVec = new Vector3 (bandBaseScale, bandBaseScale, bandBaseScale);
		this.transform.position = new Vector3(0, 0, 0);
		previousScalingAxis = scalingAxis;
	}

	void CreateBandObjects()
	{
        for(int i = 0; i < numBands; i++) {
			GameObject bandInstance = (GameObject) Instantiate(sampleCubePrefab);
        	bandInstance.transform.position = this.transform.position;
            bandInstance.transform.parent = this.transform;
            bandInstance.name = "Band" + i;
			bandInstance.transform.localScale = baseScaleVec;
            bandInstance.transform.position = new Vector3 (
				this.transform.position.x,
				this.transform.position.y,
				bandGap * i
			);
            bandArr[i] = bandInstance;
        }
	}

	bool IsValid()
	{
		for (int i = 0; i < numBands; i++) {
			if (useBuffer) {
				if (float.IsNaN(audioProcessor.audioBandBuffer[i])) {
					return false;
				}
			}
			else {
				if (float.IsNaN(audioProcessor.audioBand[i])) {
					return false;
				}
			}
		}
		return true;
	}

	void UpdateBandData()
	{
		if (useBuffer) UpdateBandsWithBuffer();
		else UpdateBandsWithoutBuffer();
	}

	void UpdateBandsWithBuffer()
	{
		for (int i = 0; i < numBands; i++) {
			float value = audioProcessor.audioBandBuffer[i] * scaleMultiplier + bandBaseScale;
			SetScaleVector(i, value);
			bandArr[i].transform.localScale = scaleVec;
		}
	}

	void UpdateBandsWithoutBuffer()
	{
		for (int i = 0; i < numBands; i++) {
			float value = audioProcessor.audioBand[i] * scaleMultiplier + bandBaseScale;
			SetScaleVector(i, value);
			bandArr[i].transform.localScale = scaleVec;
		}
	}

	void SetScaleVector(int index, float value)
	{
		if (scalingAxis == ScalingAxis.x) {
			scaleVec = new Vector3(
				value, 
				bandArr[index].transform.localScale.y,
				bandArr[index].transform.localScale.z
				);
		}
		else if (scalingAxis == ScalingAxis.y) {
			scaleVec = new Vector3(
				bandArr[index].transform.localScale.x,
				value, 
				bandArr[index].transform.localScale.z
				);
		}
		else {
			scaleVec = new Vector3(
				bandArr[index].transform.localScale.x,
				bandArr[index].transform.localScale.y,
				value
				);
		}
	}

	void ChangePositionAxis()
	{
		if (scalingAxis != previousScalingAxis) {
	        for(int i = 0; i < numBands; i++) {
				GameObject bandInstance = (GameObject) Instantiate(sampleCubePrefab);
	        	bandInstance.transform.position = this.transform.position;
	            bandInstance.transform.parent = this.transform;
	            bandInstance.name = "Band" + i;
				bandInstance.transform.localScale = baseScaleVec;
	            bandInstance.transform.position = new Vector3 (
					this.transform.position.x,
					this.transform.position.y,
					bandGap * i
				);
	            bandArr[i] = bandInstance;
	        }
		}
	}
}
