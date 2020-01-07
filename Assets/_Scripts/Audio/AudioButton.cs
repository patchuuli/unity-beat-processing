using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    void Start()
    {
		GetComponent<Button>().onClick.AddListener(PlayAudio);
    }
	void PlayAudio()
	{
		FindObjectOfType<AudioSource>().Play();
	}
}
