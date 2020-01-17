using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
TODO:
	Edit "SpectralFluxAnalyzer::analyzeSpectrum take an extra argument to set the FreqRange
	Will have to make FreqRange visible to that class

	Notice how the max brilliance index is only 800 or something. 
	Maybe make curSpectrum[] and prevSpectrum[] only be of length [maxBrillianceIndex]

*/

[RequireComponent (typeof(AudioSource))]
[RequireComponent (typeof(SpectrumAnalyzer))]
//[RequireComponent (typeof(SpectralFluxAnalyzer))]
//[RequireComponent (typeof(BandGenerator))]
public class AudioProcessor : MonoBehaviour
{
	public enum FreqRange {
		SubBass,
		Bass,
		LowMidrange,
		Midrange,
		UpperMidrange,
		Presence,
		Brilliance,
		Full
	};

	public struct SpectrumRange {
		public string name;
		public int min;
		public int max;
	};

	private SpectrumRange[] spectrumRange;
	private SpectrumRange rangeToProcess;

	private float[] spectrumData;

	public static int numSamples = 1024;
	private int frequency;
	private int frequencyNyquist;
	private float freqPerBin;

	//public float fluxThreshold = 0.6f;
	//private int thresholdWindowSize = 30;
	//private float thresholdMultiplier = 1.0f;

	private AudioSource audioSource;
	private SpectrumAnalyzer spectrumAnalyzer;
	private BandGenerator bandGenerator;


    void Start()
    {
		audioSource = GetComponent<AudioSource>();
		audioSource.time = 15f;
		audioSource.Play();
		spectrumAnalyzer =  GetComponent<SpectrumAnalyzer>();
		spectrumData = new float[numSamples];
		GetFreqDataFromClip();
		SetSpectrumRange(FreqRange.Bass);
    }

    void Update()
    {
		audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);
		spectrumAnalyzer.UpdateSpectrumData(spectrumData);
		spectrumAnalyzer.AnalyzeSpectrum(rangeToProcess);
		//spectrumAnalyzer.analyzeSpectrum(spectrumData, audioSource.time, rangeToProcess);
		/*
		GetCurrentSpectrumData();
		if (GetSpectralFlux(FreqRange.Bass) > fluxThreshold ||
			GetSpectralFlux(FreqRange.Bass) > fluxThreshold) {
			Debug.Log("KICK");
		}
		if (GetSpectralFlux(FreqRange.Midrange) > fluxThreshold ||
			GetSpectralFlux(FreqRange.UpperMidrange) > fluxThreshold) {
			Debug.Log("CLAP");
		}
		*/
    }

	void GetFreqDataFromClip()
	{
		frequency = audioSource.clip.frequency;
		frequencyNyquist = frequency / 2;
		freqPerBin = (float)frequencyNyquist / (float)numSamples;
	}

	void SetSpectrumRange(FreqRange freqRange)
	{
		spectrumRange = new SpectrumRange[8];
		spectrumRange[0].name = "SubBass";
		spectrumRange[0].min = (int) (20f/freqPerBin);
		spectrumRange[0].max = (int) (60f/freqPerBin);

		spectrumRange[1].name = "Bass";
		spectrumRange[1].min = spectrumRange[0].max;
		spectrumRange[1].max = (int) (250f/freqPerBin);

		spectrumRange[2].name = "LowMidrange";
		spectrumRange[2].min = spectrumRange[1].max;
		spectrumRange[2].max = (int) (500f/freqPerBin);

		spectrumRange[3].name = "Midrange";
		spectrumRange[3].min = spectrumRange[2].max;
		spectrumRange[3].max = (int) (2000f/freqPerBin);

		spectrumRange[4].name = "UpperMidrange";
		spectrumRange[4].min = spectrumRange[3].max;
		spectrumRange[4].max = (int) (4000f/freqPerBin);

		spectrumRange[5].name = "Presence";
		spectrumRange[5].min = spectrumRange[4].max;
		spectrumRange[5].max = (int) (6000f/freqPerBin);

		spectrumRange[6].name = "Brilliance";
		spectrumRange[6].min = spectrumRange[5].max;
		spectrumRange[6].max = (int) (20000f/freqPerBin);

		spectrumRange[7].name = "Full";
		spectrumRange[7].min = spectrumRange[0].min;
		spectrumRange[7].max = spectrumRange[6].max;

		rangeToProcess = spectrumRange[(int)freqRange];
	}

/*
	float GetSpectralFlux(FreqRange range)
	{
		int firstSampleIndex, lastSampleIndex;
		switch (range) {
			case (FreqRange.Full):
			firstSampleIndex = 0;
			lastSampleIndex  = numSamples-1;
			break;

			case (FreqRange.SubBass):
			firstSampleIndex = spectrumRange[0].min;
			lastSampleIndex  = spectrumRange[0].max;
			break;
			
			case (FreqRange.Bass):
			firstSampleIndex = spectrumRange[1].min;
			lastSampleIndex  = spectrumRange[1].max;
			break;

			case (FreqRange.LowMidrange):
			firstSampleIndex = spectrumRange[2].min;
			lastSampleIndex  = spectrumRange[2].max;
			break;

			case (FreqRange.Midrange):
			firstSampleIndex = spectrumRange[3].min;
			lastSampleIndex  = spectrumRange[3].max;
			break;

			case (FreqRange.UpperMidrange):
			firstSampleIndex = spectrumRange[4].min;
			lastSampleIndex  = spectrumRange[4].max;
			break;

			case (FreqRange.Presence):
			firstSampleIndex = spectrumRange[5].min;
			lastSampleIndex  = spectrumRange[5].max;
			break;

			case (FreqRange.Brilliance):
			firstSampleIndex = spectrumRange[6].min;
			lastSampleIndex  = spectrumRange[6].max;
			break;

			default:
			firstSampleIndex = 0;
			lastSampleIndex  = numSamples-1;
			break;
		}

		float flux = 0f;
		for (int i = firstSampleIndex; i <= lastSampleIndex; i++) {
			flux += Mathf.Max(0f, spectrumData[i] - prevSpectrumData[i]);
		}
		return flux;
	}
    */

}