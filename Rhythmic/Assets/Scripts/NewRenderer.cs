using UnityEngine;
using UnityEngine.UI;

public class NewRenderer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Image>().enabled = true;
    }
}
