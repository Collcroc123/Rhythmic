using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongScanner : MonoBehaviour
{
    public MusicData music;

    void Awake()
    {
        Scan();
        this.GetComponent<Menu>().CreateList();
    }

    public void Scan()
    {
        if(music.scanned) return;
        music.Clear();
        var groupFolders = System.IO.Directory.GetDirectories(music.directory);
        foreach (string group in groupFolders)
        {
            string groupName = group.Replace(music.directory, "");
            List<SongData> songList = new List<SongData>(); // Stores list of songs in current group
            var songFolders = System.IO.Directory.GetDirectories(group);
            foreach (string dir in songFolders)
            {
                var songNames = System.IO.Directory.GetFiles(dir, "*.sm");
                if (songNames.Length <= 0) songNames = System.IO.Directory.GetFiles(dir, "*.ssc");
                foreach (string file in songNames)
                {
                    var song = ScriptableObject.CreateInstance<SongData>(); // Creates new Song
					song.fileName = file;
                    song.directory = System.IO.Directory.GetParent(file).ToString();
					song.group = groupName;
	                songList.Add(song);
                }
            }
            music.groupList.Add(songList);
        }
        //music.scanned = true;
    }
}
