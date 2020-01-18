using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatFinder : MonoBehaviour
{
	AudioProcessor audioProcessor;
	float thresholdMultiplier = 1.5f;
	int thresholdWindowSize = 50;
	float[] curSpectrum;
	float[] prevSpectrum;
	public SpectralFluxInfo[] spectralFluxSamples;


	public BeatFinder () {
		audioProcessor = FindObjectOfType<AudioProcessor>();
		spectralFluxSamples = new SpectralFluxInfo[audioProcessor.numSamples];
		curSpectrum = new float[audioProcessor.numSamples];
		prevSpectrum = new float[audioProcessor.numSamples];
	}

	float GetSpectralFlux(AudioProcessor.SpectrumRange spectrumRange)
	{
		float sum = 0f;
		for (int i = spectrumRange.min; i < spectrumRange.max; i++) {
			sum += Mathf.Max (0f, curSpectrum [i] - prevSpectrum [i]);
		}
		return sum;
	}

}
