using System;
using UnityEngine;

public class MusicImport : MonoBehaviour
{
    private float listPos;
    public int songNum;
    private GameObject button;
    public GameObject listPrefab;

    private void Start()
    {
        //CreateList();
        //This script is junk ngl
    }
    /*
    void CreateList()
    {
        songNum++;
        var newSong = Instantiate(listPrefab, new Vector3(0,listPos,0), Quaternion.identity);
        newSong.transform.SetParent(gameObject.transform, false);
        newSong.GetComponent<SongInformation>().songNumber = songNum;
        listPos += -150;
    }*/

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