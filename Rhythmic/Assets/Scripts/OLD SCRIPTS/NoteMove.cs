using UnityEngine;

public class NoteMove : MonoBehaviour
{
    public float songBPM = 120f;
    private float currentBPS = 0.0f;
    //float wonderWall = 88.0f;

    void Start()
    {
        currentBPS = songBPM / 60.0f;
        currentBPS *= 4f;
    }

    void FixedUpdate()
    {
        transform.position -= new Vector3(0f, 0f, currentBPS * Time.deltaTime);
    }
}