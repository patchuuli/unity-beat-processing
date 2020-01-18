using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluxInfo {
	public float time;
	public float flux;
	public float threshold;
	public float prunedFlux;
	public bool isPeak;
}


public class SpectrumAnalyzer : MonoBehaviour
{
	List<FluxInfo> spectrumInfoPerFrame;
	AudioProcessor audioProcessor;
	int indexToProcess;
	float[] currentSpectrum;
	float[] previousSpectrum;
	public int thresholdWindowSize = 50;
	public float thresholdMultiplier;

	void Start()
	{
		audioProcessor = FindObjectOfType<AudioProcessor>();
		spectrumInfoPerFrame = new List<FluxInfo>();
		currentSpectrum = new float[audioProcessor.numSamples];
		previousSpectrum = new float[audioProcessor.numSamples];
		indexToProcess = thresholdWindowSize/2;
	}

	void UpdateSpectrumData(float[] newSpectrum)
	{
		currentSpectrum.CopyTo(previousSpectrum,0);
		newSpectrum.CopyTo(currentSpectrum,0);
	}

	float GetFluxOfRange(AudioProcessor.SpectrumRange spectrumRange)
	{
		float sum = 0f;
		for (int i = spectrumRange.min; i < spectrumRange.max; i++) {
			sum += Mathf.Max(0f, currentSpectrum[i] - previousSpectrum[i]);
		}
		return sum;
	}

	float GetFluxThreshold(int index) 
	{
		int windowStartIndex = Mathf.Max (0, index - thresholdWindowSize / 2);
		int windowEndIndex = Mathf.Min (spectrumInfoPerFrame.Count - 1, index + thresholdWindowSize / 2);

		float sum = 0f;
		for (int i = windowStartIndex; i < windowEndIndex; i++) {
			sum += spectrumInfoPerFrame[i].flux;
		}
		//float avg = sum / thresholdWindowSize;
		float avg = sum / (windowEndIndex-windowStartIndex);
		return avg * thresholdMultiplier;
	}

	float GetPrunedFlux(int index) {
		return Mathf.Max(0f, spectrumInfoPerFrame[index].flux - spectrumInfoPerFrame[index].threshold);
	}

	public void AnalyzeSpectrum(float[] newSpectrum, AudioProcessor.SpectrumRange spectrumRange)
	{
		UpdateSpectrumData(newSpectrum);
		FluxInfo thisFrame = new FluxInfo();
		thisFrame.flux = GetFluxOfRange(spectrumRange);
		thisFrame.time = audioProcessor.CurrentSongTime();
		spectrumInfoPerFrame.Add(thisFrame);

		if (spectrumInfoPerFrame.Count >= thresholdWindowSize) {
			spectrumInfoPerFrame[indexToProcess].threshold = GetFluxThreshold(indexToProcess);
			spectrumInfoPerFrame[indexToProcess].prunedFlux = GetPrunedFlux(indexToProcess);
			spectrumInfoPerFrame[indexToProcess-1].isPeak = isPeak(indexToProcess-1);
			if(spectrumInfoPerFrame[indexToProcess-1].isPeak) {
				Debug.Log("beat");
			}
			indexToProcess++;
		}
		else {
			//Debug.Log(string.Format("Not ready: {0}/{1} req'd frames", spectrumInfoPerFrame.Count, thresholdWindowSize));
		}

	}

	bool isPeak(int index) {
		if (spectrumInfoPerFrame[index].prunedFlux > spectrumInfoPerFrame[index + 1].prunedFlux &&
			spectrumInfoPerFrame[index].prunedFlux > spectrumInfoPerFrame[index - 1].prunedFlux) {
			return true;
		} 
		else {
			return false;
		}
	}

}
