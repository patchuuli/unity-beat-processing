using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPM : MonoBehaviour
{
	private static BPM bpmInstance;
    private AudioClip audioClip;
	private float bpm;

	public static bool beatFull = false;
	private float beatInterval;
	private float beatTimer;
	private int beatCount;

	public static bool beatDouble = false;
	private float beatInterval_double;
	private float beatTimer_double;
	private int beatCount_double;

	public static bool beatHalf = false;
	private float beatInterval_half;
	private float beatTimer_half;
	private int beatCount_half;

	public static bool beat8th = false;
	private float beatInterval_8th;
	private float beatTimer_8th;
	private int beatCount_8th;

	public enum BeatDivision {Double, Full, Half, Eighth};

    void Start()
    {
		EnforceSingleton();
		GetBPMValue();
		SetIntervalValues();
    }

    void Update()
    {
		DetectBeat();
		DetectBeat_double();
		DetectBeat_half();
		DetectBeat_8th();
    }

	void EnforceSingleton()
	{ if (bpmInstance != null && bpmInstance != this) {
			Destroy(this.gameObject);
		}
		else {
			bpmInstance = this;
			DontDestroyOnLoad(this.gameObject);
		}
	}

	void GetBPMValue()
	{
		audioClip = FindObjectOfType<AudioSource>().clip;
		bpm = UniBpmAnalyzer.AnalyzeBpm(audioClip);
	}

	void SetIntervalValues()
	{
		if (bpm == 0) bpm = 60;
		beatInterval = 60/bpm;
		beatInterval_half = beatInterval/2.0f;
		beatInterval_8th = beatInterval/8.0f;
		beatInterval_double = beatInterval/0.5f;
	}

	void DetectBeat()
	{
		beatFull = false;
		beatTimer += Time.deltaTime;
		if (beatTimer >= beatInterval) {
			beatFull = true;
			beatTimer -= beatInterval;
			beatCount++;
		}
	}

	void DetectBeat_double()
	{
		beatDouble = false;
		beatTimer_double += Time.deltaTime;
		if (beatTimer_double >= beatInterval_double) {
			beatDouble = true;
			beatTimer_double -= beatInterval_double;
			beatCount_double++;
		}
	}

	void DetectBeat_half()
	{
		beatHalf = false;
		beatTimer_half += Time.deltaTime;
		if (beatTimer_half >= beatInterval_half) {
			beatHalf = true;
			beatTimer_half -= beatInterval_half;
			beatCount_half++;
		}
	}

	void DetectBeat_8th()
	{
		beat8th = false;
		beatTimer_8th += Time.deltaTime;
		if (beatTimer_8th >= beatInterval_8th) {
			beat8th = true;
			beatTimer_8th -= beatInterval_8th;
			beatCount_8th++;
		}
	}
}
