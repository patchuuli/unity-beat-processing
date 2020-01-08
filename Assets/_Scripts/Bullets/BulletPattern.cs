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
		Boring, Sine, Burst, Flower
	};
	public BulletPatternType bulletPatternType;
	public GameObject bulletPrefab;
	public float bulletSpeed = 50.0f;
	public float rateOfFire = 0.25f;
	public float timeSinceLastShot = 0.0f;

	public float sineAmplitude = 2.0f;
	public float sineFrequency = 1.0f;

	public List<GameObject> bulletList = new List<GameObject>();
	private List<float> bulletLaunchPos = new List<float>();
	private List <float> bulletLaunchTime = new List<float>();
	private bool canFire = true;
	private Player player;
	private PlayArea playArea;


    void Start()
    {
		SetObjectReferences();
    }

    void Update()
    {
		CoolDown();
		MoveBullets();
		RemoveBullets();
    }

	void SetObjectReferences()
	{
		player = FindObjectOfType<Player>();
		playArea = FindObjectOfType<PlayArea>();
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
			case (BulletPatternType.Burst):
				LaunchBurstPattern();
				break;
			default:
				break;
		}
	}

	private void LaunchBoringPattern()
	{
		if (canFire) {
			bulletList.Add(Instantiate(bulletPrefab, player.GetPos(), Quaternion.identity));
			timeSinceLastShot = 0.0f;
		}
	}
	
	private void LaunchSinePattern()
	{
		if (canFire) {
			for (int i = 0; i < 2; i++) {
				bulletList.Add(Instantiate(bulletPrefab, player.GetPos(), Quaternion.identity));
				bulletLaunchPos.Add(player.GetPos().x);
				bulletLaunchTime.Add(Time.realtimeSinceStartup);
				//bulletLaunchPos[bulletList.Count-1] = player.GetPos().x;
				//bulletLaunchTime[bulletList.Count-1] = Time.realtimeSinceStartup;
			}
			timeSinceLastShot = 0.0f;
		}
	}

	private void LaunchBurstPattern()
	{
		if (canFire) {
			for (int i = 0; i < 3; i++) {
				bulletList.Add(Instantiate(bulletPrefab, player.GetPos(), Quaternion.identity));
			}
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
		else if (bulletPatternType == BulletPatternType.Burst) {
			MoveBurstBullets(bulletX, bulletY, bulletZ);
		}
	}

	void MoveBoringBullets(float bulletX, float bulletY, float bulletZ)
	{
		for (int i = 0; i < bulletList.Count; i++) {
			bulletX = bulletList[i].transform.position.x;
			bulletY = bulletList[i].transform.position.y + bulletSpeed * Time.deltaTime;
			bulletZ = bulletList[i].transform.position.z;
			bulletList[i].transform.position = new Vector3 (bulletX, bulletY, bulletZ);
		}
	}

	void MoveSineBullets(float bulletX, float bulletY, float bulletZ)
	{
		for (int i = 0; i < bulletList.Count; i++) {
			bulletX = sineAmplitude * SineOffset(i);
			bulletY = bulletList[i].transform.position.y + bulletSpeed/2 * Time.deltaTime;
			bulletZ = bulletList[i].transform.position.z;
			if (i % 2 == 0) {
				bulletList[i].transform.position = new Vector3 (bulletLaunchPos[i]-bulletX, bulletY, bulletZ);
			}
			else {
				bulletList[i].transform.position = new Vector3 (bulletLaunchPos[i]+bulletX, bulletY, bulletZ);
			}
		}
	}

	void MoveBurstBullets(float bulletX, float bulletY, float bulletZ)
	{
		Vector3 leftVec = new Vector3 (
			-bulletSpeed, 
			bulletSpeed,
			0.0f
		);
		Vector3 rightVec = new Vector3 (
			bulletSpeed, 
			bulletSpeed,
			0.0f
		);

		for (int i = 0; i < bulletList.Count; i++) {
			int direction = 1;
			/*
			if (bulletList[i].transform.position.x < playArea.minX || 
				bulletList[i].transform.position.x > playArea.maxX) {
				direction = -1;
			}
			*/

			if (i % 3 == 0) { // Left
				bulletList[i].transform.position += new Vector3 (
					direction * -bulletSpeed * Time.deltaTime,
					bulletSpeed* Time.deltaTime,
					0
				);
			}
			else if (i % 3 == 1) { // Middle
				bulletList[i].transform.position += Vector3.up;
			}
			else { // Right
				bulletList[i].transform.position += new Vector3 (
					direction * bulletSpeed * Time.deltaTime,
					bulletSpeed* Time.deltaTime,
					0
				);
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

	float SineOffset(int index)
	{
		float timePassed = Time.realtimeSinceStartup - bulletLaunchTime[index];
		return Mathf.Sin(2* Mathf.PI* sineFrequency * timePassed);
	}

	void RemoveBullets()
	{
		for (int i = 0; i < bulletList.Count; i++) {
			if (bulletList[i].transform.position.y > playArea.maxY) {
					Destroy(bulletList[i]);
					bulletList.RemoveAt(i);
					if(bulletPatternType == BulletPatternType.Sine) {
						bulletLaunchPos.RemoveAt(i);
						bulletLaunchTime.RemoveAt(i);
					}
			}
		}
	}
}
