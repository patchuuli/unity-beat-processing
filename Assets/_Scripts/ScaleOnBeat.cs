using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnBeat : MonoBehaviour
{
    public float startScale = 0.5f;
    public float maxScale = 4.0f;
	public float scaleDecreaseFactor = 0.1f;
    Material material;
	public BPM.BeatDivision beatDiv = BPM.BeatDivision.Double;

    void Start()
    {
    }

    void Update()
    {
		switch(beatDiv) {
			case BPM.BeatDivision.Double:
				if (BPM.beatDouble) {
					ResetScale();
					Debug.Log("double");
				}
				else {
					ShrinkScale();
				}
				break;
			case BPM.BeatDivision.Full:
				if (BPM.beatFull) {
					ResetScale();
					Debug.Log("full");
				}
				else {
					ShrinkScale();
				}
				break;
			case BPM.BeatDivision.Half:
				if (BPM.beatHalf) {
					ResetScale();
					Debug.Log("half");
				}
				else {
					ShrinkScale();
				}
				break;
			case BPM.BeatDivision.Eighth:
				if (BPM.beat8th) {
					ResetScale();
					Debug.Log("eighth");
				}
				else {
					ShrinkScale();
				}
				break;
			default:
				return;
		}
    }

	void ShrinkScale() 
	{
        transform.localScale = new Vector3 (
            gameObject.transform.localScale.x - scaleDecreaseFactor,
            gameObject.transform.localScale.y - scaleDecreaseFactor,
            gameObject.transform.localScale.z - scaleDecreaseFactor
        );
		/*
		if (gameObject.transform.localScale.x < 0) {
			gameObject.transform.localScale.x = 0;
		}
		if (gameObject.transform.localScale.y < 0) {
			gameObject.transform.localScale.y = 0;
		}
		if (gameObject.transform.localScale.z < 0) {
			gameObject.transform.localScale.z = 0;
		}
		*/
	}

	void ResetScale()
	{
        transform.localScale = new Vector3 (
            maxScale + startScale,
            maxScale + startScale,
            maxScale + startScale
        );

	}

}