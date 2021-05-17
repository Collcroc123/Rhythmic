using UnityEngine;

public class Renderer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<MeshRenderer>().enabled = true;
    }
}
