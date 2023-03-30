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

    private GameObject currentGroup;

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
        currentGroup = newGroup;
        var temp = System.IO.Directory.GetFiles(music.directory+groupTitle, "*.png");
        if (temp.Length > 0) newGroup.GetComponent<SongInformation>().groupBanner = temp[0];
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
		artist.text = song.artist;
		BPM.text = song.displayBPM.ToString();
		length.text = song.length;
        PlaySnippet();
	}

    public void PlaySnippet()
    {

    }

    public void Select()
    {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        fade.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level");
    }
}
