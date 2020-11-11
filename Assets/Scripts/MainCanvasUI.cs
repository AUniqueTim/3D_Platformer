using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCanvasUI : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Quit.");
    }
    public void Restart()
    {
        SceneManager.LoadScene("TestScene_01");
        Debug.Log("Loaded Test Scene 1");
    }
}
