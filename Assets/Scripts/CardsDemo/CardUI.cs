using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CardUI : MonoBehaviour
{
    public Card cardData;
    public Image backgroundImage;
    public Image selectedHighlight;
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI suitText;
    public Button cardButton;

    private GameManager gameManager;

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
        transform.position = startPos;
        transform.DOMove(transform.position, duration).SetEase(Ease.OutQuad);
    }
}
