using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Raycast forward follow at speed until raycast length less than minimum.
    //Adjust rotation when too close to an object.
    //Cast new ray to follow.

    //On Player Sight, turn off Raycast, follow player, slowly increasing speed
    // Unless Player == PowerUpped, in which cast leave raycast on (continue patrol).

    [SerializeField] private GameObject wacMan;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject powerUp;
    public float enemySpeed;
    [SerializeField] private float originalEnemySpeed;
    [SerializeField] private Vector3 enemyFollowOffset;
    [SerializeField] private float originalDistance;
    [SerializeField] private float playerDetectionDistanceFloat;
    [SerializeField] private float distanceToPowerUp;

    public bool wacManPoweredUp;
    public float wacManPowerUpTime;
    public bool playerPoweredUp;
    public int playerLives;
    Ray playerDistanceRay;
     
    public void Awake()
    {
        enemySpeed = Toolbox.Instance.m_EnemyManager.enemySpeed;
        originalEnemySpeed = enemySpeed;
        originalDistance = Vector3.Distance(player.position, transform.position - enemyFollowOffset);
    }
    public void Update()
    {
        

        //playerPoweredUp = Toolbox.Instance.m_pickUps.playerPoweredUp;
        //wacManPoweredUp = Toolbox.Instance.m_pickUps.wacManPoweredUp;

        playerLives = Toolbox.Instance.m_PlayerManager.playerLives;

        Vector3 direction = Vector3.forward * playerDetectionDistanceFloat;
        float distance = Vector3.Distance(player.position, transform.position - enemyFollowOffset);
        distanceToPowerUp = Vector3.Distance(powerUp.transform.position, transform.position);
        Vector3 directiontoPowerUp = Vector3.forward * distanceToPowerUp;
        Ray playerDistanceRay = new Ray(transform.position, direction.normalized * playerDetectionDistanceFloat);
        Ray playerDetectionDistance = new Ray(transform.position, Vector3.forward.normalized * playerDetectionDistanceFloat);
        Ray powerUpDetectionRay = new Ray(powerUp.transform.position, directiontoPowerUp.normalized *distanceToPowerUp);

        Debug.DrawRay(playerDistanceRay.origin, playerDistanceRay.direction * distance, Color.cyan, 1f);
        //gameObject.transform.Translate(playerDistanceRay.direction * enemySpeed * Time.deltaTime);  //This line chases player.
        gameObject.transform.Translate(powerUpDetectionRay.direction * enemySpeed * Time.deltaTime);


        Debug.DrawRay(playerDetectionDistance.origin, Vector3.forward, Color.green);
        Debug.DrawRay(playerDistanceRay.origin, direction);
        RaycastHit playerHit;
        if (Physics.Raycast(playerDetectionDistance, out playerHit, playerDetectionDistanceFloat, layerMask)){
            if (playerHit.collider.gameObject.tag == "Player")
            {
                gameObject.transform.Translate(playerDistanceRay.direction * enemySpeed * Time.deltaTime);
                //enemySpeed +=1;
                //playerHit.collider.gameObject.SetActive(false);
                Toolbox.Instance.m_CameraController.CameraState = 3;
                Debug.Log("Player Detection Distance ray Hit");
            }
            if (wacManPoweredUp == false)
            {
                //return;
                //enemySpeed --;
                //Debug.Log("Lowered Enemy speed");
            }
            else if (wacManPoweredUp == true)
            {
                enemySpeed += 1;
                Debug.Log("Raised Enemy speed.");
            }
            else if (playerPoweredUp)
            {
                enemySpeed -= 1;
            }
        }

        

        Debug.DrawRay(playerDistanceRay.origin, playerDistanceRay.direction, Color.yellow, 120);
        RaycastHit hit;
        
        if (Physics.Raycast(playerDistanceRay, out hit, distance, layerMask))
        {
            if (hit.collider.gameObject.GetComponent<Renderer>() != null && hit.collider.gameObject.tag=="Player")
            {
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                Debug.Log("PlayerDistanceRay Hit Player.");
                if (wacManPoweredUp)
                {
                    Toolbox.Instance.m_PlayerManager.playerLives -= 1;
                    player.transform.position = new Vector3(0, 0, 0);
                }
                else if (playerPoweredUp && !wacManPoweredUp)
                {
                    enemySpeed-=1;

                }
                //else { gameObject.transform.Translate(transform.position * distanceToPowerUp * enemySpeed * Time.deltaTime); }
            }
            Debug.DrawRay(playerDistanceRay.origin, direction, Color.magenta, 10, true);
        }
        RaycastHit powerUpHit;
        Debug.DrawRay(powerUpDetectionRay.origin, powerUpDetectionRay.direction, Color.black);
        if (Physics.Raycast(powerUpDetectionRay, out powerUpHit, distanceToPowerUp, layerMask))
        {
            //gameObject.transform.Translate(transform.position + powerUp.transform.position * enemySpeed * Time.deltaTime);
            Debug.Log("WacMan PowerUp Detection Ray Hit.");
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag=="Player" && playerPoweredUp && !wacManPoweredUp)
        {
            Toolbox.Instance.m_EnemyManager.wacManLives -= 1;
            Toolbox.Instance.m_EnemyManager.wacMan.gameObject.transform.position = new Vector3(Random.Range(-100,100),0);
            //gameObject.SetActive(false);
            Debug.Log("Player killed WacMan.");
            //1UP UI
            Toolbox.Instance.m_PlayerManager.playerLives += 1;
            enemySpeed = originalEnemySpeed;
            playerPoweredUp = false;
        }
        else if (collision.gameObject.tag == "Player" && !playerPoweredUp  && wacManPoweredUp)
        {
            Toolbox.Instance.m_PlayerManager.playerLives -= 1;
            Debug.Log("WacMan killed player.");
            wacMan.transform.position = new Vector3(Random.Range(-100,100),0 );
            wacManPoweredUp = false;
            enemySpeed = originalEnemySpeed;
            if (Toolbox.Instance.m_PlayerManager.playerLives <= 0)
            {
                Toolbox.Instance.m_PlayerManager.GameOver();
                collision.gameObject.SetActive(false);
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
