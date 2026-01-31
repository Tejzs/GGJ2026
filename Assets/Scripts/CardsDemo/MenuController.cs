using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button startButton;
    public Button restartButton;
    public Button quitButton;
    
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        if (startButton != null)
            startButton.onClick.AddListener(() => gameManager.StartLevel1());
        
        if (restartButton != null)
            restartButton.onClick.AddListener(() => gameManager.RestartGame());
        
        if (quitButton != null)
            quitButton.onClick.AddListener(() => Application.Quit());
    }
}
