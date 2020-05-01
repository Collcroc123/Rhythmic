using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate512cubes : MonoBehaviour
{
    public GameObject cubePrefab;
    GameObject[] sampleCube = new GameObject[64];
    public float maxScale;

    void Start()
    {
        for(int i = 0; i < 64; i++)
        {
            GameObject cubeInstance = (GameObject)Instantiate(cubePrefab);
            cubeInstance.transform.position = this.transform.position;
            cubeInstance.transform.parent = this.transform;
            cubeInstance.name = "SampleCube" + i;
            this.transform.eulerAngles = new Vector3 (0, -0.1f * i, 0);
            cubeInstance.transform.position = Vector3.forward * 100;
            sampleCube[i] = cubeInstance;
        }
    }

    void Update()
    {
        for (int i = 0; i < 64; i++)
        {
            if (sampleCube != null)
            {
                sampleCube[i].transform.localScale = new Vector3(0.16f, (AudioPeer.samples[i] * maxScale) + 0.01f, 0.16f);
            }
        }
    }
}
