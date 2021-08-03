using UnityEngine;

[CreateAssetMenu]
public class ArrayData : ScriptableObject
{
    public SongData[] songInfo;
    
    public void Clear()
    {
        for (int i = 0; i < songInfo.Length; i++)
        {
            songInfo[i] = null;
        }
    }
}
