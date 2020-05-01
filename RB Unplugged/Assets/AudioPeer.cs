using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    AudioSource audioSource;
    public static float[] samples = new float[64];
    public static float[] freqBand = new float[32];

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        GetSpectrumAudioSource();
        //MakeFrequencyBands();
    }

    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }
    
    void MakeFrequencyBands()
    {
        int count = 0;
        for(int i = 0; i < 32; i++)
        {
            float average = 0f;
            int sampleCount = (int)Mathf.Pow(2,i) * 2;
            if(i == 31)
            {
                sampleCount += 2;
            }
            for(int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count+1);
                    count++;
            }
            average /= count;
            freqBand[i] = average * 10;
        }
    }
}
