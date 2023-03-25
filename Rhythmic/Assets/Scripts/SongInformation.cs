using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SongInformation : MonoBehaviour
{
    public SongData song;
    private RawImage banner;
    public List<GameObject> groupChildren = new List<GameObject>();
    public string groupBanner;
    private bool collapsed = true;

    void Awake()
    {
        banner = GameObject.Find("Banner").GetComponent<RawImage>();
    }
    
    public void GetImage(string dir)
    {
        if (dir == "") dir = song.directory+song.banner;
        var bytes = System.IO.File.ReadAllBytes(dir);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(bytes);
        banner.texture = tex;
    }

    public void Group()
    {
        for (int i=0; i < groupChildren.Count; i++)
        {
            groupChildren[i].SetActive(collapsed);
        }
        //transform.GetChild(1).gameObject.SetActive(collapsed);
        collapsed = !collapsed;
        GetImage(groupBanner);
    }
}
