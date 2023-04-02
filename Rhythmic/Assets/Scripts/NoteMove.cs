using System.Collections;
using UnityEngine;

public class NoteMove : MonoBehaviour
{
    public MusicData music;
    public Manager manager;
    private float currentBps;
    public float speed = 8;

    private bool ready;

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
        currentBps = (music.currentSong.displayBPM / 60.0f) * speed;
        yield return new WaitForSeconds(music.currentSong.offset);
        ready = true;
    }
}