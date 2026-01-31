using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

// Alternative CardUI without DOTween dependency
public class CardUI_Simple : MonoBehaviour
{
    public Card cardData;
    public Image backgroundImage;
    public Image selectedHighlight;
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI suitText;
    public Button cardButton;

    private GameManager gameManager;
    private bool isAnimating = false;

    public void Initialize(Card card, GameManager manager)
    {
        cardData = card;
        gameManager = manager;

        if (!card.isEmpty)
        {
            rankText.text = card.rank.ToString();
            suitText.text = card.GetSuitSymbol();
            rankText.color = card.GetSuitColor();
            suitText.color = card.GetSuitColor();
            backgroundImage.color = Color.white;
        }
        else
        {
            rankText.text = "";
            suitText.text = "";
            backgroundImage.color = new Color(1, 1, 1, 0.3f);
        }

        UpdateSelectedVisual();
        
        cardButton.onClick.RemoveAllListeners();
        cardButton.onClick.AddListener(OnCardClicked);
    }

    public void UpdateSelectedVisual()
    {
        if (selectedHighlight != null)
        {
            selectedHighlight.gameObject.SetActive(cardData.selected);
        }
    }

    private void OnCardClicked()
    {
        if (!cardData.isEmpty && gameManager.IsPlayerTurn())
        {
            cardData.selected = !cardData.selected;
            UpdateSelectedVisual();
            gameManager.UpdateDamagePreview();
        }
    }

    public void AnimateFromPosition(Vector3 startPos, float duration = 0.4f)
    {
        if (!isAnimating)
        {
            StartCoroutine(AnimateCoroutine(startPos, transform.position, duration));
        }
    }

    private IEnumerator AnimateCoroutine(Vector3 start, Vector3 end, float duration)
    {
        isAnimating = true;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            
            // Ease out quad
            t = 1 - (1 - t) * (1 - t);
            
            transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        transform.position = end;
        isAnimating = false;
    }
}
