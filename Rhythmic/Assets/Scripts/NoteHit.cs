using UnityEngine;

public class NoteHit : MonoBehaviour
{
	public Manager manager; // Manager script
	public string button; // What key to press
    public bool autoPlay; // Note plays itself
    public AudioSource click; // Click sound
    private Animator flash; // Animation that plays when you press key
	//public GameObject death; // Death screen

	private GameObject note; // Current note in collider
    private float noteDistance; // Distance of note from collider center

    private void Start()
    {
	    flash = gameObject.GetComponent<Animator>();
    }

    void Update ()
    {
		if (autoPlay)
	    {
		    if (note != null && note.tag == "NOTE")
		    {
			    if (noteDistance <= 0.0001)
			    {
					click.Play();
					manager.Note(note, "PERFECT!");
			    }
		    }
	    }
	    else if (Input.GetKeyDown(button) && !autoPlay)
	    {
		    flash.SetTrigger("ButtonPress");
		    if (note != null && note.tag == "NOTE")
		    {
				if (noteDistance > 1.5) manager.Note(note, "POOR");
			    else if (noteDistance > 1.2) manager.Note(note, "OKAY");
				else if (noteDistance > 1) manager.Note(note, "GOOD");
			    else if (noteDistance > 0.75) manager.Note(note, "GREAT!");
			    else manager.Note(note, "PERFECT!"); //if (noteDistance <= 0.75)
		    }
	    }
	    if (manager.healthVal <= 0)
        {
            //death.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "NOTE")
        {
            note = other.gameObject;
			noteDistance = Vector2.Distance(other.transform.position, transform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
	    if (other.gameObject.tag == "NOTE")
	    {
			note = other.gameObject;
		    noteDistance = Vector2.Distance(other.transform.position, transform.position);
	    }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
		manager.Note(note, "MISS");
		note = null;
    }	
}