using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Text hitCount; //Displays number of hit notes, UI
    public Text missCount; //Displays number of missed notes, UI
    public Text score; //Displays score, UI
    public Text combText; //Displays combo/streak, UI
    public Text rankText; //Displays hit ranking, UI
    public Slider health; //Displays multiplier bar, UI
    public float healthVal = 50;
    public int totalHits = 0; //Number of hit notes, int
    public int totalMisses = 0; //Number of missed notes, int
    public int totalScore; //Total score, int
    public int combo = 0; //Number of notes without fail, int
    public int perfectPoints = 10050;
    public int greatPoints = 7540;
    public int goodPoints = 5030;
    public int okayPoints = 2520;
    public int poorPoints = 1710;

    void Update ()
    {
        health.value = healthVal;
        hitCount.text = totalHits.ToString();
        missCount.text = totalMisses.ToString();
        score.text = totalScore.ToString();
        combText.text = combo.ToString();
    }
}