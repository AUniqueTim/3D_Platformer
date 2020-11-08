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

    public int playerLives;
    public TextMeshProUGUI oneUps;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject wacMan;

    



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
        

        if (playerLives > 3) { playerLives = 3; }
        if (playerLives <= 0)
        {
            playerLost = true;
            GameOver();
        }
        if (Toolbox.Instance.m_EnemyManager.wacManLives <= 0)
        {
            playerWon = true;
            GameOver();
            //Next Level.
        }
        //if (Toolbox.Instance.m_pickUps.pickedUpPellet)
        //{
        //    nbPellets += 1;
        //}
        //if (Toolbox.Instance.m_pickUps.pickedUpPowerUp)
        //{
        //    nbPowerUps += 1;
        //}
        nbPickUps = nbPellets + nbPowerUps;
        //nbPickUps = Toolbox.Instance.m_pickUps.pelletCount + Toolbox.Instance.m_pickUps.powerUps;

        //wacManPoweredUp = Toolbox.Instance.m_pickUps.wacManPoweredUp;
        //playerPoweredUp = Toolbox.Instance.m_pickUps.playerPoweredUp;
        //nbPellets = Toolbox.Instance.pelletCount;
        //nbPickUps = Toolbox.Instance.pickUpCount;
        if (playerPoweredUp == false)
        {
            return;

        }
        else if (playerPoweredUp == true)
        {
            Debug.Log("Player Powered Up.");
        }
        
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pellet") { nbPellets += 1; }
        
        if (collision.gameObject.tag == "PowerUp") { nbPowerUps += 1; }
    }
    public void GameOver()
    {
        
        if (playerLost)
        {
            Toolbox.Instance.m_CameraController.CameraState = 3;
            losePanel.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (playerWon)
        {
            wacMan.SetActive(false);
            winPanel.SetActive(true);
        }
        
    }
}
