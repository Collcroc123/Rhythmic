using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteHit : MonoBehaviour
{
	public Manager manager;
	private IEnumerator coroutine;
    private IEnumerator noteKill;
    //public string key;
    public string button;
    public Material transparent;
    public Material white;
    public MeshRenderer mesh;
    public Slider health;
    public GameObject death;
    private GameObject currentNote;
    private GameObject missedNote;

	void Start ()
    {

    }
	
	void Update ()
    {
        if (currentNote != null)
        {
			if (currentNote.tag == "NOTE")
			{
				if (Input.GetButtonDown(button))
				{
					Destroy(currentNote);
					coroutine = waitNote();
					StartCoroutine(coroutine);
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
                coroutine = waitNote();
                StartCoroutine(coroutine);
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

	private IEnumerator waitNote()
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
            //manager.combo = 0;
            health.value -= 5.0f;
        }
        Destroy(other.gameObject, 0.2f);
    }
}
