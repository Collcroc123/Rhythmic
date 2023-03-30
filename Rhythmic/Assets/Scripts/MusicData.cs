using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class MusicData : ScriptableObject
{
    public SongData currentSong; //"C:/Users/Collin/Documents/Project OutFox/Songs/";
    public string directory = @"C:\Users\Collin\Documents\Repos\Rhythmic\Rhythmic\Assets\Resources\Music\";
    public List<List<SongData>> groupList = new List<List<SongData>>();
    public bool scanned;
    
    public void Clear()
    {
        for (int i = 0; i < groupList.Count; i++)
        {
            groupList[i] = null;
        }
    }
}
