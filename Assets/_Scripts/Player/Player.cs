using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float moveSpeed = 1.0f;
	PlayerInput input;

	Vector3 lowerLeftCorner;
	Vector3 upperRightCorner;
	float cameraXPos, cameraYPos;
	float minXPos, maxXPos, minYPos, maxYPos;

    void Start()
    {
		input = GetComponent<PlayerInput>();
		GetScreenBounds();
		Debug.Log("minXPos = " + minXPos);
		Debug.Log("maxXPos = " + maxXPos);
		Debug.Log("minYPos = " + minYPos);
		Debug.Log("maxYPos = " + maxYPos);
    }

    void Update()
    {
		EnforcePositiveSpeed();
		MovePlayer();
		//ConfineToScreenEdges();
    }

	void EnforcePositiveSpeed() {if (moveSpeed < 0.0f) moveSpeed = 0.0f;}

	void MovePlayer()
	{
		float moveX = 0.0f;
		float moveY = 0.0f;
		if (input.isMovingUp) {
			moveY += moveSpeed;
		}
		if (input.isMovingDown) {
			moveY -= moveSpeed;
		}
		if (input.isMovingLeft) {
			moveX -= moveSpeed;
		}
		if (input.isMovingRight) {
			moveX += moveSpeed;
		}
		transform.position += new Vector3 (moveX, moveY, 0.0f);
	}

	void ConfineToScreenEdges()
	{
	}

	void GetScreenBounds()
	{
		//Renderer renderer = FindObjectOfType<Renderer>();
		lowerLeftCorner = Camera.main.ViewportToWorldPoint(Vector3.zero);
		upperRightCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
	}
}
