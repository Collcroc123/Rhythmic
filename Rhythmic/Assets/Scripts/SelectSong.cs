using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SelectSong : MonoBehaviour
{
    private GameObject button;
    public Animator fade;
    public IntData songNumber;
    void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            StartCoroutine(LoadGame());
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        button = other.gameObject.transform.gameObject;
    }

    IEnumerator LoadGame()
    {
        //songNumber.value = button.GetComponent<SongInformation>().songNumber;
        fade.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level");
    }
}
