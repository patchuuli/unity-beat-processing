using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPattern : MonoBehaviour
{
	/*
	Bullet Types:
		Boring: A single boring bullet -- fast
		Sine: 	2 bullets per fire in sine wave -- medium
		Flower: 1 larger bullet, explodes into more after x distance - slow
	*/
	public enum BulletPatternType {
		Boring, Sine, Flower
	};
	public BulletPatternType bulletPatternType;

	private int bulletsPerPattern;



	public GameObject bulletPrefab;
	public float bulletSpeed = 50.0f;
	public float rateOfFire = 0.25f;
	public float timeSinceLastShot = 0.0f;

	private GameObject[] bulletArr = new GameObject[512];
	private uint bulletCount = 0;
	private bool canFire = true;
	private Player player;



    void Start()
    {
		player = FindObjectOfType<Player>();
		SetPatternType();
    }

    void Update()
    {
		CoolDown();
		MoveBullets();
    }

	void SetPatternType()
	{
		switch(bulletPatternType) {
			case (BulletPatternType.Boring):
				InitBoringPattern();
				break;
			case (BulletPatternType.Sine):
				InitSinePattern();
				break;
			case (BulletPatternType.Flower):
				InitFlowerPattern();
				break;
			default:
				break;
		}
		Debug.Log(bulletPatternType);
	}

	void InitBoringPattern()
	{
		bulletsPerPattern = 1;
	}

	void InitSinePattern()
	{

	}

	void InitFlowerPattern()
	{

	}

	public void LaunchPattern()
	{
		if (canFire) {

			bulletArr[bulletCount] = Instantiate(bulletPrefab, player.GetPos(), Quaternion.identity);
			bulletCount++;
			Debug.Log("bulletCount = " + bulletCount);
			timeSinceLastShot = 0.0f;
		}
	}

	void MoveBullets()
	{
		for (int i = 0; i < bulletCount; i++) {
			bulletArr[i].transform.position = new Vector3 (
				bulletArr[i].transform.position.x,
				bulletArr[i].transform.position.y + bulletSpeed * Time.deltaTime,
				bulletArr[i].transform.position.z
			);
		}
	}
	void CoolDown()
	{
		if (timeSinceLastShot > (1/rateOfFire)) {
			canFire = true;
		}
		else {
			canFire = false;
			timeSinceLastShot += Time.deltaTime;
		}
	}
}
