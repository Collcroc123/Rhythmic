using UnityEngine;

public class DeleteNote : MonoBehaviour
{
    public NewManager manager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "NOTE")
        {
            manager.totalMisses++;
            manager.combo = 0;
            manager.healthVal -= 5f;
            Destroy(other.gameObject, 1f);
        }
    }
}