using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Text hitCount; //Displays number of hit notes, UI
    public Text missCount; //Displays number of missed notes, UI
    public Text score; //Displays score, UI
    public Text multX; //Displays multiplier, UI
    public Text combText; //Displays combo/streak, UI
    public Slider multiplier; //Displays multiplier bar, UI
    public int multInt = 1; //1-5 or 2-12, int
    public int totalHits; //Number of hit notes, int
    public int totalMisses; //Number of missed notes, int
    public int totalScore; //Total score, int
    public int combo = 0; //Number of notes without fail, int
    public int sliderVal = 0; //Multiplier Bar's INT, int
    public int points = 500; //How mamy points each note is worth, int

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
        /*
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
        */
    }
}