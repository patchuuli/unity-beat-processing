using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryAttack : MonoBehaviour
{
	public GameObject bulletPrefab;
	public float bulletSpeed = 50.0f;
	public float rateOfFire = 0.25f;
	public float timeSinceLastShot = 0.0f;

	private GameObject[] bulletArr = new GameObject[512];
	//private uint bulletArr.Length = 0;
	private bool canFire = true;
	private Player player;

    void Start()
    {
		player = FindObjectOfType<Player>();
    }

    void Update()
    {
		CoolDown();
		MoveBullets();
    }

	public void FireBullet() {
		if (canFire) {

			bulletArr[bulletArr.Length] = Instantiate(bulletPrefab, player.GetPos(), Quaternion.identity);
			Debug.Log("bulletArr.Length = " + bulletArr.Length);
			timeSinceLastShot = 0.0f;
		}
	}

	void MoveBullets()
	{
		for (int i = 0; i < bulletArr.Length; i++) {
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
