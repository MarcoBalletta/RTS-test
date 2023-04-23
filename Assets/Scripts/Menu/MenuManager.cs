using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject startPanel;

    public void ClickedStartGame()
    {
        levelPanel.gameObject.SetActive(true);
        startPanel.gameObject.SetActive(false);
    }

    public void BackToStartPanel()
    {
        startPanel.gameObject.SetActive(true);
        levelPanel.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
