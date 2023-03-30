using System.Collections;
using UnityEngine;

public class NotePlacer : MonoBehaviour
{
    public GameObject prefabGreen; //Green Note
    public GameObject prefabBlue; //Blue Note
    public GameObject prefabRed; //Red Note
    public GameObject prefabPurple; //Purple Note
    private GameObject newNote; //Placeholder Note
    private float nextNote, nextRow, nextArrow; //Space Between Rows, Vertical Loc, Horizontal Loc
    private int currentMeasure, measureNum = 4; //Point in Measure Array, Number of Lines in Measure
    private bool placing;
    public SongData song;

    void Start()
	{
		nextNote = 1500f/measureNum;
		StartCoroutine(WaitSong());
		/*READING .sm FILES!
		1's = Normal note
		2's = Hold Button
		3's = End of Hold / Mash
		4's = Mash
		M's = Mines (ignore)
		, represents a measure
		number of lines between , represents time signature for that measure
		F = Full, H = Half, T = Third, Q = Quarter, E = Eighth, etc.
		4 lines = F x4
		8 lines = F,H x4
		12 lines = F, 
		16 lines = F,Q,H,Q x4
		24 lines = F,T,T,H,T,T x4
		32 lines = F,E,Q,E,H,E,Q,E x4
		48 lines = F,T,T,Q,T,T,H,T,T,Q,T,T x4
		; = End of the Song (difficulty)
		*/
	}

    void NotePlace()
	{
        string[] lines = System.IO.File.ReadAllLines(song.fileName);
        foreach (string line in lines)
		{
			if (line.Contains("#NOTES:") && !placing)
			{
				placing = true;
			}
			
			if (placing)
			{
				if (line.Length == 4)
				{
					foreach (char num in line)
					{
						nextArrow++;
						if (num == '1' || num == '2' || num == '4')
						{
							if (nextArrow == 1)
							{
								newNote = Instantiate(prefabGreen, new Vector3(-210, nextRow, 0), Quaternion.Euler(0, 0, 90));
								newNote.transform.SetParent(gameObject.transform, false);
								//nextArrow++;
							}
							else if (nextArrow == 2)
							{
								newNote = Instantiate(prefabBlue, new Vector3(-70, nextRow, 0), Quaternion.Euler(0, 0, 180));
								newNote.transform.SetParent(gameObject.transform, false);
								//nextArrow++;
							}
							else if (nextArrow == 3)
							{
								newNote = Instantiate(prefabRed, new Vector3(70, nextRow, 0), Quaternion.Euler(0, 0, 0));
								newNote.transform.SetParent(gameObject.transform, false);
								//nextArrow++;
							}
							else if (nextArrow == 4)
							{
								newNote = Instantiate(prefabPurple, new Vector3(210, nextRow, 0), Quaternion.Euler(0, 0, -90));
								newNote.transform.SetParent(gameObject.transform, false);
								//nextArrow++;
								nextArrow = 0;
							}
						}
						else if ((num == '0' || num == '3' || num == 'M') && nextArrow == 4)
						{
							nextArrow = 0;
						}
					}
					nextRow += nextNote;
				}
				else if (line == ",")
				{
					currentMeasure++;
					measureNum = song.expertMap[currentMeasure];
					print(measureNum);
					//A = B/C, B = measureNum * distance moved for 1 full note
					nextNote = 1500f/measureNum;
				}
				else if (line == ";")
				{
					return;
				}
			}
		}
	}
    
    private IEnumerator WaitSong()
    {
	    yield return new WaitForSeconds(1.0f);
	    measureNum = song.expertMap[0];
	    NotePlace();
    }
}