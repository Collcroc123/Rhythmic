using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SongPlayer : MonoBehaviour
{
    public IntData songNumber;
    private Animator fade;
    private AudioSource source;
    private AudioClip clip;
    //[HideInInspector]public Slider timeBar;
    [HideInInspector] public bool loopBool;
    [HideInInspector] public bool muteBool;
    //[HideInInspector] public Text currentArtist;
    //[HideInInspector] public Text currentSong;
    //[HideInInspector] public Image currentBackground;
    public GameObject background;
    private string location;
    public ArrayData songDatas;

    private void Awake()
    {
        source = GameObject.Find("NoteBoard").GetComponent<AudioSource>();
        fade = GameObject.Find("FadePanel").GetComponent<Animator>();
        //location = songDatas.songInfo[songNumber.value].songAddress;
        print(location);
        StartCoroutine(LoadAudio());
        //background.SetActive(false);
    }

    public void Update()
    {
        //timeBar.value = source.time;
    }

    private IEnumerator LoadAudio()
    {
        //yield return new WaitForSeconds(0.25f);
        WWW request = GetAudioFromFile(location);
        yield return request;
        
        clip = request.GetAudioClip();
        clip.name = location;
        print("Playing: " + clip);
        source.clip = clip;
        //timeBar.maxValue = source.clip.length;
        //source.Play();
        //currentArtist.text = songDatas.songInfo[songNumber.value].artist;
        //currentSong.text = songDatas.songInfo[songNumber.value].title;
        //background.SetActive(true);
    }
    
    private WWW GetAudioFromFile(string path)
    {
        string audioToLoad = string.Format(path); // + "{0}", filename
        WWW request = new WWW(audioToLoad);
        return request;
    }

    public void ExitScene()
    {
        StartCoroutine(ExitMusic());
    }
    
    public IEnumerator ExitMusic()
    {
        fade.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2.75f);
        SceneManager.LoadScene("Main Menu");
    }
}
