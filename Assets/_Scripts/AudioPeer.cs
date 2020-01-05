using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{

	AudioSource audioSource;
	int audioFreq;
	float hertzPerSample;
	const int numSamples = 1024;
	const FFTWindow fftWindowType = FFTWindow.BlackmanHarris;

	public float[] samplesLeft = new float[numSamples];
	public float[] samplesRight = new float[numSamples];

	private float[] freqBands;
	private float[] bandBuffer;
	private float[] bufferDecrease;
	private float[] freqBandHighest;

	[HideInInspector]
	public float[] audioBand, audioBandBuffer;
	[HideInInspector]
	public float amplitude, amplitudeBuffer;

	private float amplitudeHighest;

	public float audioProfile = 5;
	public float barFallAccel = 1.20f;
	public float barFallBaseSpeed = 0.01f;

	public enum Channel {Stereo, Left, Right};
	public Channel channel = new Channel();

	public int BANDS = 7;

    void Start()
    {
		InitData(BANDS);
    }

    void Update()
    {
		if (!audioSource.isPlaying) {
			return;
		}
		GetSpectrumAudioSource();
		MakeFreqBands(BANDS);
		BandBuffer(BANDS);
		CreateAudioBands(BANDS);
		GetAmplitude(BANDS);
    }

	void InitData(int numBands)
	{
		audioSource = GetComponent<AudioSource>();
		audioBand = new float[numBands];
		audioBandBuffer = new float[numBands];
		channel = Channel.Stereo;
		//AudioProfile(audioProfile, numBands);	
		freqBands = new float[numBands];
		bandBuffer = new float[numBands];
		bufferDecrease = new float[numBands];
		freqBandHighest = new float[numBands];
	}
    void GetSpectrumAudioSource()
    {
		audioSource.GetSpectrumData(samplesLeft, 0, fftWindowType);
		audioSource.GetSpectrumData(samplesRight, 1, fftWindowType);
    }
	void MakeFreqBands(int numBands)
	{
		int count = 0;

		for (int i = 0; i < numBands; i++) {
			float average = 0;
			int sampleCount = (int) Mathf.Pow(2,i+1); // the number of samples within band 'i'
			if (i == numBands-1) {
				sampleCount += 2;
			}

			for (int j = 0; j < sampleCount; j++) { // find average of all amplitudes in band 'i'
			if (channel == Channel.Stereo) {
				average += (samplesLeft[count] + samplesRight[count]) * (count+1);
			}
			else if (channel == Channel.Left) {
				average += samplesLeft[count] * (count+1);
			}
			else if (channel == Channel.Right) {
				average += samplesRight[count] * (count+1);
			}
				count++;
			}
			average /= count;
			freqBands[i] = average * 10;
		}
	}
	void BandBuffer(int numBands)
	{
		for (int i = 0; i < numBands; i++) {
			if (freqBands[i] < bandBuffer[i]) {
				bandBuffer[i] -= bufferDecrease[i];
				bufferDecrease[i] *= barFallAccel;
			}
			else {
				bandBuffer[i] = freqBands[i];
				bufferDecrease[i] = barFallBaseSpeed;
			}
		}

	}
	void CreateAudioBands(int numBands)
	{
		for (int i = 0; i < numBands; i++) {
			if (freqBands[i] > freqBandHighest[i]) {
				freqBandHighest[i] = freqBands[i];
			}
			audioBand[i] = (freqBands[i]/freqBandHighest[i]);
			audioBandBuffer[i] = (bandBuffer[i]/freqBandHighest[i]);
		}

	}
	void GetAmplitude(int numBands) // put average amplitude into one value
	{
		float currentAmplitude = 0;
		float currentAmplitudeBuffer = 0;

		for (int i = 0; i < numBands; i++) {
			currentAmplitude += audioBand[i];
			currentAmplitudeBuffer += audioBandBuffer[i];
		}

		if (currentAmplitude > amplitudeHighest) {
			amplitudeHighest = currentAmplitude;
		}
		amplitude = currentAmplitude / amplitudeHighest;
		amplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;
	}
	void AudioProfile(float audioProfile,int numBands)
	{
		for (int i = 0; i < numBands; i++) {
			freqBandHighest[i] = audioProfile;
		}
	}

}
