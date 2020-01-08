using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renderer : MonoBehaviour
{
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        //GetComponent<MeshRenderer>().enabled = true;
    }
}
