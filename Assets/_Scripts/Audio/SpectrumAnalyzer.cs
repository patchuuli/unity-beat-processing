using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectralFluxInfo {
	public float time;
	public float flux;
	public float threshold;
	public float prunedFlux;
	public bool isPeak;
}


public class SpectrumAnalyzer : MonoBehaviour
{
	List<SpectralFluxInfo> spectrumInfoPerFrame;
	AudioProcessor audioProcessor;
	int indexToProcess;
	float[] currentSpectrum;
	float[] previousSpectrum;

	// Threshold to Determine if Peak
	public int thresholdWindowSize = 50;
	public float thresholdMultiplier = 5.8f;

	// Average Peak Values
	public float avgPeakValue;
	private float totalPeakSum;
	private int numPeaks;

	public float avgNonPeakValue;
	private float totalNonPeakSum;
	private int numNonPeaks;

	public float avgOverallValue;

	void Start()
	{
		audioProcessor = FindObjectOfType<AudioProcessor>();
		spectrumInfoPerFrame = new List<SpectralFluxInfo>();
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
		//Debug.Log("Min: " + spectrumRange.min + "\nMax: " + spectrumRange.max + "\nName: ");
		for (int i = spectrumRange.min; i < spectrumRange.max; i++) {
			sum += Mathf.Max(0f, currentSpectrum[i] - previousSpectrum[i]);
		}
		return sum;
	}

	float GetThresholdOfIndex(int index) 
	{
		int windowStartIndex = Mathf.Max (0, index - thresholdWindowSize / 2);
		int windowEndIndex = Mathf.Min (spectrumInfoPerFrame.Count - 1, index + thresholdWindowSize / 2);

		float sum = 0f;
		for (int i = windowStartIndex; i < windowEndIndex; i++) {
			sum += spectrumInfoPerFrame[i].flux;
		}
		float avg = sum / thresholdWindowSize;
		return avg * thresholdMultiplier;
	}

	float GetPrunedFlux(int index) {
		return Mathf.Max(0f, spectrumInfoPerFrame[index].flux - spectrumInfoPerFrame[index].threshold);
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

	public void AnalyzeSpectrum(float[] newSpectrum, AudioProcessor.SpectrumRange spectrumRange)
	{
		UpdateSpectrumData(newSpectrum);
		SpectralFluxInfo thisFrame = new SpectralFluxInfo();
		thisFrame.flux = GetFluxOfRange(spectrumRange);
		thisFrame.time = audioProcessor.CurrentSongTime();
		spectrumInfoPerFrame.Add(thisFrame);

		if (spectrumInfoPerFrame.Count >= thresholdWindowSize) {
			spectrumInfoPerFrame[indexToProcess].threshold = GetThresholdOfIndex(indexToProcess);
			spectrumInfoPerFrame[indexToProcess].prunedFlux = GetPrunedFlux(indexToProcess);

			// Find peak: Check [n-1] for peak since now have [n-2]'th and [n]'th info
			spectrumInfoPerFrame[indexToProcess-1].isPeak = isPeak(indexToProcess-1);
			if(spectrumInfoPerFrame[indexToProcess-1].isPeak) {
				Debug.Log("beat");
				totalPeakSum += spectrumInfoPerFrame[indexToProcess-1].prunedFlux;
				numPeaks++;
				avgPeakValue = totalPeakSum / numPeaks;
			}
			else {
				totalNonPeakSum += spectrumInfoPerFrame[indexToProcess-1].prunedFlux;
				numNonPeaks++;
				avgNonPeakValue = totalNonPeakSum / numNonPeaks;
			}
			avgOverallValue = (totalNonPeakSum+totalPeakSum) / (numNonPeaks+numPeaks);
			indexToProcess++;
		}
		else {
			//Debug.Log(string.Format("Not ready: {0}/{1} req'd frames", spectrumInfoPerFrame.Count, thresholdWindowSize));
		}

	}

}
