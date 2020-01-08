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
	public GameObject bulletPrefab;
	public float bulletSpeed = 50.0f;
	public float rateOfFire = 0.25f;
	public float timeSinceLastShot = 0.0f;

	public float sineAmplitude = 2.0f;
	public float sineFrequency = 1.0f;

	private int bulletsPerPattern;
	private GameObject[] bulletArr = new GameObject[512];
	public uint bulletCount = 0;
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
				//InitSinePattern();
				break;
			case (BulletPatternType.Flower):
				//InitFlowerPattern();
				break;
			default:
				break;
		}
	}

	void InitBoringPattern()
	{
		bulletsPerPattern = 1;
	}

	public void LaunchPattern()
	{
		switch(bulletPatternType) {
			case (BulletPatternType.Boring):
				LaunchBoringPattern();
				break;
			case (BulletPatternType.Sine):
				LaunchSinePattern();
				break;
			case (BulletPatternType.Flower):
				//InitFlowerPattern();
				break;
			default:
				break;
		}
	}

	private void LaunchBoringPattern()
	{
		if (canFire) {
			bulletArr[bulletCount] = Instantiate(bulletPrefab, player.GetPos(), Quaternion.identity);
			bulletCount++;
			timeSinceLastShot = 0.0f;
		}
	}
	
	private void LaunchSinePattern()
	{
		if (canFire) {
			bulletArr[bulletCount++] = Instantiate(bulletPrefab, player.GetPos(), Quaternion.identity);
			bulletArr[bulletCount++] = Instantiate(bulletPrefab, player.GetPos(), Quaternion.identity);
			//bulletCount+= 2;
			timeSinceLastShot = 0.0f;
		}
	}

	void MoveBullets()
	{
		float bulletX = 0.0f;
		float bulletY = 0.0f; 
		float bulletZ = 0.0f;

		if (bulletPatternType == BulletPatternType.Boring) {
			MoveBoringBullets(bulletX, bulletY, bulletZ);
		}
		else if (bulletPatternType == BulletPatternType.Sine) {
			MoveSineBullets(bulletX, bulletY, bulletZ);
		}
	}

	void MoveBoringBullets(float bulletX, float bulletY, float bulletZ)
	{
		for (int i = 0; i < bulletCount; i++) {
			bulletX = bulletArr[i].transform.position.x;
			bulletY = bulletArr[i].transform.position.y + bulletSpeed * Time.deltaTime;
			bulletZ = bulletArr[i].transform.position.z;
			bulletArr[i].transform.position = new Vector3 (bulletX, bulletY, bulletZ);
		}
	}

	void MoveSineBullets(float bulletX, float bulletY, float bulletZ)
	{
		for (int i = 0; i < bulletCount; i++) {
			bulletX = sineAmplitude * Mathf.Sin(2*Mathf.PI*sineFrequency*Time.realtimeSinceStartup);
			bulletY = bulletArr[i].transform.position.y + bulletSpeed/2 * Time.deltaTime;
			bulletZ = bulletArr[i].transform.position.z;
			bulletArr[i].transform.position = new Vector3 (bulletX, bulletY, bulletZ);
			if (i % 2 == 0) {
				bulletArr[i].transform.position = new Vector3 (-bulletX, bulletY, bulletZ);
			}
			else {
				bulletArr[i].transform.position = new Vector3 (bulletX, bulletY, bulletZ);
			}
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
