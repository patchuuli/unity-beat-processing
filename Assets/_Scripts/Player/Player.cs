using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float basePlayerSpeed = 35.0f;
	public float focusModePlayerSpeedMultiplier = 0.5f;
	float currentPlayerSpeed;

	BulletPattern bulletPattern;

	PlayerInput input;
	PlayArea playArea;

    void Start()
    {
		SetComponentsReferences();
    }

    void Update()
    {
		EnforcePositiveSpeed();
		MovePlayer();
		AttackPrimary();
    }

	void SetComponentsReferences()
	{
		input = GetComponent<PlayerInput>();
		playArea = FindObjectOfType<PlayArea>();
		bulletPattern = FindObjectOfType<BulletPattern>();	
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
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x + moveX * Time.deltaTime, playArea.minX, playArea.maxX),
			Mathf.Clamp(transform.position.y + moveY * Time.deltaTime, playArea.minY, playArea.maxY),
			transform.position.z
		);
	}

	void AttackPrimary()
	{
		if (input.isFiringPrimaryWeapon) {
			bulletPattern.LaunchPattern();
		}
	}

	public Vector3 GetPos()
	{
		return transform.position;
	}
}
