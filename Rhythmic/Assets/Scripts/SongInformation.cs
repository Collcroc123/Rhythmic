using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SongInformation : MonoBehaviour
{
    public SongData song;
    private Menu manager;
    public List<GameObject> groupChildren = new List<GameObject>();
    public string groupBanner;
    private bool collapsed = true;

    void Awake()
    {
        manager = GameObject.Find("Manager").GetComponent<Menu>();
    }

    public void GetInfo()
    {
        string[] lines = System.IO.File.ReadAllLines(song.fileName);
        foreach (string line in lines)
        {
            if (!line.Contains("#NOTES:"))
            {
				if (line.Contains("#TITLE:")) 
                {
                    song.title = ExtractData(line);
                    transform.GetChild(0).GetComponent<Text>().text = song.title;
                }
				else if (line.Contains("#SUBTITLE:")) song.subtitle = ExtractData(line);
				else if (line.Contains("#ARTIST:")) song.artist = ExtractData(line);
				else if (line.Contains("#BANNER:")) 
				{
                    var temp = System.IO.Directory.GetFiles(song.directory, "*bn.png");
                    if (temp.Length > 0) song.banner = temp[0]; //.Replace(song.directory, "")
					else song.banner = song.directory + "\\" + ExtractData(line);
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
                    if (temp.Length > 0) song.background = temp[0]; //.Replace(song.directory, "")
                    else song.background = song.directory + "\\" + ExtractData(line);
	            	/*if (song.background.Length < 3) 
	            	{
	            		List<string> temp = new List<string>(); // Search for banner manually
	            		temp.AddRange(System.IO.Directory.GetFiles(song.directory, "*bg.png"));
	            		song.background = temp[0];
	            	}*/
			    }
				else if (line.Contains("#CDTITLE:")) song.cdtitle = ExtractData(line);
				else if (line.Contains("#MUSIC:")) song.music = ExtractData(line);
	        	else if (line.Contains("#OFFSET:")) song.offset = float.Parse(ExtractData(line)); //, CultureInfo.InvariantCulture.NumberFormat
	        	else if (line.Contains("#SAMPLESTART:")) song.sampleStart = float.Parse(ExtractData(line)); //, CultureInfo.InvariantCulture.NumberFormat
	        	else if (line.Contains("#SAMPLELENGTH:")) song.sampleLength = float.Parse(ExtractData(line)); //, CultureInfo.InvariantCulture.NumberFormat
	        	else if (line.Contains("#DISPLAYBPM:")) 
                {
                    //if (line.Contains())
                    //song.displayBPM = Mathf.Round(float.Parse(ExtractData(line)));
                }
                
	        	else if (line.Contains("#BPMS:")) 
                {
                    //song.bpms = ExtractData(line);
                    //if (song.diplayBPM == "") song.displayBPM = 
                }
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
        transform.gameObject.SetActive(false);
    }

    public string ExtractData(string line)
	{
		int dataStart = line.IndexOf(':')+1;
		int dataEnd = line.LastIndexOf(';');
		return line.Substring(dataStart, dataEnd - dataStart);
	}

    public void DisplayGroup()
    {
        for (int i=0; i < groupChildren.Count; i++)
        {
            groupChildren[i].SetActive(collapsed);
        }
        collapsed = !collapsed;
        GetImage(groupBanner);
    }

    public void DisplayInfo()
    {
        GetImage(song.banner);
        manager.DisplayInfo(song);
    }

    public void GetImage(string dir)
    {
        //if (dir == "" || dir == null) dir = song.banner;
        var bytes = System.IO.File.ReadAllBytes(dir);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(bytes);
        manager.banner.texture = tex;
    }
}
