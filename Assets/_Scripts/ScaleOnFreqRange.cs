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
    private AudioPeer audioPeer;

    void Start()
    {
        audioPeer = FindObjectOfType<AudioPeer>();
		/*
		Debug.Log("Subbass = " + (int)FrequencyRange.SubBass);
		Debug.Log("Bass = " + (int)FrequencyRange.Bass);
		Debug.Log("LowMidRange = " + (int)FrequencyRange.LowMidRange);
		*/
    }

    void Update()
    {
		if (float.IsNaN(audioPeer.amplitudeBuffer) || 
			float.IsNaN(audioPeer.amplitude)) {
				return;
		}
        if (useBuffer) {
            transform.localScale = new Vector3 (
                audioPeer.audioBandBuffer[(int)frequencyRange] * maxScale,
                audioPeer.audioBandBuffer[(int)frequencyRange] * maxScale,
                audioPeer.audioBandBuffer[(int)frequencyRange] * maxScale
            );
        }
        else {
            transform.localScale = new Vector3 (
                audioPeer.audioBand[(int)frequencyRange] * maxScale,
                audioPeer.audioBand[(int)frequencyRange] * maxScale,
                audioPeer.audioBand[(int)frequencyRange] * maxScale
            );
        }
    }

}
