using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScanSongs : MonoBehaviour
{
    public string directory = @"C:/Users/Collin/Documents/Project OutFox/Songs/";
    public GameObject groupPrefab, entryPrefab;
    public GameObject scrollList;
    private GameObject currentGroup;
    
    public List<List<SongData>> groupList = new List<List<SongData>>(); // Stores list of all groups
    
    void Awake()
    {
        Scanner(true);
    }

    public void Scanner(bool firstTime)
    {
        if(!firstTime) return;
        List<string> groupDirList = new List<string>(); // Stores list of group folders
        List<string> songDirList = new List<string>(); // Stores list of song folders
        groupDirList.AddRange(System.IO.Directory.GetDirectories(directory));

        var groupFolders = System.IO.Directory.GetDirectories(directory);
        foreach (string group in groupFolders)
        {
            print("GROUP: "+ group.Replace(directory, ""));
            CreateGroup(group.Replace(directory, ""));
            List<SongData> songList = new List<SongData>(); // Stores list of songs in current group

            var songFolders = System.IO.Directory.GetDirectories(group);
            foreach (string dir in songFolders)
            {
                var songNames = System.IO.Directory.GetFiles(dir, "*.sm");
                if (songNames.Length <= 0) songNames = System.IO.Directory.GetFiles(dir, "*.ssc");
                foreach (string file in songNames)
                {
                    var song = new SongData(); // Creates new Song
                    int index = file.LastIndexOf("\\"); // Finds last slash in Directory
					song.fileName = file.Substring(index+1);
					song.groupName = group.Replace(directory, "");
                    song.directory = dir + "\\";

                    string[] lines = System.IO.File.ReadAllLines(file);
                    foreach (string line in lines)
                    {
                        if (!line.Contains("#NOTES:"))
                        {
	            			if (line.Contains("#TITLE:")) song.title = ExtractData(line);
	            			else if (line.Contains("#SUBTITLE:")) song.subtitle = ExtractData(line);
	            			else if (line.Contains("#ARTIST:")) song.artist = ExtractData(line);
	            			else if (line.Contains("#BANNER:")) 
	            			{
                                var temp = System.IO.Directory.GetFiles(song.directory, "*bn.png");
                                if (temp.Length > 0) song.banner = temp[0].Replace(song.directory, "");
	            				else song.banner = ExtractData(line);
	            				/*if (song.banner.Length < 3) 
	            				{
	            					List<string> temp = new List<string>(); // Search for banner manually
	            					temp.AddRange(System.IO.Directory.GetFiles(song.directory, "*bn.png"));
	            					song.banner = temp[0];
	            				}*/
	            			}
	            			else if (line.Contains("#BACKGROUND:"))
	            			{
                                var temp = System.IO.Directory.GetFiles(song.directory, "*bg.png");
                                if (temp.Length > 0) song.background = temp[0].Replace(song.directory, "");
                                else song.background = ExtractData(line);
				            	/*if (song.background.Length < 3) 
				            	{
				            		List<string> temp = new List<string>(); // Search for banner manually
				            		temp.AddRange(System.IO.Directory.GetFiles(song.directory, "*bg.png"));
				            		song.background = temp[0];
				            	}*/
				            }
			            	//else if (line.Contains("#CDTITLE:")) song.cdtitle = ExtractData(line);
			            	else if (line.Contains("#MUSIC:")) song.music = ExtractData(line);
	            			else if (line.Contains("#OFFSET:")) song.offset = float.Parse(ExtractData(line)); //, CultureInfo.InvariantCulture.NumberFormat
	            			else if (line.Contains("#SAMPLESTART:")) song.sampleStart = float.Parse(ExtractData(line)); //, CultureInfo.InvariantCulture.NumberFormat
	            			else if (line.Contains("#SAMPLELENGTH:")) song.sampleLength = float.Parse(ExtractData(line)); //, CultureInfo.InvariantCulture.NumberFormat
	            			else if (line.Contains("#DISPLAYBPM:")) song.displayBPM = ExtractData(line);
	            			//else if (line.Contains("#BPMS:")) song.BPMS = ExtractData(line);
	            			//else if (line.Contains("#STOPS:")) song.stops = ExtractData(line);
	            			//else if (line.Contains('#') && !line.Contains("NOTES")) Log.Info("❌ NOT TRACKED: "+line);//.Substring(0, line.IndexOf(':')));
                        }
                        /*else
                        {
                            if (line.Length == 4)
                            {
                                lineCount++;
                            }
                            else if (line == ",")
                            {
                                song.expertMap[currentMeasure] = lineCount; //HERE
                                currentMeasure++;
                                lineCount = 0;
                            }
                            else if (line == ";")
                            {
                                return;
                            }
                        }*/
                    }
                    print("SONG: "+ song.title);
	                songList.Add(song);
                    CreateEntry(song);
                }
            }
            groupList.Add(songList);
        }
    }

    public string ExtractData(string line)
	{
		int dataStart = line.IndexOf(':')+1;
		int dataEnd = line.LastIndexOf(';');
		return line.Substring(dataStart, dataEnd - dataStart);
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
        newSong.transform.GetChild(0).GetComponent<Text>().text = newSong.GetComponent<SongInformation>().song.title;
        currentGroup.GetComponent<SongInformation>().groupChildren.Add(newSong.transform.gameObject);
        newSong.SetActive(false);
    }

    public static void DisplayInfo(SongData entry)
	{/*
		Banner.Texture = Texture.Load(FileSystem.Data, entry.directory+"/"+entry.banner);
		Artist.Text = "🎨 " + entry.artist;
		BPM.Text = "🥁 " + entry.displayBPM + " BPM";
		Time.Text = "⏳ " + entry.songLength;*/
	}
}
