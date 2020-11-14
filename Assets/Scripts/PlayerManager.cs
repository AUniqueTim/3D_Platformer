using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public int nbPickUps;
    public int nbPellets;
    public int nbPowerUps;

    public bool playerWon;
    public bool playerLost;

    public bool playerPoweredUp;
    public bool wacManPoweredUp;
    public float playerPowerUpTime;

    public bool playPowerUpSound;

    public int playerLives;
    public TextMeshProUGUI oneUps;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject wacMan;

    public bool pickedUpPellet;

    [SerializeField] private GameObject ghostIcon01;
    [SerializeField] private GameObject ghostIcon02;
    [SerializeField] private GameObject ghostIcon03;


    //START SINGLETON

    public static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerManager>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject();
                    singleton.AddComponent<PlayerManager>();
                    singleton.name = "(Singleton) PlayerManager";
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
        //END SINGLETON
        
    }
    public void Update()
    {
        oneUps.text = playerLives.ToString();
        nbPickUps = nbPellets + nbPowerUps;

        if (playerLives > 3) { playerLives = 3; }
        if (playerLives <= 0)
        {
            Toolbox.Instance.m_CameraController.CameraState = 3;
            playerLost = true;
            GameOver();
        }
        if (Toolbox.Instance.m_EnemyManager.wacManLives <= 0)
        {
            playerWon = true;
            GameOver();
            //Next Level.
        }
        

        if (!playerPoweredUp)
        {
            playPowerUpSound = false;
        }
        else if (playerPoweredUp)
        {
            playPowerUpSound = true;
            Debug.Log("Player Powered Up.");
           
        }

        //UI
        if (playerLives >= 3)
        {
            ghostIcon01.gameObject.SetActive(true);
            ghostIcon02.gameObject.SetActive(true);
            ghostIcon03.gameObject.SetActive(true);
        }
        else if (playerLives == 2)
        {
            Toolbox.Instance.audioSource = Toolbox.Instance.playerDeathSound;
            Toolbox.Instance.playerDeathSound.PlayOneShot(Toolbox.Instance.playerDeathSoundClip);
            ghostIcon01.gameObject.SetActive(false);
            ghostIcon02.gameObject.SetActive(true);
            ghostIcon03.gameObject.SetActive(true);
        }
        else if (playerLives == 1)
        {
            ghostIcon01.gameObject.SetActive(false);
            ghostIcon02.gameObject.SetActive(false);
            ghostIcon03.gameObject.SetActive(true);
        }
        else if (playerLives <= 0)
        {
            ghostIcon01.gameObject.SetActive(false);
            ghostIcon02.gameObject.SetActive(false);
            ghostIcon03.gameObject.SetActive(false);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pellet") { nbPellets += 1; pickedUpPellet = true; /*pickedUpPellet = false;*/ }
        if (collision.gameObject.tag == "PowerUp") { nbPowerUps += 1; }
    }
    public void GameOver()
    {
        if (playerLost)
        {
            Toolbox.Instance.m_CameraController.CameraState = 3;
            losePanel.SetActive(true);
            mainCanvas.SetActive(false);
            gameObject.SetActive(false);
        }
        else if (playerWon)
        {
            wacMan.SetActive(false);
            winPanel.SetActive(true);
            //mainCanvas.SetActive(false);
        }
        
    }
}
