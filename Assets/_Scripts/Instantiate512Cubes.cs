using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate512Cubes : MonoBehaviour
{
    public GameObject sampleCubePrefab;
    const int numCubes = 512;
    const float theta = 360.0f/numCubes;
    const int cubeDistance = 100;
    const int cubeScale = 10;
    public float maxScale = 100000;
    public float cubeHeight = 2;
    GameObject[] sampleCube = new GameObject[numCubes];
    AudioPeer audioPeer;


    void Start()
    {
		audioPeer = FindObjectOfType<AudioPeer>();
        for(int i = 0; i < numCubes; i++) {
            GameObject instanceSampleCube = (GameObject) Instantiate(sampleCubePrefab);
            instanceSampleCube.transform.position = this.transform.position;
            instanceSampleCube.transform.parent = this.transform;
            instanceSampleCube.name = "Cube" + i;
            this.transform.eulerAngles = new Vector3(0, theta * i, 0);
            instanceSampleCube.transform.position = Vector3.forward * cubeDistance;
            sampleCube[i] = instanceSampleCube;
        }
        
    }

    void Update()
    {
        for (int i = 0; i < numCubes; i++) {
            if (sampleCube != null) {
                //sampleCube[i].transform.localScale = new Vector3(cubeScale, audioPeer.samplesLeft[i] * maxScale + 2,cubeScale);
            }
        }
    }
}
