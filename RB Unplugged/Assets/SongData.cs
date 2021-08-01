using TagLib.NonContainer;
using UnityEngine;

[CreateAssetMenu]
public class SongData : ScriptableObject
{
    //public int songNum;
    public string baseDirectory;
    public string textAddress;
    public string songAddress;
    public string backgroundAddress;
    public string coverAddress;
    public string title;
    public string artist;
    public float bpm;
    public float offset;
    public int[] expertMap = new int[999];

    public void Clear()
    {
        for (int i = 0; i < expertMap.Length; i++)
        {
            expertMap[i] = 0;
        }
    }
}
