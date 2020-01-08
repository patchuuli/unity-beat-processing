using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	string 	KEY_MOVE_UP = "up";
	string 	KEY_MOVE_DOWN = "down";
	string 	KEY_MOVE_LEFT = "left";
	string 	KEY_MOVE_RIGHT = "right";
	string 	KEY_FIRE_PRIMARY = "a";
	string 	KEY_FIRE_SPECIAL = "s";
	string 	KEY_FOCUS_MODE = "left shift";
	string 	KEY_PAUSE = "space";
	short 	MOUSE_LEFT_CLICK = 0;
	short 	MOUSE_RIGHT_CLICK = 1;
	short 	MOUSE_MIDDLE_CLICK = 2;

	public bool isMovingUp = false;
	public bool isMovingDown = false;
	public bool isMovingLeft = false;
	public bool isMovingRight = false;
	public bool isFiringPrimaryWeapon = false;
	public bool isFiringSpecialWeapon = false;
	public bool isFocused = false;
	public bool isPaused = false;
	public bool isLeftClicked = false;
	public bool isRightClicked = false;
	public bool isMiddleClicked = false;

    void Update()
    {
		ProcessPauseInput();
		ProcessMovementInput();
		ProcessWeaponInput();
		ProcessFocusModeInput();
		ProcessMouseButtons();
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
		isFiringPrimaryWeapon = Input.GetKey(KEY_FIRE_PRIMARY) ? true : false;
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
	void ProcessMouseButtons()
	{
		isLeftClicked = Input.GetMouseButton(MOUSE_LEFT_CLICK) ? true : false;
		isRightClicked = Input.GetMouseButton(MOUSE_RIGHT_CLICK) ? true : false;
		isMiddleClicked = Input.GetMouseButton(MOUSE_MIDDLE_CLICK) ? true : false;
	}
	public Vector3 GetMousePos()
	{
		return Input.mousePosition;
	}
}
