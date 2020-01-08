using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : MonoBehaviour
{
	public float minX, minY, maxX, maxY;
	private Vector2 screenBounds;

    void Start()
    {
		FindWorldScreenBounds();
    }

	void FindWorldScreenBounds()
	{
		screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		minX = -screenBounds.x;
		maxX = screenBounds.x;
		minY = -screenBounds.y;
		maxY = screenBounds.y;
	}
}
