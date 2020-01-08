using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStart : MonoBehaviour
{
    AudioSource musicData;
    // Start is called before the first frame update
    void Start()
    {
        musicData = GetComponent<AudioSource>();
        musicData.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitPlayMusic()
    {
        yield return new WaitForSeconds(5);
        musicData.UnPause();
    }
}
