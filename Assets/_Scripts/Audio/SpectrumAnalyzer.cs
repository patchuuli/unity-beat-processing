using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumAnalyzer : MonoBehaviour
{
	public struct SpectralFluxInfo {
		public float time;
		public float spectralFlux;
		public float threshold;
		public float prunedSpectralFlux;
		public bool isPeak;
	}

	//SpectralFluxInfo[] spectralFluxSamples;
	//float spectralFluxAggregate;
	float[] spectralFluxPerSample;
	float[] prunedSpectralFluxPerSample;
	float[] fluxThresholdPerSample;
	float[] currentSpectrum;
	float[] previousSpectrum;
	public float thresholdMultiplier;
	int thresholdWindowSize = 30;

	public SpectrumAnalyzer()
	{
		currentSpectrum = new float[AudioProcessor.numSamples];
		previousSpectrum = new float[AudioProcessor.numSamples];
		spectralFluxPerSample = new float[AudioProcessor.numSamples];
	}

	public void UpdateSpectrumData(float[] newSpectrum)
	{
		currentSpectrum.CopyTo(previousSpectrum,0);
		newSpectrum.CopyTo(currentSpectrum,0);
	}

	float GetSpectralFlux()
	{
		float sum = 0f;
		for (int i = 0; i < AudioProcessor.numSamples; i++) {
			spectralFluxPerSample[i] = Mathf.Max(0f, currentSpectrum[i] - previousSpectrum[i]);
			fluxThresholdPerSample[i] = GetFluxThreshold(i);
			prunedSpectralFluxPerSample[i] = Mathf.Max (0f, spectralFluxPerSample[i] - fluxThresholdPerSample[i]);
			sum += spectralFluxPerSample[i];
		}
		return sum;
	}

	float GetFluxThreshold(int index) 
	{
		int windowStartIndex = Mathf.Max (0, index - thresholdWindowSize / 2);
		int windowEndIndex = Mathf.Min (AudioProcessor.numSamples - 1, index + thresholdWindowSize / 2);

		float sum = 0f;
		for (int i = windowStartIndex; i < windowEndIndex; i++) {
			sum += spectralFluxPerSample[i];
		}
		float avg = sum / thresholdWindowSize;
		return avg * thresholdMultiplier;
	}

	public void AnalyzeSpectrum(AudioProcessor.SpectrumRange spectrumRange)
	{
	}

}
