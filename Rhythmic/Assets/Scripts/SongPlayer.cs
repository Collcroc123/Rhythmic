using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SongPlayer : MonoBehaviour
{
    public AudioSource source;
    public SongData song;

    private AudioClip clip;
    //public Slider timeBar;

    private void Awake()
    {
        StartCoroutine(LoadAudio());
    }

    public void Update()
    {
        //timeBar.value = source.time;
    }

    private IEnumerator LoadAudio()
    {
        //yield return new WaitForSeconds(0.25f);
        WWW request = GetAudioFromFile(song.music);
        yield return request;
        
        clip = request.GetAudioClip();
        clip.name = song.music;
        print("Playing: " + clip);
        source.clip = clip;
        //timeBar.maxValue = source.clip.length;
        //source.Play();
    }
    
    private WWW GetAudioFromFile(string path)
    {
        string audioToLoad = string.Format(path); // + "{0}", filename
        WWW request = new WWW(audioToLoad);
        return request;
    }
}
