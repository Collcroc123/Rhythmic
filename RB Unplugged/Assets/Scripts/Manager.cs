using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Text hitCount; //How many notes you hit, UI
    public Text missCount; //How many notes you missed, UI
    public Text score; //Score, UI
    public Text multX; //Multiplier, UI
    public Text combText; //Combo, UI
    public int multInt = 1; //1-5 or 2-12, int
    public int totalHits; //Number of hit notes, int
    public int totalMisses; //Number of missed notes, int
    public int totalScore; //Total score, int
    public Slider multiplier; //Multiplier Bar, UI
    public int combo = 0; //How many notes without fail, int
    public int sliderVal = 0; //Multiplier Bar's INT, int
    public int points = 500; //How much each note is worth, int

    void Start ()
    {
        sliderVal = combo;
        multiplier.maxValue = 10;
    }
	
	void Update ()
    {
        multX.text = multInt.ToString();
        multiplier.value = sliderVal;
        hitCount.text = totalHits.ToString();
        missCount.text = totalMisses.ToString();
        score.text = totalScore.ToString();
        combText.text = combo.ToString();

        if (combo >= 40)
        {
            sliderVal = combo - 40;
            points = 2500;
            multInt = 5;
        }
        else if (combo >= 30 && combo < 40)
        {
            sliderVal = combo - 30;
            points = 2000;
            multInt = 4;
        }
        else if (combo >= 20 && combo < 30)
        {
            sliderVal = combo - 20;
            points = 1500;
            multInt = 3;
        }
        else if (combo >= 10 && combo < 20)
        {
            sliderVal = combo - 10;
            points = 1000;
            multInt = 2;
        }
        else if (combo < 10)
        {
            sliderVal = combo;
            points = 500;
            multInt = 1;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FRET")
        {
            Destroy(other.gameObject, 0.2f);
        }
        if (other.gameObject.tag == "DOUBLE")
        {
            Destroy(other.gameObject, 0.2f);
        }
    }
}
