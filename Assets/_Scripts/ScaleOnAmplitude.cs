using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAmplitude : MonoBehaviour
{
    public float startScale = 0.5f;
    public float maxScale = 4.0f;
	public float scaleDecreaseFactor = 0.1f;
    public float red, green, blue;  
    Material material;

    void Start()
    {
        //material = GetComponent<MeshRenderer>().materials[0];
    }

    void Update()
    {
		if (BPM.beatDouble) {
			Debug.Log("Double Beat!");
            transform.localScale = new Vector3 (
                maxScale,
                maxScale,
                maxScale
            );
        }
        else {
            transform.localScale = new Vector3 (
                gameObject.transform.localScale.x - scaleDecreaseFactor,
                gameObject.transform.localScale.y - scaleDecreaseFactor,
                gameObject.transform.localScale.z - scaleDecreaseFactor
            );
        }
    }

}