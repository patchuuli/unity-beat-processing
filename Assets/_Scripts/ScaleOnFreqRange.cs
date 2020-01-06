using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnFreqRange : MonoBehaviour
{
	public enum FrequencyRange {
		SubBass,
		Bass,
		LowMidRange,
		MidRange,
		UpperMidRange,
		Presence,
		Brilliance
		};

	[Header("Size")]
    public float startScale = 0.5f;
    public float maxScale = 4.0f;

	[Header("Data Selection")]
	public FrequencyRange frequencyRange;
    public bool useBuffer = true;

    private Material material;
    private AudioProcessor audioProcessor;

    void Start()
    {
        audioProcessor = FindObjectOfType<AudioProcessor>();
		/*
		Debug.Log("Subbass = " + (int)FrequencyRange.SubBass);
		Debug.Log("Bass = " + (int)FrequencyRange.Bass);
		Debug.Log("LowMidRange = " + (int)FrequencyRange.LowMidRange);
		*/
    }

    void Update()
    {
		if (float.IsNaN(audioProcessor.amplitudeBuffer) || 
			float.IsNaN(audioProcessor.amplitude)) {
				return;
		}
        if (useBuffer) {
            transform.localScale = new Vector3 (
                audioProcessor.audioBandBuffer[(int)frequencyRange] * maxScale,
                audioProcessor.audioBandBuffer[(int)frequencyRange] * maxScale,
                audioProcessor.audioBandBuffer[(int)frequencyRange] * maxScale
            );
        }
        else {
            transform.localScale = new Vector3 (
                audioProcessor.audioBand[(int)frequencyRange] * maxScale,
                audioProcessor.audioBand[(int)frequencyRange] * maxScale,
                audioProcessor.audioBand[(int)frequencyRange] * maxScale
            );
        }
    }

}
