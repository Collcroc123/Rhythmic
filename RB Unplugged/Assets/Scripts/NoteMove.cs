using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteMove : MonoBehaviour
{
    float currentBPS = 0.0f;
    float songBPM = 0.0f;
    float wonderWall = 88.0f;
    float smoothCriminal = 126.0f;
    float trustInYou = 170.0f;
    public Text speed;
    private float speedInt;

    void Start()
    {
        songBPM = wonderWall;
        currentBPS = songBPM / 60.0f;
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(0, 0, (currentBPS * 8.0f) * Time.deltaTime); //32 meters off!!!! (4 whole beats)
        speedInt = (currentBPS * 8.0f) * Time.deltaTime;
        speed.text = speedInt.ToString();
        //if notes moving, - before (currentBPS)
    }
    //coroutine - do this work, wait for seconds. While loop
}
