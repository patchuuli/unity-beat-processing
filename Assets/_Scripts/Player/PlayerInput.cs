using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	string KEY_MOVE_UP = "up";
	string KEY_MOVE_DOWN = "down";
	string KEY_MOVE_LEFT = "left";
	string KEY_MOVE_RIGHT = "right";
	string KEY_FIRE_MAIN = "s";
	string KEY_FIRE_SPECIAL = "d";
	string KEY_FOCUS_MODE = "left shift";
	string KEY_PAUSE = "space";

	public bool isMovingUp = false;
	public bool isMovingDown = false;
	public bool isMovingLeft = false;
	public bool isMovingRight = false;
	public bool isFiringMainWeapon = false;
	public bool isFiringSpecialWeapon = false;
	public bool isFocused = false;
	public bool isPaused = false;

    void Update()
    {
		ProcessMovementInput();
		ProcessWeaponInput();
		ProcessFocusModeInput();
		ProcessPauseInput();
    }

	void ProcessMovementInput()
	{
		isMovingUp = Input.GetKey(KEY_MOVE_UP) ? true : false;
		isMovingDown = Input.GetKey(KEY_MOVE_DOWN) ? true : false;
		isMovingLeft = Input.GetKey(KEY_MOVE_LEFT) ? true : false;
		isMovingRight = Input.GetKey(KEY_MOVE_RIGHT) ? true : false;
	}
	void ProcessWeaponInput()
	{
		isFiringMainWeapon = Input.GetKey(KEY_FIRE_MAIN) ? true : false;
		isFiringSpecialWeapon = Input.GetKey(KEY_FIRE_SPECIAL) ? true : false;
	}
	void ProcessFocusModeInput()
	{
		isFocused = Input.GetKey(KEY_FOCUS_MODE) ? true : false;
	}
	void ProcessPauseInput()
	{
		if (Input.GetKeyDown(KEY_PAUSE)) {
			if (isPaused) isPaused = false;
			else isPaused = true;
		}
	}
}
