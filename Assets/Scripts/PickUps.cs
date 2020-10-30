using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    [SerializeField] GameObject pelletPrefab;
    [SerializeField] GameObject powerUpPrefab;
    [SerializeField] Transform spawnTransform;
    [SerializeField] private bool spawnPelletAllowed;
    [SerializeField] private float powerUpTime;
    [SerializeField] private float wacManPowerUpTime;
    public int pelletCount;
    public bool wacManPoweredUp;
    public bool playerPoweredUp;
    public int pickUps;
   
    public void Awake()
    {
        
        spawnPelletAllowed = true;
        wacManPowerUpTime = Toolbox.Instance.m_enemy.wacManPowerUpTime;
        wacManPoweredUp = false;

    }
    public void Update()
    {
        pickUps.ToString("Total Pick Ups" + pickUps);

        Toolbox.Instance.m_enemy.wacManPoweredUp = wacManPoweredUp;
        Toolbox.Instance.m_enemy.playerPoweredUp = playerPoweredUp;
        Toolbox.Instance.m_PlayerManager.nbPellets = pelletCount;
        Toolbox.Instance.m_PlayerManager.nbPickUps = pickUps;
        if (wacManPoweredUp)
        {
            Toolbox.Instance.m_enemy.wacManPoweredUp = true;
        }
        if (playerPoweredUp)
        {
            Toolbox.Instance.m_enemy.playerPoweredUp = true;
        }

        if (Time.time >= powerUpTime + wacManPowerUpTime && wacManPoweredUp == true)
        {
            wacManPoweredUp = false;
            Debug.Log("PowerUp Deactivated.");
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           // pickUps += 1;
            //Toolbox.Instance.m_PlayerManager.nbPickUps += 1;
            //Debug.Log("PickUps:" + pickUps);
            if (spawnPelletAllowed == true && gameObject.tag=="PowerUp")
            {
                Instantiate(powerUpPrefab, new Vector3(Random.Range(-25,25), Random.Range(1.75f,10), Random.Range(-25,25)), powerUpPrefab.transform.rotation);
                pickUps += 1;
                Debug.Log("Spawned Pellet:" + collision.gameObject.name);
            }
            else if (spawnPelletAllowed == true && gameObject.tag == "Pellet")
            {
                Toolbox.Instance.m_PlayerManager.nbPellets+= 1;
                pickUps += 1;
                Debug.Log("Pellet consumed.");
            }
            Destroy(gameObject);
        }
        if (gameObject.tag=="PowerUp" && collision.gameObject.tag == "WacMan")
        {
            wacManPoweredUp = true;
            collision.gameObject.GetComponent<Renderer>().material.SetColor("white", Color.white);
            pickUps+=1;
            Toolbox.Instance.m_PlayerManager.nbPickUps += 1;
            Debug.Log("WacMan PowerUp Activated, Total PickUps:" + pickUps);
            Instantiate(powerUpPrefab, new Vector3(Random.Range(-25, 25), Random.Range(1.75f, 5), Random.Range(-25, 25)), powerUpPrefab.transform.rotation);
            
            powerUpTime = Time.time;

            Destroy(gameObject);
        }
        if (gameObject.tag =="PowerUp" && collision.gameObject.tag == "Player")
        {
            Toolbox.Instance.m_enemy.playerPoweredUp = true;
            //playerPoweredUp = true;
            collision.gameObject.GetComponent<Renderer>().material.SetColor("green", Color.green);
            pickUps += 1;
            //Toolbox.Instance.m_PlayerManager.nbPickUps += 1;
            Debug.Log("Player PowerUp Activated, Total PickUps:" + pickUps/*.ToString("TotalPickUps")*/);
           
            powerUpTime = Time.time;

            Destroy(gameObject);
        }

    }
}
