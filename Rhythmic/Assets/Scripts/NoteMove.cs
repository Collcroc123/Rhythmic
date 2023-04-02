using System.Collections;
using UnityEngine;

public class NoteMove : MonoBehaviour
{
    public MusicData music;
    public Manager manager;
    private float songBpm;
    private float currentBps;
    private bool ready;
    public AudioSource source;
    private float speed = 10;

    void Start()
    {
        StartCoroutine(WaitMove());
    }

    void Update()
    {
        if (ready && manager.ready)
        {
            if (!manager.source.isPlaying) manager.source.Play();
            transform.position -= new Vector3(0f, currentBps * Time.deltaTime, 0f);
        }
    }
    
    private IEnumerator WaitMove()
    {
        yield return new WaitForSeconds(1.0f);
        print(songBpm);
        currentBps = music.currentSong.displayBPM / 60.0f;
        currentBps *= speed;
        source.Play();
        yield return new WaitForSeconds(music.currentSong.offset);
        ready = true;
    }
    
    /*void OnBecameInvisible()
    {
        Debug.Log("I'm not visible anymore");
    }

    void OnBecameVisible()
    {
        Debug.Log("Hey! I'm visible!");
    }*/
}