using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum GameScene
{
    MENU,
    LEVEL1,
    LEVEL2,
    WIN,
    LOST
}

public enum GameTurn
{
    PLAYER_TURN,
    BOSS_TURN,
    GAME_OVER
}

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject menuPanel;
    public GameObject gamePanel;
    public GameObject winPanel;
    public GameObject lostPanel;

    [Header("Game UI")]
    public TextMeshProUGUI bossNameText;
    public Slider bossHealthSlider;
    public TextMeshProUGUI bossHealthText;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI drawPileText;
    public TextMeshProUGUI discardPileText;
    public TextMeshProUGUI turnText;
    public TextMeshProUGUI comboMessageText;
    public TextMeshProUGUI damagePreviewText;
    public Button attackButton;

    [Header("Card UI")]
    public GameObject cardPrefab;
    public Transform handContainer;
    public Transform drawPilePosition;

    [Header("Game Settings")]
    public int boss1MaxHP = 100;
    public int boss2MaxHP = 200;
    public int playerStartHP = 100;
    public int level2PlayerHP = 1000;

    private DeckManager deckManager;
    private CardUI[] cardUIs = new CardUI[5];
    
    private GameScene currentScene = GameScene.MENU;
    private GameTurn currentTurn = GameTurn.PLAYER_TURN;
    
    private int playerHP;
    private int bossHP;
    private int currentBossMaxHP;
    private int currentLevel = 1;
    
    private string lastComboMessage = "";
    private float comboMessageTimer = 0f;
    private float bossAttackTimer = 0f;

    void Start()
    {
        deckManager = gameObject.AddComponent<DeckManager>();
        
        // Create card UI objects
        for (int i = 0; i < 5; i++)
        {
            GameObject cardObj = Instantiate(cardPrefab, handContainer);
            cardUIs[i] = cardObj.GetComponent<CardUI>();
        }

        attackButton.onClick.AddListener(OnAttackButtonClicked);
        
        ShowMenu();
    }

    void Update()
    {
        if (currentScene == GameScene.LEVEL1 || currentScene == GameScene.LEVEL2)
        {
            // Update combo message timer
            if (comboMessageTimer > 0f)
            {
                comboMessageTimer -= Time.deltaTime;
                if (comboMessageTimer <= 0f)
                {
                    comboMessageText.text = "";
                }
            }

            // Boss turn logic
            if (currentTurn == GameTurn.BOSS_TURN)
            {
                bossAttackTimer += Time.deltaTime;
                if (bossAttackTimer >= 1.0f)
                {
                    BossAttack();
                    bossAttackTimer = 0f;
                    
                    if (playerHP <= 0)
                    {
                        currentTurn = GameTurn.GAME_OVER;
                        ShowLost();
                    }
                    else if (bossHP <= 0)
                    {
                        currentTurn = GameTurn.GAME_OVER;
                        if (currentLevel == 1)
                        {
                            StartLevel2();
                        }
                        else
                        {
                            ShowWin();
                        }
                    }
                    else
                    {
                        currentTurn = GameTurn.PLAYER_TURN;
                        UpdateTurnDisplay();
                    }
                }
            }
        }
    }

    public void ShowMenu()
    {
        currentScene = GameScene.MENU;
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        winPanel.SetActive(false);
        lostPanel.SetActive(false);
    }

    public void StartLevel1()
    {
        Debug.Log("Starting Level 1");
        
        // Cancel any pending invokes
        
        currentScene = GameScene.LEVEL1;
        currentLevel = 1;
        playerHP = playerStartHP;
        bossHP = boss1MaxHP;
        currentBossMaxHP = boss1MaxHP;
        currentTurn = GameTurn.PLAYER_TURN;
        bossAttackTimer = 0f;
        comboMessageTimer = 0f;

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        winPanel.SetActive(false);
        lostPanel.SetActive(false);

        bossNameText.text = "BOSS 1";
        bossHealthSlider.maxValue = currentBossMaxHP;
        
        deckManager.InitDeck();
        deckManager.ShuffleDeck();
        deckManager.DealHand();
        
        UpdateHandUI();
        UpdateUI();
    }

    public void StartLevel2()
    {
        Debug.Log("Starting Level 2");
        
        // Cancel any pending invokes
        CancelInvoke();
        
        currentScene = GameScene.LEVEL2;
        currentLevel = 2;
        playerHP = level2PlayerHP;
        bossHP = boss2MaxHP;
        currentBossMaxHP = boss2MaxHP;
        currentTurn = GameTurn.PLAYER_TURN;
        bossAttackTimer = 0f;

        gamePanel.SetActive(true);
        winPanel.SetActive(false);
        lostPanel.SetActive(false);

        bossNameText.text = "BOSS 2";
        bossHealthSlider.maxValue = currentBossMaxHP;
        
        deckManager.InitDeck();
        deckManager.ShuffleDeck();
        deckManager.DealHand();
        
        UpdateHandUI();
        UpdateUI();
    }

    void UpdateHandUI()
    {
        for (int i = 0; i < 5; i++)
        {
            cardUIs[i].Initialize(deckManager.hand[i], this);
        }
    }

    void UpdateUI()
    {
        bossHealthSlider.value = bossHP;
        bossHealthText.text = $"{bossHP} HP";
        playerHealthText.text = $"PLAYER HP: {playerHP}";
        drawPileText.text = $"Draw: {deckManager.drawPile.Count}";
        discardPileText.text = $"Discard: {deckManager.discardPile.Count}";
        UpdateTurnDisplay();
    }

    void UpdateTurnDisplay()
    {
        if (currentTurn == GameTurn.PLAYER_TURN)
        {
            turnText.text = "YOUR TURN";
            turnText.color = Color.white;
            attackButton.interactable = true;
        }
        else if (currentTurn == GameTurn.BOSS_TURN)
        {
            turnText.text = "BOSS TURN";
            turnText.color = Color.red;
            attackButton.interactable = false;
        }
    }

    public void UpdateDamagePreview()
    {
        if (currentTurn == GameTurn.PLAYER_TURN)
        {
            ComboResult preview = ComboChecker.CheckCombos(deckManager.hand);
            int baseDmg = deckManager.CalculateDamage();
            int totalDmg = (int)(baseDmg * preview.multiplier);

            if (baseDmg > 0)
            {
                damagePreviewText.text = $"Damage: {totalDmg}";
                damagePreviewText.color = preview.hasCombo ? Color.yellow : Color.white;
            }
            else
            {
                damagePreviewText.text = "";
            }
        }
    }

    void OnAttackButtonClicked()
    {
        if (currentTurn != GameTurn.PLAYER_TURN)
        {
            Debug.Log("Not player turn, can't attack");
            return;
        }

        ComboResult combo = ComboChecker.CheckCombos(deckManager.hand);
        int baseDamage = deckManager.CalculateDamage();
        
        if (baseDamage <= 0)
        {
            Debug.Log("No cards selected");
            return;
        }
        
        int finalDamage = (int)(baseDamage * combo.multiplier);

        bossHP -= finalDamage;
        if (bossHP < 0) bossHP = 0;

        Debug.Log($"Dealt {finalDamage} damage to boss. Boss HP: {bossHP}");

        // Show combo message
        if (combo.hasCombo)
        {
            lastComboMessage = $"{combo.message} ({baseDamage} -> {finalDamage} dmg)";
            comboMessageTimer = 3.0f;
        }
        else
        {
            lastComboMessage = $"{finalDamage} damage";
            comboMessageTimer = 2.0f;
        }
        comboMessageText.text = lastComboMessage;
        comboMessageText.color = combo.hasCombo ? Color.yellow : Color.white;

        deckManager.DiscardSelectedCards();
        UpdateHandUI();
        UpdateUI();
        
        damagePreviewText.text = "";

        // Check if boss is dead before switching turns
        if (bossHP <= 0)
        {
            Debug.Log($"Boss defeated! Current level: {currentLevel}");
            currentTurn = GameTurn.GAME_OVER;
            
            if (currentLevel == 1)
            {
                // Small delay before starting level 2
                Invoke(nameof(StartLevel2), 1.5f);
            }
            else
            {
                Invoke(nameof(ShowWin), 1.5f);
            }
        }
        else
        {
            currentTurn = GameTurn.BOSS_TURN;
            UpdateTurnDisplay();
        }
    }

    void BossAttack()
    {
        int damage = Random.Range(5, 13);
        playerHP -= damage;
        if (playerHP < 0) playerHP = 0;
        UpdateUI();
    }

    void ShowWin()
    {
        Debug.Log("You Win!");
        CancelInvoke();
        currentScene = GameScene.WIN;
        currentTurn = GameTurn.GAME_OVER;
        gamePanel.SetActive(false);
        winPanel.SetActive(true);
        lostPanel.SetActive(false);
    }

    void ShowLost()
    {
        Debug.Log("You Lost!");
        CancelInvoke();
        currentScene = GameScene.LOST;
        currentTurn = GameTurn.GAME_OVER;
        gamePanel.SetActive(false);
        winPanel.SetActive(false);
        lostPanel.SetActive(true);
    }

    public bool IsPlayerTurn()
    {
        return currentTurn == GameTurn.PLAYER_TURN;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}