using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Manager : MonoBehaviour
{
    public MusicData music;
    public AudioSource source;
    public Text hitCount; // Displays number of hit notes
    public Text missCount; // Displays number of missed notes
    public Text score; // Displays score
    public Text comboText; // Displays combo/streak
    public Text rankText; // Displays hit ranking
    public Slider health;
    public Slider timeBar;

    public int perfectPoints = 10050;
    public int greatPoints = 7540;
    public int goodPoints = 5030;
    public int okayPoints = 2520;
    public int poorPoints = 1710;

    public float healthVal = 50;
    public int totalHits = 0;
    public int totalMisses = 0;
    public int totalScore;
    public int combo = 0;
    public bool ready;

    void Awake()
    {
        Play(music.currentSong);
    }

    void Update ()
    {
        health.value = healthVal;
        timeBar.value = source.time;
        hitCount.text = totalHits.ToString();
        missCount.text = totalMisses.ToString();
        score.text = totalScore.ToString();
        comboText.text = combo.ToString();
    }

    void Play(SongData song)
    {
        source.clip = null;
        source.Stop();
        string dir = song.directory.Replace(@"C:\Users\Collin\Documents\Repos\Rhythmic\Rhythmic\Assets\Resources\", "")+"\\"+song.music;
        AudioClip clip = (AudioClip)Resources.Load(dir, typeof(AudioClip));
        source.clip = clip;
        timeBar.maxValue = source.clip.length;
        ready = true;
    }

    public void Note(GameObject note, string rating)
    {
        switch (rating)
        {
        case "MISS":
            Miss(note, 0, new Color(0.6f, 0.2f, 0.2f, 1f), rating);
            healthVal -= 10f;
            Destroy(note, 1f);
            break;
        case "POOR":
            Miss(note, poorPoints, new Color(0.6f, 0.4f, 0.2f, 1f), rating);
            healthVal += 1f;
			Destroy(note);
            break;
        case "OKAY":
            Miss(note, okayPoints, new Color(0.5f, 0.2f, 0.6f, 1f), rating);
            healthVal += 1f;
			Destroy(note);
            break;
        case "GOOD":
            Hit(note, goodPoints, new Color(0.3f, 0.6f, 0.2f, 1f), rating);
            break;
        case "GREAT!":
            Hit(note, greatPoints, new Color(0.6f, 0.6f, 0.2f, 1f), rating);
            break;
        case "PERFECT!":
            Hit(note, perfectPoints, new Color(0.2f, 0.5f, 0.6f, 1f), rating);
            break;
        default:
            print ("INCORRECT RATING");
            break;
        }
    }

    void Hit(GameObject note, int points, Color col, string rating)
	{
		totalScore += points;
		rankText.color = col;
		rankText.text = rating;
		totalHits++;
		combo++;
		healthVal += 1f;
		Destroy(note);
	}

	void Miss(GameObject note, int points, Color col, string rating)
	{
		totalScore += points;
		totalMisses++;
		rankText.color = col;
		rankText.text = rating;
		comboText.color = new Color(1f, 1f, 1f, 1f);
        combo = 0;
	}
}