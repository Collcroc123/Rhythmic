using System.Collections;
using UnityEngine;

public class ScrollList : MonoBehaviour
{
    private Vector3 endPos;
    private Vector3 startPos;
    private float speed = 20f;
    private float distance = 1.603333f;

    private void Start()
    {
        startPos = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown("up") || Input.GetAxis("Mouse ScrollWheel") > 0f) //Forward
        {
            GoUp();
        }
        else if (Input.GetKeyDown("down") || Input.GetAxis("Mouse ScrollWheel") < 0f) //Back
        {
            GoDown();
        }
    }

    IEnumerator Move()
    {
        while (transform.position != endPos)
        {
            transform.position = Vector3.Lerp(transform.position,endPos, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            if (Mathf.Abs(transform.position.y - endPos.y) < 0.05f)
            {
                startPos = endPos;
                transform.position = endPos;
            }
        }
    }

    public void GoUp()
    {
        endPos = new Vector3(4, startPos.y - (distance), 0);
        if (endPos.y < 0f)
        {
            endPos.y = 0f;
        }
        StartCoroutine(Move());
    }

    public void GoDown()
    {
        endPos = new Vector3(4, startPos.y + (distance), 0); //2.77777f
        StartCoroutine(Move());
    }
}