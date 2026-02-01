using UnityEngine;

public class OpenLink : MonoBehaviour
{
    [SerializeField] private string url = "https://scratch.mit.edu/projects/1273752334";

    public void OpenURL()
    {
        Application.OpenURL(url);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Link")) {
        
            Debug.Log("ww");
        }
    }
}

