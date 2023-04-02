using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NotePlacer : MonoBehaviour
{
	public MusicData music;
    public GameObject arrowPrefab;
	public float measureDistance = 3000f;
    private float currentNote = 1, currentRow = -29000, nextRow;

    void Start()
	{
		//music.currentSong.chart.measure.Clear();
		Scan();
		//Place();
	}

	void Scan()
	{
		bool placing = false;
		int currentMeasure = 0;
		Measure first = new Measure();
		music.currentSong.chart.measure.Add(first);
		string[] lines = System.IO.File.ReadAllLines(music.currentSong.fileName);
        foreach (string line in lines)
		{
			if (line.Contains("#NOTES:")) placing = true;
			else if (placing)
			{
				if (line == "" || line == null) continue;
				if (line.StartsWith(";")) break; // End of the Song (difficulty)
				if (line.StartsWith(","))
				{ // End of previous measure / start of next measure
					Measure temp = new Measure();
					music.currentSong.chart.measure.Add(temp);
					currentMeasure++;
					continue;
				}
				else if (line.Length == 4) music.currentSong.chart.measure[currentMeasure].row.Add(line);
			}
		}
		Place();
	}

	void Place()
	{
		foreach (Measure measure in music.currentSong.chart.measure)
		{
			/*switch (measure.row.Count)
        	{ // F = Full, H = Half, T = Third, Q = Quarter, E = Eighth, etc.
        	case 4: // 4 lines = F x4
        	    break;
        	case 8: // 8 lines = F,H x4
    		    break;
        	case 12: // 12 lines = F,T,T x4
        	    break;
    		case 16: // 16 lines = F,Q,H,Q x4
    		    break;
    		case 24: // 24 lines = F,T,T,H,T,T x4
        	    break;
        	case 32: // 32 lines = F,E,Q,E,H,E,Q,E x4
        	    break;
			case 48: // 48 lines = F,T,T,Q,T,T,H,T,T,Q,T,T x4
				break;
        	default:
        	    print ("ERROR! UNSUPPORTED MEASURE SIZE: " + measure.row.Count);
        	    break;
        	}*/

			nextRow = measureDistance/measure.row.Count;
			foreach (string row in measure.row)
			{
				foreach (char note in row)
				{
					if (note == '1' || note == '2' || note == '4')
					{
						if (currentNote == 1) CreateNote(note, -210, -90, new Color(1f, 0f, 0f, 1f));
						else if (currentNote == 2) CreateNote(note, -70, 0, new Color(0f, 1f, 0f, 1f));
						else if (currentNote == 3) CreateNote(note, 70, 180, new Color(0f, 0f, 1f, 1f));
						else if (currentNote == 4) CreateNote(note, 210, 90, new Color(1f, 1f, 0f, 1f));
					}
					else if ((note == '0' || note == '3' || note == 'M') && currentNote == 4) currentNote = 0;
					currentNote++; // 1-2-3-4
				}
				currentRow += nextRow;
			}
		}
	}

	void CreateNote(char noteType, float hPos, float rot, Color col)
	{
		print("PLACED NOTE " + currentNote);
		GameObject newNote = Instantiate(arrowPrefab, new Vector3(hPos, currentRow, 0), Quaternion.Euler(0, 0, rot));
		newNote.transform.SetParent(gameObject.transform, false);
		newNote.GetComponent<Image>().color = col;
		if (currentNote == 4) currentNote = 0;
	}
}