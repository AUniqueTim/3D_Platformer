using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    [SerializeField] GameObject pelletPrefab;
    [SerializeField] GameObject powerUpPrefab;
    public GameObject powerUpPrefabClone;
    [SerializeField] Transform spawnTransform;
    [SerializeField] private bool spawnPelletAllowed;
    [SerializeField] private float powerUpTime;
    [SerializeField] private float wacManPowerUpTime;

    //public int pelletCount;
    //public int pickUps;
    //public int powerUps;

    public bool wacManPoweredUp;
    public bool playerPoweredUp;

    //public bool pickedUpPowerUp;
    //public bool pickedUpPellet;

    public void Awake()
    {
        
        spawnPelletAllowed = true;
        wacManPowerUpTime = Toolbox.Instance.m_enemy.wacManPowerUpTime;
        wacManPoweredUp = false;

    }
    public void Update()
    {
        
        
        Toolbox.Instance.m_PlayerManager.nbPickUps.ToString("Total Pick Ups" + Toolbox.Instance.m_PlayerManager.nbPickUps);

        //Toolbox.Instance.m_enemy.wacManPoweredUp = wacManPoweredUp;
        //Toolbox.Instance.m_enemy.playerPoweredUp = playerPoweredUp;
        //Toolbox.Instance.m_PlayerManager.nbPellets = pelletCount;
        //Toolbox.Instance.m_PlayerManager.nbPickUps = pickUps;
        //Toolbox.Instance.m_PlayerManager.nbPowerUps = powerUps;
        //Toolbox.Instance.m_PlayerManager.nbPickUps = powerUps;
        //if (wacManPoweredUp)
        //{
        //    Toolbox.Instance.m_enemy.wacManPoweredUp = true;
        //}
        //if (playerPoweredUp)
        //{
        //    Toolbox.Instance.m_enemy.playerPoweredUp = true;
        //}

        //if (Time.time >= powerUpTime + wacManPowerUpTime && wacManPoweredUp == true)
        //{
        //    wacManPoweredUp = false;
        //    Debug.Log("PowerUp Deactivated.");
        //}
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           
            if (spawnPelletAllowed == true && gameObject.tag=="PowerUp")
            {
                powerUpPrefabClone = Instantiate(powerUpPrefab, new Vector3(Random.Range(-25,25), Random.Range(1.75f,10), Random.Range(-25,25)), powerUpPrefab.transform.rotation);
                
                //pickUps += 1;
                Toolbox.Instance.m_PlayerManager.nbPowerUps += 1;
                //pickedUpPowerUp = true;
                Debug.Log("Spawned Pellet:" + collision.gameObject.name);
                gameObject.SetActive(false);

            }
            if (spawnPelletAllowed == true && gameObject.tag == "Pellet")
            {
                Toolbox.Instance.m_PlayerManager.nbPellets += 1;

                //pelletCount += 1;
                //pickedUpPellet = true;
                Toolbox.Instance.m_HighScore.points += 10;
                Debug.Log("Pellet consumed.");
                gameObject.SetActive(false);

            }

        }
        if (gameObject.tag=="PowerUp" && collision.gameObject.tag == "WacMan")
        {
            Toolbox.Instance.m_enemy.wacManPoweredUp = true;
            Toolbox.Instance.m_PlayerManager.wacManPoweredUp = true;
            //wacManPoweredUp = true;
            collision.gameObject.GetComponent<Renderer>().material.SetColor("white", Color.white);
            
            Debug.Log("WacMan PowerUp Activated, Total PickUps:" + Toolbox.Instance.m_PlayerManager.nbPickUps);
            powerUpPrefabClone = Instantiate(powerUpPrefab, new Vector3(Random.Range(-25, 25), Random.Range(1.75f, 5), Random.Range(-25, 25)), powerUpPrefab.transform.rotation);

            //powerUpTime = Time.time;

            gameObject.SetActive(false);


        }
        if (gameObject.tag =="PowerUp" && collision.gameObject.tag == "Player")
        {
            //powerUps += 1;
            Toolbox.Instance.m_enemy.playerPoweredUp = true;
            Toolbox.Instance.m_PlayerManager.playerPoweredUp = true;
            collision.gameObject.GetComponent<Renderer>().material.SetColor("green", Color.green);

            Toolbox.Instance.m_PlayerManager.nbPowerUps += 1;
            Debug.Log("Player PowerUp Activated");

            //powerUpTime = Time.time;
            gameObject.SetActive(false);


        }
        //pickedUpPellet = false;
        //pickedUpPowerUp = false;
    }
}
