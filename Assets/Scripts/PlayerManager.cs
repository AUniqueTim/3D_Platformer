using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int nbPickUps;
    public int nbPellets;
    public int nbPowerUps;

    public bool playerPoweredUp;
    public bool wacManPoweredUp;
    public float playerPowerUpTime;

    public int playerLives;



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
        nbPickUps = nbPellets + nbPowerUps;

        wacManPoweredUp = Toolbox.Instance.m_pickUps.wacManPoweredUp;
        playerPoweredUp = Toolbox.Instance.m_pickUps.playerPoweredUp;
        //nbPellets = Toolbox.Instance.m_pickUps.pelletCount;
        //nbPickUps = Toolbox.Instance.m_pickUps.pickUps;
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

}
