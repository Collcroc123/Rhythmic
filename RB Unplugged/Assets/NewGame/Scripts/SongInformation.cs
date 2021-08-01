using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System.Linq;

public class SongInformation : MonoBehaviour
{
    private int currentMeasure, lineCount; //Current Measure, Num Lines Between Commas
    private bool setInfo;
    public string title;
    public Text titleText;
    public string artist;
    public string songName;
    public ArrayData songDatas;
    public int songNumber;
    private string bpmString, offsetString;
    
    void Awake()
    {
        StartCoroutine(WaitSong());
    }

    void NoteScanner()
    {
        string[] lines = System.IO.File.ReadAllLines(songDatas.songInfo[songNumber].textAddress);
        foreach (string line in lines)
        {
            if (!setInfo)
            {
                if (line.Contains("#TITLE:"))
                {
                    songDatas.songInfo[songNumber].title = line.Replace("#TITLE:", "");
                    songDatas.songInfo[songNumber].title = songDatas.songInfo[songNumber].title.Replace(";", "");
                    titleText.text = songDatas.songInfo[songNumber].title;
                }
                else if (line.Contains("#ARTIST:")) 
                {
                    songDatas.songInfo[songNumber].artist = line.Replace("#ARTIST:", "");
                    songDatas.songInfo[songNumber].artist = songDatas.songInfo[songNumber].artist.Replace(";", "");
                }
                else if (line.Contains("#BPMS:"))
                {
                    if (line.Contains(","))
                    {
                        bpmString = line.Substring(0, line.IndexOf(","));
                        bpmString = bpmString.Substring(bpmString.IndexOf("="));
                        bpmString = bpmString.Replace("=", "");
                        songDatas.songInfo[songNumber].bpm = float.Parse(bpmString, CultureInfo.InvariantCulture);
                    }
                    else if (line.Contains(",") == false)
                    {
                        bpmString = line.Replace(";", "");
                        bpmString = bpmString.Substring(bpmString.IndexOf("="));
                        bpmString = bpmString.Replace("=", "");
                        songDatas.songInfo[songNumber].bpm = float.Parse(bpmString, CultureInfo.InvariantCulture);
                    }
                }
                else if (line.Contains("#OFFSET:"))
                {
                    offsetString = line.Replace("#OFFSET:", "");
                    offsetString = offsetString.Replace(";", "");
                    songDatas.songInfo[songNumber].offset = float.Parse(offsetString, CultureInfo.InvariantCulture);
                }
                else if (line.Contains("#MUSIC:"))
                {
                    songName = line.Replace("#MUSIC:", "");
                    songName = songName.Replace(";", "");
                    songDatas.songInfo[songNumber].songAddress = songDatas.songInfo[songNumber].baseDirectory + "\\" + songName;
                }
                else if (line.Contains("#BACKGROUND:"))
                {
                    songDatas.songInfo[songNumber].backgroundAddress = line.Replace("#BACKGROUND:", "");
                    songDatas.songInfo[songNumber].backgroundAddress = songDatas.songInfo[songNumber].backgroundAddress.Replace(";", "");
                }
                else if (line.Contains("#NOTES:") && !setInfo)
                {
                    setInfo = true;
                }
            }
            else if (setInfo)
            {
                if (line.Length == 4)
                {
                    lineCount++;
                }
                else if (line == ",")
                {
                    songDatas.songInfo[songNumber].expertMap[currentMeasure] = lineCount; //HERE
                    currentMeasure++;
                    lineCount = 0;
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
        yield return new WaitForSeconds(0.25f);
        NoteScanner();
    }
}
