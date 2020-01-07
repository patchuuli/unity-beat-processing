using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float basePlayerSpeed = 35.0f;
	public float focusModePlayerSpeedMultiplier = 0.5f;
	float currentPlayerSpeed;

	PrimaryAttack primaryAttack;

	PlayerInput input;
	Vector3 lowerLeftCorner;
	Vector3 upperRightCorner;
	float cameraXPos, cameraYPos;
	//float minXPos, maxXPos, minYPos, maxYPos;

    void Start()
    {
		input = GetComponent<PlayerInput>();
		GetScreenBounds();
		primaryAttack = FindObjectOfType<PrimaryAttack>();
		/*
		Debug.Log("minXPos = " + minXPos);
		Debug.Log("maxXPos = " + maxXPos);
		Debug.Log("minYPos = " + minYPos);
		Debug.Log("maxYPos = " + maxYPos);
		*/
    }

    void Update()
    {
		EnforcePositiveSpeed();
		MovePlayer();
		FireWeapon();
		//ConfineToScreenEdges();
    }

	void EnforcePositiveSpeed() {if (currentPlayerSpeed < 0.0f) currentPlayerSpeed = 0.0f;}

	void MovePlayer()
	{
		float moveX = 0.0f;
		float moveY = 0.0f;
		currentPlayerSpeed = input.isFocused ? basePlayerSpeed * focusModePlayerSpeedMultiplier : basePlayerSpeed;
		if (input.isMovingUp) {
			moveY += currentPlayerSpeed;
		}
		if (input.isMovingDown) {
			moveY -= currentPlayerSpeed;
		}
		if (input.isMovingLeft) {
			moveX -= currentPlayerSpeed;
		}
		if (input.isMovingRight) {
			moveX += currentPlayerSpeed;
		}
		transform.position += new Vector3 (moveX * Time.deltaTime, moveY * Time.deltaTime, 0.0f);
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

	void FireWeapon()
	{
		if (input.isFiringPrimaryWeapon) {
			primaryAttack.FireBullet();
		}
	}

	public Vector3 GetPos()
	{
		return transform.position;
	}
}
