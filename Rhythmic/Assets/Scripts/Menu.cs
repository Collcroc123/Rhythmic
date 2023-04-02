using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Animator fade;
    public GameObject groupPrefab, entryPrefab;
    public GameObject scrollList;
    public RawImage banner;
    public Text artist, BPM, length;
    public MusicData music;
    public AudioSource source;
    private GameObject currentGroup;
    
    private bool fading;

    public void CreateList()
    {
        foreach(List<SongData> group in music.groupList)
        {
            CreateGroup(group[0].group);
            foreach(SongData song in group)
            {
                CreateEntry(song);
            }
        }
    }

    void CreateGroup(string groupTitle)
    {
        var newGroup = Instantiate(groupPrefab);
        newGroup.transform.SetParent(scrollList.transform, false);
        newGroup.transform.GetChild(0).GetComponent<Text>().text = groupTitle;
        var temp = System.IO.Directory.GetFiles(music.directory+groupTitle, "*.png");
        if (temp.Length > 0) newGroup.GetComponent<SongInformation>().groupBanner = temp[0];
        newGroup.GetComponent<Button>().onClick.AddListener(source.Stop);
        currentGroup = newGroup;
        //print("GROUP: "+ groupTitle);
    }

    void CreateEntry(SongData song)
    {
        var newSong = Instantiate(entryPrefab);
        newSong.transform.SetParent(scrollList.transform, false);
        newSong.GetComponent<SongInformation>().song = song;
        currentGroup.GetComponent<SongInformation>().groupChildren.Add(newSong.transform.gameObject);
        newSong.GetComponent<SongInformation>().GetInfo();
        //print("SONG: "+ song.fileName);
    }

    public void DisplayInfo(SongData song)
	{
        music.currentSong = song;
		artist.text = song.artist;
		BPM.text = song.displayBPM.ToString();
        StartCoroutine(PlaySnippet(song));
	}

    IEnumerator PlaySnippet(SongData song)
    {
        source.clip = null;
        source.Stop();
        yield return new WaitForSeconds(0.5f);
        string dir = song.directory.Replace(@"C:\Users\Collin\Documents\Repos\Rhythmic\Rhythmic\Assets\Resources\", "")+"\\"+song.music;
        AudioClip clip = (AudioClip)Resources.Load(dir, typeof(AudioClip));
        source.clip = clip;
        source.time = song.sampleStart;
        length.text = source.clip.length.ToString();
        source.Play();
        source.SetScheduledEndTime(AudioSettings.dspTime+(song.sampleLength));
    }

    void Update()
    {
        if (source.isPlaying && !fading && source.time >= music.currentSong.sampleStart+music.currentSong.sampleLength-1f)
        {
            fading = true;
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        float startVolume = source.volume;
        while (source.volume > 0f)
        {
            source.volume -= startVolume * Time.deltaTime / 1f;
            yield return null;
        }
        source.Stop();
        source.volume = startVolume;
        source.time = music.currentSong.sampleStart;
        source.Play();
        source.SetScheduledEndTime(AudioSettings.dspTime+(music.currentSong.sampleLength));
        fading = false;
    }

    public void Select()
    {
        if (music.currentSong != null)
        {
            //StartCoroutine(LoadGame());
            SceneManager.LoadScene("Level");
        }
    }

    IEnumerator LoadGame()
    {
        fade.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level");
    }
}
