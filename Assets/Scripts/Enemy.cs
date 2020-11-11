using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    

    [SerializeField] private GameObject wacMan;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform player;
    [SerializeField] private Transform powerUp;
    public float enemySpeed;
    [SerializeField] private float originalEnemySpeed;
    [SerializeField] private Vector3 enemyFollowOffset;
    [SerializeField] private float originalDistance;
    [SerializeField] private float playerDetectionDistanceFloat;
    [SerializeField] private float distanceToPowerUp;
    [SerializeField] private float distance;
    [SerializeField] private GameObject powerUpPrefabClone;

    public bool wacManPoweredUp;
    public float wacManPowerUpTime;
    public bool playerPoweredUp;
    public int playerLives;
    Ray playerDistanceRay;

    public bool collidingWithPlayer;

    public void Awake()
    {
        enemySpeed = Toolbox.Instance.m_EnemyManager.enemySpeed;
        originalEnemySpeed = enemySpeed;
        originalDistance = Vector3.Distance(player.position, transform.position - enemyFollowOffset);

        powerUpPrefabClone = Toolbox.Instance.m_pickUps.powerUpPrefabClone;
    }
    public void Update()
    {

        wacManPoweredUp = Toolbox.Instance.m_PlayerManager.wacManPoweredUp;
        playerPoweredUp = Toolbox.Instance.m_PlayerManager.playerPoweredUp;


        playerLives = Toolbox.Instance.m_PlayerManager.playerLives;

        Vector3 direction = Vector3.forward * playerDetectionDistanceFloat;
        distance = Vector3.Distance(player.position, transform.position - enemyFollowOffset);

        Vector3 directiontoPowerUp = powerUpPrefabClone.gameObject.transform.position - transform.position;
        distanceToPowerUp = Vector3.Distance(powerUpPrefabClone.gameObject.transform.position, transform.position);
        

        Ray playerDistanceRay = new Ray(transform.position, direction.normalized * playerDetectionDistanceFloat);
        Ray playerDetectionDistance = new Ray(transform.position, Vector3.forward.normalized * playerDetectionDistanceFloat);
        Ray powerUpDetectionRay = new Ray(powerUpPrefabClone.gameObject.transform.position, directiontoPowerUp.normalized * distanceToPowerUp);
        

        Debug.DrawRay(playerDistanceRay.origin, playerDistanceRay.direction * distance, Color.cyan, 1f);
        gameObject.transform.Translate(playerDistanceRay.direction * enemySpeed * Time.deltaTime);  //This line chases player.
        //gameObject.transform.Translate(powerUpDetectionRay.direction * enemySpeed * Time.deltaTime);// This line chases (FIRST ONLY) PowerUp.
        //if (gameObject.tag == "PowerUp")
        //{
        //    powerUpPrefabClone = gameObject;
        //}

        Debug.DrawRay(playerDetectionDistance.origin, Vector3.forward.normalized * distance, Color.green);
        Debug.DrawRay(playerDistanceRay.origin, direction.normalized * distance, Color.red);
        RaycastHit playerHit;
        if (Physics.Raycast(playerDetectionDistance, out playerHit, playerDetectionDistanceFloat, layerMask)){
            if (playerHit.collider.gameObject.tag == "PowerUp")
            {
                gameObject.transform.Translate(-powerUpDetectionRay.origin * enemySpeed * Time.deltaTime);
            }
            else if (playerHit.collider.gameObject.tag == "Player")

                {
                    gameObject.transform.Translate(-playerDetectionDistance.direction * enemySpeed * Time.deltaTime);
                    //enemySpeed +=1;
                    //playerHit.collider.gameObject.SetActive(false);
                    Toolbox.Instance.m_CameraController.CameraState = 3;
                    Debug.Log("Player Detection Distance ray Hit.");
                }

            if (wacManPoweredUp)
                {
               
                    enemySpeed += 1;
                    Debug.Log("Raised Enemy speed.");
                }
            if (playerPoweredUp)
                {
                    enemySpeed -= 1;
                    Debug.Log("Lowered Enemy speed.");
                }
           
            if (enemySpeed <= -10)
                {
                    enemySpeed = 10;
                }
        }

        

        Debug.DrawRay(playerDistanceRay.origin, playerDistanceRay.direction, Color.yellow, 120);
        RaycastHit hit;
        
        if (Physics.Raycast(playerDistanceRay, out hit, distance, layerMask))
        {
            if (hit.collider.gameObject.GetComponent<Renderer>() != null && hit.collider.gameObject.tag=="Player")
            {
                //hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                Debug.Log("PlayerDistanceRay Hit Player.");

                if (wacManPoweredUp)
                {
                    gameObject.transform.Translate(playerDistanceRay.direction * enemySpeed * Time.deltaTime);
                }

                if (wacManPoweredUp && collidingWithPlayer)
                    {
                        player.transform.position = new Vector3(0, 0, 0);
                    }
                if (playerPoweredUp)
                {
                    gameObject.transform.Translate(-playerDistanceRay.direction * enemySpeed * Time.deltaTime);

                }
            }
            Debug.DrawRay(playerDistanceRay.origin, direction, Color.magenta, 10, true);
        }
        RaycastHit powerUpHit;
        Debug.DrawRay(powerUpDetectionRay.origin, powerUpDetectionRay.direction * distanceToPowerUp, Color.black);
        if (Physics.Raycast(powerUpDetectionRay, out powerUpHit, distanceToPowerUp, layerMask) && powerUpHit.collider.gameObject.tag=="PowerUp")
        {
            //gameObject.transform.Translate(powerUp.position -transform.position * distanceToPowerUp * Time.deltaTime);
            
            powerUpHit.collider.gameObject.GetComponent<MeshRenderer>().material.SetColor("Yellow", Color.yellow);
            Debug.Log("WacMan PowerUp Detection Ray Hit.");
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collidingWithPlayer = true;
        }
        else { collidingWithPlayer = false; }

        if (collision.gameObject.tag=="Player" && playerPoweredUp && !wacManPoweredUp)
        {
            Toolbox.Instance.m_EnemyManager.wacManLives -= 1;
            Toolbox.Instance.m_EnemyManager.wacMan.gameObject.transform.position = new Vector3(Random.Range(-100,100),0);
            //gameObject.SetActive(false);
            Debug.Log("Player killed WacMan.");
            //1UP UI
            Toolbox.Instance.m_PlayerManager.playerLives += 1;
            enemySpeed = originalEnemySpeed;
            Toolbox.Instance.m_PlayerManager.playerPoweredUp = false;
        }
        else if (collision.gameObject.tag == "Player" && !playerPoweredUp  && wacManPoweredUp)
        {
            Toolbox.Instance.m_PlayerManager.playerLives -= 1;
            Debug.Log("WacMan killed player.");
            wacMan.transform.position = new Vector3(Random.Range(-100,100),0 );
            Toolbox.Instance.m_PlayerManager.wacManPoweredUp = false;
            enemySpeed = originalEnemySpeed;
            if (Toolbox.Instance.m_PlayerManager.playerLives <= 0)
            {
                collision.gameObject.SetActive(false);
                Toolbox.Instance.m_PlayerManager.GameOver();
            }

        }
        else if(collision.gameObject.tag=="Player" && playerPoweredUp && wacManPoweredUp)
        {
            playerPoweredUp = false;
            wacManPoweredUp = false;
        }
        else if (collision.gameObject.tag== "Player" && !playerPoweredUp && !wacManPoweredUp)
        {
            return;
        }
    }
}
