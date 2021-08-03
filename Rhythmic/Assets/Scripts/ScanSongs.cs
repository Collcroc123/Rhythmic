using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanSongs : MonoBehaviour
{
    private string[] albumFolders; //Contains All the Group Folders
    private string[] songFolders; //Contains All Song Folders
    private string[] songNames; //Contains All Song Files
    public ArrayData songDatas;
    public int textNum;
    public static string directory = @"C:\Games\StepMania 5\Songs\";
    private float listPos;
    private GameObject button;
    public GameObject listPrefab;
    public GameObject listSled;
    
    void Start()
    {
        FindSongs();
    }
    
    public void FindSongs()
    {
        songDatas.Clear();
        textNum = 0;
        print("Loading Songs...");
        albumFolders = System.IO.Directory.GetDirectories(directory);
        foreach (string folder in albumFolders)
        {
            songFolders = System.IO.Directory.GetDirectories(folder);
            foreach (string album in songFolders)
            {
                songNames = System.IO.Directory.GetFiles(album);
                foreach (string song in songNames)
                {
                    print("Checking Folders...");
                    if (song.Contains(".ssc") || song.Contains(".sm"))
                    {
                        textNum++;
                        songDatas.songInfo[textNum] = ScriptableObject.CreateInstance<SongData>();
                        songDatas.songInfo[textNum].textAddress = song;
                        songDatas.songInfo[textNum].baseDirectory = album;
                        CreateList();
                        //songDatas.songInfo[textNum].songNum = textNum;
                    }
                }
            }
        }
    }
    void CreateList()
    {
        var newSong = Instantiate(listPrefab, new Vector3(0,listPos,0), Quaternion.identity);
        newSong.transform.SetParent(listSled.transform, false);
        newSong.GetComponent<SongInformation>().songNumber = textNum;
        listPos += -150;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        button = other.gameObject.transform.GetChild(0).gameObject;
        button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        button = other.gameObject.transform.GetChild(0).gameObject;
        button.SetActive(false);
    }
}
