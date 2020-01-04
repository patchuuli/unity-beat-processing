using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
	AudioSource audioSource;
	int audioFreq;
	float hertzPerSample;
	const int numSamples = 512;
	const int numBands = 8;
	const int numChannels = 0;
	const FFTWindow fftWindowType = FFTWindow.BlackmanHarris;

	public static float[] samples = new float[numSamples];
	public static float[] freqBands = new float[numBands];

    void Start()
    {
		audioSource = GetComponent<AudioSource>();
		audioFreq = audioSource.clip.frequency;
		hertzPerSample = audioFreq/numSamples;
		Debug.Log(
			"Title: " + audioSource.clip.name + 
			"\nFrequency: " + audioFreq + 
			"\nChannels: " + audioSource.clip.channels +
			"\nLength: " + audioSource.clip.length + 
			"\nSamples: " + audioSource.clip.samples
		);
    }

    void Update()
    {
		GetSpectrumAudioSource();
		MakeFreqBands();
    }
    void GetSpectrumAudioSource()
    {
		audioSource.GetSpectrumData(samples, numChannels, fftWindowType);
    }
	void MakeFreqBands()
	{
		int count = 0;

		for (int i = 0; i < numBands; i++) {
			float average = 0;
			int sampleCount = (int) Mathf.Pow(2,i+1); // the number of samples within band 'i'
			if (i == numBands-1) {
				sampleCount += 2;
			}

			for (int j = 0; j < sampleCount; j++) { // find average of all amplitudes in band 'i'
				average += samples[count] * (count+1);
				count++;
			}

			average /= count;
			freqBands[i] = average * 10;
		}
	}
}
