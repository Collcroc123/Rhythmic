using System.Collections;
using UnityEngine;

public class NewNoteMove : MonoBehaviour
{
    //public SongInformation songInfo;
    private float songBpm;
    private float currentBps;
    private bool ready;
    public AudioSource source;
    private float speed = 10;
    public ArrayData songDatas;
    public IntData songNumber;

    void Start()
    {
        StartCoroutine(WaitMove());
    }

    void Update()
    {
        if (ready)
        {
            transform.position -= new Vector3(0f, currentBps * Time.deltaTime, 0f);
        }
    }
    
    private IEnumerator WaitMove()
    {
        yield return new WaitForSeconds(1.0f);
        songBpm = songDatas.songInfo[songNumber.value].bpm;
        print(songBpm);
        currentBps = songBpm / 60.0f;
        currentBps *= speed;
        source.Play();
        yield return new WaitForSeconds(songDatas.songInfo[songNumber.value].offset);
        ready = true;
    }
}