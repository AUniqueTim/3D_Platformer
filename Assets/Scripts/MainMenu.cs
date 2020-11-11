using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject controlsPanel;
    public void Quit()
    {
        Application.Quit();
        Debug.Log("User quit.");
    }
    public void NewGame()
    {
        SceneManager.LoadScene("TestScene_01");
        Debug.Log("Loaded Test Scene");
    }
    public void SettingsPanel()
    {
        mainMenuPanel.gameObject.SetActive(false);
        controlsPanel.gameObject.SetActive(false);
        settingsPanel.gameObject.SetActive(true);
        Debug.Log("Settings Panel activated.");
    }
    public void ControlsPanel()
    {
        mainMenuPanel.gameObject.SetActive(false);
        controlsPanel.gameObject.SetActive(true);
        settingsPanel.gameObject.SetActive(false);
        Debug.Log("Controls Panel activated.");
    }
    public void MainMenuPanel()
    {
        mainMenuPanel.gameObject.SetActive(true);
        controlsPanel.gameObject.SetActive(false);
        settingsPanel.gameObject.SetActive(false);
        Debug.Log("Main Menu activated.");
    }
}
