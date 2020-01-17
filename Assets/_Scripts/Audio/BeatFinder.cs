using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatFinder : MonoBehaviour
{
	float thresholdMultiplier = 1.5f;
	int thresholdWindowSize = 50;
	float[] curSpectrum;
	float[] prevSpectrum;
	public SpectralFluxInfo[] spectralFluxSamples;


	public BeatFinder () {
		spectralFluxSamples = new SpectralFluxInfo[AudioProcessor.numSamples];
		curSpectrum = new float[AudioProcessor.numSamples];
		prevSpectrum = new float[AudioProcessor.numSamples];
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
