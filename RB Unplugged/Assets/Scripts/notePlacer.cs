using System.Collections;
using UnityEngine;

public class notePlacer : MonoBehaviour
{
    public GameObject prefabRed; //Red note
    public GameObject prefabYellow; //Yellow note
    public GameObject prefabGreen; //Green note
    public GameObject prefabBlue; //Blue note
    public GameObject noteSled; //Moves the notes
    AudioSource music; //Plays music
    string songName = "Wonderwall"; //Accesses song file
    float nextNote = 0.0f; //Spaces out notes

    void Start()
	{
        music = GetComponent<AudioSource>();
        StartCoroutine(WaitSong());
	}

    void NotePlace()
	{
        string[] lines = System.IO.File.ReadAllLines(@"./Assets/Songs/"+songName+"/song.txt");
		foreach (string line in lines)
		{
            if(line.ToLower().Contains("a"))
            {
                (Instantiate(prefabRed, new Vector3(-4.1f, -0.25f, nextNote), Quaternion.identity) as GameObject).transform.parent = noteSled.transform;
            }
            if (line.ToLower().Contains("w"))
            {
                (Instantiate(prefabYellow, new Vector3(-2f, -0.25f, nextNote), Quaternion.identity) as GameObject).transform.parent = noteSled.transform;
            }
            if (line.ToLower().Contains("i"))
            {
                (Instantiate(prefabGreen, new Vector3(0.1f, -0.25f, nextNote), Quaternion.identity) as GameObject).transform.parent = noteSled.transform;
            }
            if (line.ToLower().Contains("l"))
            {
                (Instantiate(prefabBlue, new Vector3(2.2f, -0.25f, nextNote), Quaternion.identity) as GameObject).transform.parent = noteSled.transform;
            }
            nextNote += 1f;
        }
	}

    private IEnumerator WaitSong()
    {
        music.Pause();
        yield return new WaitForSeconds(1.0f);
        NotePlace();
        music.UnPause();
    }
}