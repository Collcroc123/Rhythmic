using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class notePlacer1 : MonoBehaviour
{
    public Transform prefabRed; //Red note
    public Transform prefabYellow; //Yellow note
    public Transform prefabGreen; //Green note
    public Transform prefabBlue; //Blue note
    public Transform shortDouble; //1 wide orange line between notes
    public Transform normalDouble; //2 wide orange line between notes
    public Transform longDouble; //3 wide orange line between notes
    public Transform fret; //The white horizontal bars
    AudioSource music; //Plays the music
    string songName; //Used to access song file

    public NoteMove noteMove; //Gets NoteMove script

    int nextNote = 0; //Starts at 0
    int nextFret = 0; //Starts at 0

    void Start()
	{
        songName = "Wonderwall"; //Manual for now, will be automatic! Finds song file with this
        music = GetComponent<AudioSource>(); //Grabs AudioSource
        music.Pause(); //Pauses Music
        StartCoroutine(WaitSong()); //Invokes WaitSong()
	}

    private IEnumerator WaitSong()
    {
        yield return new WaitForSeconds(5.0f); //Give space before notes begin placing
        noteMove.currentBPS = noteMove.songBPM / 60.0f; //Sets Beats Per Second to the song's Beats Per Minute divided by 60
        NotePlace(); //Begins Placing Notes
        music.UnPause(); //Unpauses music
    }

    void NotePlace()
	{
        string[] lines = System.IO.File.ReadAllLines(@"./Assets/Songs/"+songName+"/song.txt"); //Creates an array where each line is a line from song.txt
		foreach (string line in lines) //Begin mapping notes
		{
            Instantiate(fret, new Vector3(0, -0.21f, nextFret - 0.35f), Quaternion.identity); //Every line, create a new fret

			if(line.ToLower().Contains("a")) //If txt lists a red note...
            {
                Instantiate(prefabRed, new Vector3(-4.1f, -0.25f, nextNote), Quaternion.identity); //Place a red note!
            }
            if (line.ToLower().Contains("w")) //If txt lists a yellow note...
            {
                Instantiate(prefabYellow, new Vector3(-2f, 0f-0.25f, nextNote), Quaternion.identity); //Place a yellow note!
            }
            if (line.ToLower().Contains("i")) //If txt lists a green note...
            {
                Instantiate(prefabGreen, new Vector3(0.1f, -0.25f, nextNote), Quaternion.identity); //Place a green note!
            }
            if (line.ToLower().Contains("l")) //If txt lists a blue note...
            {
                Instantiate(prefabBlue, new Vector3(2.2f, -0.25f, nextNote), Quaternion.identity); //Place a blue note!
            }
            else
            {
                return; //Error!
            }
            nextNote += 2; //Move forward 2 units for next note
            nextFret += 8; //Move forward 8 units for next fret
        }
	}
}

/*
//THE FOLLOWING IS FOR THE ORANGE LINES THAT APPEAR BETWEEN DOUBLE NOTES. I REMOVED THEM FOR NOW UNTIL I CAN FIND A CLEANER WAY TO WRITE IT.
if (line.ToLower().Contains("a"))
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
*/