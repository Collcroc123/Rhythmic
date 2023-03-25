using UnityEngine;

public class NoteHit : MonoBehaviour
{
	public Manager manager; //Manager script
	public string button; //What key to press
	//public GameObject death; //Death screen
    private GameObject currentNote; //Currently selected note
    private float noteDistance;
    private Animator flash;
    public bool autoPlay;
    public AudioSource click;

    private void Start()
    {
	    flash = gameObject.GetComponent<Animator>();
    }

    void Update ()
    {
	    if (autoPlay)
	    {
		    if (currentNote != null && currentNote.tag == "NOTE")
		    {
			    if (noteDistance <= 0.00001)
			    {
				    manager.totalScore += manager.perfectPoints;
				    manager.rankText.text = "PERFECT!";
				    click.Play();
				    Destroy(currentNote);
				    manager.totalHits++;
				    manager.combo++; 
				    manager.healthVal += 1f;
			    }
		    }
	    }
	    else if (Input.GetKeyDown(button) && !autoPlay)
	    {
		    flash.SetTrigger("ButtonPress");
		    if (currentNote != null && currentNote.tag == "NOTE")
		    {
			    if (noteDistance <= 0.75)
			    { 
				    manager.totalScore += manager.perfectPoints;
				    manager.rankText.text = "PERFECT!";
			    }
			    else if (noteDistance > 0.75 && noteDistance <= 1)
			    {
				    manager.totalScore += manager.greatPoints;
				    manager.rankText.text = "GREAT!";
			    }
			    else if (noteDistance > 1 && noteDistance <= 1.2)
			    {
				    manager.totalScore += manager.goodPoints;
				    manager.rankText.text = "GOOD";
			    }
			    else if (noteDistance > 1.2 && noteDistance <= 1.5)
			    {
				    manager.totalScore += manager.okayPoints;
				    manager.rankText.text = "OKAY";
				    manager.combo = 0;
			    }
			    else if (noteDistance > 1.5)
			    {
				    manager.totalScore += manager.poorPoints;
				    manager.rankText.text = "POOR";
				    manager.combo = 0;
			    }
			    Destroy(currentNote);
			    manager.totalHits++;
			    manager.combo++; 
			    manager.healthVal += 1f;
		    }
	    }
	    if (manager.healthVal <= 0)
        {
            //death.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "NOTE")
        {
            currentNote = other.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
	    if (other.gameObject.tag == "NOTE")
	    {
		    noteDistance = Vector2.Distance(other.transform.position, transform.position);
	    }
    }

    void OnTriggerExit2D(Collider2D other)
    {
	    currentNote = null;
    }
}