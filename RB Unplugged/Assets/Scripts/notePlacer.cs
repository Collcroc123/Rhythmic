using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class notePlacer : MonoBehaviour
{
    //public NoteMove noteMove;
    public Transform prefabRed; //red note
    public Transform prefabYellow; //yellow note
    public Transform prefabGreen; //green note
    public Transform prefabBlue; //blue note
    public Transform shortDouble; //1 wide orange line between notes
    public Transform normalDouble; //2 wide orange line between notes
    public Transform longDouble; //3 wide orange line between notes
    public Transform fret; //the white bars
    AudioSource music; //plays the music
    string songName; //used to access song file

    public NoteMove noteMove;

    int nextNote = 0; //starts at 0
    int nextFret = 0; //starts at 0

    void Start()
	{
        //GameObject moving = GameObject.Find("Moving");
        //noteMove = GetComponent<Note Move>(); //WHAT?!?! <--------
        //LoadData();
        songName = "Wonderwall";
        music = GetComponent<AudioSource>();
        music.Pause();
        StartCoroutine(WaitSong());
	}

	void Update()
	{

    }

    void NotePlace()
	{
        //Debug.Log("WHERE???");
        string[] lines = System.IO.File.ReadAllLines(@"./Assets/Songs/"+songName+"/song.txt");
		foreach (string line in lines)
		{
            Instantiate(fret, new Vector3(0, -0.21f, nextFret - 0.35f), Quaternion.identity);
			if(line.ToLower().Contains("a"))
            {
                if(line.ToLower().Contains("w"))
                {
                    Instantiate(shortDouble, new Vector3(-2f, -0.2f, nextNote - 0.35f), Quaternion.identity);
                }
                if (line.ToLower().Contains("i"))
                {
                    Instantiate(normalDouble, new Vector3(-1.0f, -0.2f, nextNote - 0.35f), Quaternion.identity);
                }
                if (line.ToLower().Contains("l"))
                {
                    Instantiate(longDouble, new Vector3(0.0f, -0.2f, nextNote - 0.35f), Quaternion.identity);
                }
                Instantiate(prefabRed, new Vector3(-4.1f, -0.25f, nextNote), Quaternion.identity);
            }
            if (line.ToLower().Contains("w"))
            {
                if (line.ToLower().Contains("i"))
                {
                    Instantiate(shortDouble, new Vector3(0.0f, -0.2f, nextNote - 0.35f), Quaternion.identity);
                }
                if (line.ToLower().Contains("l"))
                {
                    Instantiate(normalDouble, new Vector3(1.0f, -0.2f, nextNote - 0.35f), Quaternion.identity);
                }
                Instantiate(prefabYellow, new Vector3(-2f, 0f-0.25f, nextNote), Quaternion.identity);
            }
            if (line.ToLower().Contains("i"))
            {
                if (line.ToLower().Contains("l"))
                {
                    Instantiate(shortDouble, new Vector3(2.2f, -0.2f, nextNote - 0.35f), Quaternion.identity);
                }
                Instantiate(prefabGreen, new Vector3(0.1f, -0.25f, nextNote), Quaternion.identity);
            }
            if (line.ToLower().Contains("l"))
            {
                Instantiate(prefabBlue, new Vector3(2.2f, -0.25f, nextNote), Quaternion.identity);
            }
            nextNote += 2;
            nextFret += 8;
        }
	}

    private IEnumerator WaitSong()
    {
        yield return new WaitForSeconds(5.0f);
        noteMove.currentBPS = noteMove.songBPM / 60.0f;
        NotePlace();
        music.UnPause();
    }
}
