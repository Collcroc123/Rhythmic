using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Visualizer : MonoBehaviour
{
    //https://www.youtube.com/watch?v=PgXZsoslGsg
    public float minHeight = 15f;
    public float maxHeight = 550f;
    public float updateSenstivity = 0.5f;
    public Color visualizerColor = Color.gray;
    [Space (15)]
    private string artistTitle;
    private string songTitle;
    public Text artistText;
    public Text songText;
    public AudioClip audioClip;
    public bool loop = true;
    [Space (15), Range(64, 8192)]
    public int visualizerSamples = 64;
    [Range(0.0f, 1.0f)]
    public float songVolume = 1.0f;

    VisualizerObjectScript[] visualizerObjects;
    AudioSource m_audioSource;

    void Start()
    {
        visualizerObjects = GetComponentsInChildren<VisualizerObjectScript>();
        if(!audioClip){return;}
        m_audioSource = new GameObject ("AudioSource").AddComponent<AudioSource>();
        m_audioSource.loop = loop;
        m_audioSource.clip = audioClip;
        m_audioSource.Play();
    }

    void Update()
    {
        float[] spectrumData = m_audioSource.GetSpectrumData(visualizerSamples, 0, FFTWindow.BlackmanHarris);
        for (int i = 0; i < visualizerObjects.Length; i++)
        {
            Vector2 newSize = visualizerObjects[i].GetComponent<RectTransform>().rect.size;
            newSize.y = Mathf.Clamp (Mathf.Lerp (newSize.y, minHeight + (spectrumData[i] * (maxHeight - minHeight) * 5f), updateSenstivity), minHeight, maxHeight);
            visualizerObjects[i].GetComponent<RectTransform>().sizeDelta = newSize;
            visualizerObjects[i].GetComponent<Image>().color = visualizerColor;
        }
        songText.text = audioClip.name;
        m_audioSource.volume = songVolume;
    }
}