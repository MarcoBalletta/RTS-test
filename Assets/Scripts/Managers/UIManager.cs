using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject panelEndGame;
    public TextMeshProUGUI endGameText;
    public TextMeshProUGUI descriptionTextPrefab;
    public Transform descriptionTextSocket;
    public GameObject inventoryPanel;
    public GiftIcon giftIconPrefab;

    private void OnEnable()
    {
        GameManager.instance.onUpdateTimer += UpdateTimer;
        GameManager.instance.onStartGame += 
        GameManager.instance.onEndGame += EndGame;
        GameManager.instance.onBefanaCaughtSanta += BefanaCaughtSanta;
        GameManager.instance.onGiftPicked += GiftPicked;
        GameManager.instance.onGiftDelivered += GiftDelivered;
        GameManager.instance.onSantaSelectedInfos += ShowSantaInfos;
    }

    private void UpdateTimer(float value)
    {
        timerText.text = (int)value / 60 + " : " + (int)value % 60;
    }

    private void EndGame(bool victory)
    {
        string text;
        if (victory)
        {
            text = "You won!";
        }
        else
        {
            text = "You lost!";
        }

        endGameText.text = text;
        panelEndGame.SetActive(true);
    }

    private void BefanaCaughtSanta(Entity santa, EnemyController befana)
    {
        ShowInfoText(Constants.UI_BEFANA_CAUGHT_SANTA_TEXT);
    }

    private void StartGame(Entity santa, EnemyController befana)
    {
        ShowInfoText(Constants.UI_START_GAME_TEXT);
    }

    private void GiftPicked()
    {
        ShowInfoText(Constants.UI_GIFT_PICKED_TEXT);
    }

    private void GiftDelivered()
    {
        ShowInfoText(Constants.UI_GIFT_DELIVERED_TEXT);
    }

    private void ShowInfoText(string text)
    {
        var textObject = Instantiate(descriptionTextPrefab, descriptionTextSocket);
        textObject.text = text;
    }

    private void ShowSantaInfos(SantaController santa)
    {
        if (inventoryPanel.activeInHierarchy) return;
        foreach (var item in santa.GetInventoryItems())
        {
            var iconGift = Instantiate(giftIconPrefab, inventoryPanel.transform);
            iconGift.Setup(item.DestinationBuilding);
        }
        StartCoroutine(ShowInventoryPanel());
    }

    private IEnumerator ShowInventoryPanel()
    {
        inventoryPanel.SetActive(true);
        yield return new WaitForSeconds(GameManager.instance.InventoryPanelShowTime);
        inventoryPanel.SetActive(false);
        DeleteChildrenFromInventoryPanel();
    }

    private void DeleteChildrenFromInventoryPanel()
    {
        foreach (var iconGift in inventoryPanel.transform.GetComponentsInChildren<Transform>())
        {
            if (iconGift != inventoryPanel.transform)   Destroy(iconGift.gameObject, 0.1f);
        }
    }
}
