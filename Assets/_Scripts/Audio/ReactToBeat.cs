using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactToBeat : MonoBehaviour
{
	public enum AttributeType {
		Scale, Light
	};

	[Header("General")]
	//public AttributeType attributeType;
	public bool enableScalingEffect = false;
	public bool enableLightingEffect = false;
	public bool enableAverageAmpEffect = false;
	public BeatDetector.BeatDivision beatDivision = BeatDetector.BeatDivision.Double;

	[Header("Scale Settings")]
    public float maxScale = 4.0f;
	public float minScale = 0.5f;
	public float scaleDecreaseFactor = 0.1f;

	[Header("Lighting Settings")]
    public float maxIntensity = 4.0f;
	public float minIntensity = 0.5f;
	public float lightDecreaseFactor = 0.1f;
	//public float r,g,b;

    private Material material;
	private Light lightComponent;
	private AudioProcessor audioProcessor;

	void Start()
	{
		material = GetComponent<MeshRenderer>().materials[0];
		audioProcessor = FindObjectOfType<AudioProcessor>();
		lightComponent = GetComponent<Light>();
	}

    void Update()
    {
		if (enableScalingEffect) ScaleToBeat();
		if (enableLightingEffect) LightToBeat();
		float j = audioProcessor.averageAmplitude;
    }


	/***************************************************
						AMPLITUDE
	***************************************************/
	float GetAvgAmplitude(){return audioProcessor.averageAmplitude;}

	/***************************************************
						LIGHTING
	***************************************************/

	void LightToBeat()
	{
		switch(beatDivision) {
			case BeatDetector.BeatDivision.Double:
				ProcessLightingEffect(BeatDetector.beatDouble);
				break;
			case BeatDetector.BeatDivision.Full:
				ProcessLightingEffect(BeatDetector.beatFull);
				break;
			case BeatDetector.BeatDivision.Half:
				ProcessLightingEffect(BeatDetector.beatHalf);
				break;
			case BeatDetector.BeatDivision.Eighth:
				ProcessLightingEffect(BeatDetector.beat8th);
				break;
			default:
				return;
		}
	}

	void ProcessLightingEffect(bool beatDetected)
	{
		if (beatDetected) {
			SetFullLight();
		}
		else {
			DimLight();
		}
	}

	void DimLight() 
	{
		lightComponent.intensity -= lightDecreaseFactor;
		EnforceMinimumLight();
	}

	void EnforceMinimumLight()
	{
		if (lightComponent.intensity < minIntensity) {
			lightComponent.intensity = minIntensity;
		}
	}

	void SetFullLight() {lightComponent.intensity = maxIntensity;}

	/***************************************************
						SCALING
	***************************************************/

	void ScaleToBeat()
	{
		switch(beatDivision) {
			case BeatDetector.BeatDivision.Double:
				ProcessScalingEffect(BeatDetector.beatDouble);
				break;
			case BeatDetector.BeatDivision.Full:
				ProcessScalingEffect(BeatDetector.beatFull);
				break;
			case BeatDetector.BeatDivision.Half:
				ProcessScalingEffect(BeatDetector.beatHalf);
				break;
			case BeatDetector.BeatDivision.Eighth:
				ProcessScalingEffect(BeatDetector.beat8th);
				break;
			default:
				return;
		}
	}

	void ProcessScalingEffect(bool beatDetected)
	{
		if (beatDetected) {
			ResetScale();
		}
		else {
			ShrinkScale();
		}
	}

	void ShrinkScale() 
	{
        transform.localScale = new Vector3 (
            gameObject.transform.localScale.x - scaleDecreaseFactor,
            gameObject.transform.localScale.y - scaleDecreaseFactor,
            gameObject.transform.localScale.z - scaleDecreaseFactor
        );
		EnforceMinimumScale();
	}

	void EnforceMinimumScale()
	{
		if (gameObject.transform.localScale.x < minScale ||
			gameObject.transform.localScale.y < minScale ||
			gameObject.transform.localScale.z < minScale) {

        		transform.localScale = new Vector3 (minScale, minScale, minScale);
		}
	}

	void ResetScale()
	{
        transform.localScale = new Vector3 (
            maxScale,
            maxScale,
            maxScale
        );

	}
}