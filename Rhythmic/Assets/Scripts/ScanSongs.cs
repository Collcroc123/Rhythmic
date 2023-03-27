using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScanSongs : MonoBehaviour
{
    public string directory = @"C:\Users\Collin\Documents\Repos\Rhythmic\Rhythmic\Assets\Resources\Music\"; //"C:/Users/Collin/Documents/Project OutFox/Songs/";
    public GameObject groupPrefab, entryPrefab;
    public GameObject scrollList;
    private GameObject currentGroup;
    
    public List<List<SongData>> groupList = new List<List<SongData>>(); // Stores list of all groups

    public RawImage banner;
    public Text artist, BPM, length;

    void Awake()
    {
        Scan(true);
    }

    public void Scan(bool firstTime)
    {
        if(!firstTime) return;
        var groupFolders = System.IO.Directory.GetDirectories(directory);
        foreach (string group in groupFolders)
        {
            string groupName = group.Replace(directory, "");
            //print("GROUP: "+ groupName);
            CreateGroup(groupName);
            List<SongData> songList = new List<SongData>(); // Stores list of songs in current group
            var songFolders = System.IO.Directory.GetDirectories(group);
            foreach (string dir in songFolders)
            {
                var songNames = System.IO.Directory.GetFiles(dir, "*.sm");
                if (songNames.Length <= 0) songNames = System.IO.Directory.GetFiles(dir, "*.ssc");
                foreach (string file in songNames)
                {
                    var song = ScriptableObject.CreateInstance<SongData>(); // Creates new Song
					song.fileName = file;
                    song.directory = System.IO.Directory.GetParent(file).ToString();
					song.group = groupName;
	                songList.Add(song);
                    CreateEntry(song);
                }
            }
            groupList.Add(songList);
        }
    }

    void CreateGroup(string groupTitle)
    {
        var newGroup = Instantiate(groupPrefab);
        newGroup.transform.SetParent(scrollList.transform, false);
        newGroup.transform.GetChild(0).GetComponent<Text>().text = groupTitle;
        currentGroup = newGroup;
        var temp = System.IO.Directory.GetFiles(directory+groupTitle, "*.png");
        if (temp.Length > 0) newGroup.GetComponent<SongInformation>().groupBanner = temp[0];
    }

    void CreateEntry(SongData songD)
    {
        var newSong = Instantiate(entryPrefab);
        newSong.transform.SetParent(scrollList.transform, false);
        newSong.GetComponent<SongInformation>().song = songD;
        currentGroup.GetComponent<SongInformation>().groupChildren.Add(newSong.transform.gameObject);
        newSong.GetComponent<SongInformation>().GetInfo();
    } 
}