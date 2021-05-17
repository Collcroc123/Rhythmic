using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NoteHit : MonoBehaviour
{
	public Manager manager; //Manager script
	public string button; //What key to press
    public Material white; //Pure white material for anim
    public Material transparent; //Transparent color material for anim
    public MeshRenderer mesh; //Mesh renderer for anim
    public Slider health; //Health bar
    public GameObject death; //Death screen
    private GameObject currentNote; //Currently selected note
    private GameObject missedNote;

    void Update ()
    {
        if (currentNote != null)
        {
			if (currentNote.tag == "NOTE")
			{
				if (Input.GetButtonDown(button))
				{
					Destroy(currentNote);
					StartCoroutine(noteAnim());
					//Debug.Log(key + " HIT");
					manager.totalHits++;
					manager.combo++;
					manager.totalScore += manager.points;
					health.value += 2.5f;
				}
			}
			else if (currentNote.tag == "LONGNOTE")
			{
				
			}
        }
        else if (currentNote == null)
        {
            if (Input.GetButtonDown(button))
            {
	            StartCoroutine(noteAnim());
                //Debug.Log(key + " MISSCLICK");
                manager.totalMisses++;
                manager.combo = 0;
                manager.points = 500;
                manager.multInt = 1;
                health.value -= 5.0f;
            }
        }
		if (health.value == 0)
        {
            //death.SetActive(true);
        }
    }

	private IEnumerator noteAnim() //flashes color when note pressed
	{
			mesh.material = white;
			yield return new WaitForSeconds(0.1f);
			mesh.material = transparent;
	}

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "NOTE")
        {
            currentNote = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NOTE")
        {
            currentNote = null;
            missedNote = other.gameObject;
            //Debug.Log(key + " MISS");
            manager.totalMisses++;
            manager.combo = 0;
            health.value -= 5.0f;
        }
        Destroy(other.gameObject, 0.2f);
    }
}