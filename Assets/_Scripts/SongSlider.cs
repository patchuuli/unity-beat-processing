using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongSlider : MonoBehaviour
{
	private Slider slider;
	private AudioClip audioClip;
	private AudioSource audioSource;
    void Start()
    {
		slider = GetComponent<Slider>();
		audioSource = FindObjectOfType<AudioSource>();
		audioClip = audioSource.clip;
		slider.maxValue = (float) audioClip.length;
		slider.minValue = 0.0f;
		slider.value = slider.maxValue / 2;
    }

    void Update()
    {
    }
}
