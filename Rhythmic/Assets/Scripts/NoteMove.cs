using System.Collections;
using UnityEngine;

public class NoteMove : MonoBehaviour
{
    //public SongInformation songInfo;
    private float songBpm;
    private float currentBps;
    private bool ready;
    public AudioSource source;
    private float speed = 10;
    public SongData song;

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
        print(songBpm);
        currentBps = song.displayBPM / 60.0f;
        currentBps *= speed;
        source.Play();
        yield return new WaitForSeconds(song.offset);
        ready = true;
    }

    void OnBecameInvisible()
    {
        Debug.Log("I'm not visible anymore");
    }

    void OnBecameVisible()
    {
        Debug.Log("Hey! I'm visible!");
    }
}