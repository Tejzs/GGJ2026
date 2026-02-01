using UnityEngine;

public class PickUpMaskOnTouch : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SpadeMask;
    
    [SerializeField] private Sprite SpadeMaskArt;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpadeMask = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpadeMask"))
        {
            SpadeMask.sprite = SpadeMaskArt;
        }
    }
}
