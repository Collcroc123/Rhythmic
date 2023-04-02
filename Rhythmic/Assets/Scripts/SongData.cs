using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class SongData : ScriptableObject
{
    public string directory; // Folder path
	public string fileName; // .sm file
	public string group; // Parent folder name
	public string title; // Title displayed in menu
	public string subtitle; // Subtitle displayed in menu
	public string artist; // Artist displayed in menu
	public string banner; // Song banner displayed in menu
	public string background; // Song background displayed in game
	public string cdtitle; // Song icon displayed in menu
	public string music; // Song file name
	public float offset;
	public float sampleStart; // Plays portion when selected in menu
	public float sampleLength; // How long menu selection plays
	public float displayBPM; // BPM displayed in menu
	public List<float> bpms = new List<float>();
	public List<float> stops = new List<float>();

	//public List<List<int>> expertMap = new List<List<int>>();
	public Chart chart = new Chart();

    //public string titleTranslit;
	//else if (value.Contains("#TITLETRANSLIT:")) song.titleTranslit = ExtractData(value);
	//public string subtitleTranslit;
	//else if (value.Contains("#SUBTITLETRANSLIT:")) song.subtitleTranslit = ExtractData(value);
	//public string artistTranslit;
	//else if (value.Contains("#ARTISTTRANSLIT:")) song.artistTranslit = ExtractData(value);
	//public string genre;
	//else if (value.Contains("#GENRE:")) song.genre = ExtractData(value);
	//public string credit;
	//else if (value.Contains("#CREDIT:")) song.credit = ExtractData(value);
	//public string lyricsPath;
	//else if (value.Contains("#LYRICSPATH:")) song.lyricsPath = ExtractData(value);
	//public string selectable;
	//else if (value.Contains("#SELECTABLE:")) song.selectable = ExtractData(value);
	//public List<float> BGchanges = new List<float>();
	//else if (value.Contains("#BGCHANGES:")) song.BGchanges = ExtractData(value);
	//public List<float> keySounds = new List<float>();
	//else if (value.Contains("#KEYSOUNDS:")) song.keySounds = ExtractData(value);
	//ATTACKS, JACKET, FGCHANGES, LASTBEATHINT, NITGVERSION
}

[System.Serializable]
public class Measure
{
	public List<string> row = new List<string>();
}

[System.Serializable]
public class Chart
{
	public List<Measure> measure = new List<Measure>();
}