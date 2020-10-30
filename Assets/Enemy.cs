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

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform player;
    [SerializeField] private float enemySpeed;
    [SerializeField] private Vector3 enemyFollowOffset;
    [SerializeField] private float originalDistance;
    [SerializeField] private float playerDetectionDistanceFloat;

    public bool wacManPoweredUp;
    public float wacManPowerUpTime;
    public bool playerPoweredUp;
    public int playerLives;
    
    public void Awake()
    {
        
       
        enemySpeed = Toolbox.Instance.m_EnemyManager.enemySpeed;
        originalDistance = Vector3.Distance(player.position, transform.position - enemyFollowOffset);
    }
    public void Update()
    {
        playerPoweredUp = Toolbox.Instance.m_pickUps.playerPoweredUp;
        wacManPoweredUp = Toolbox.Instance.m_pickUps.wacManPoweredUp;

        Toolbox.Instance.m_PlayerManager.playerLives = playerLives;

        Vector3 direction = Vector3.forward * playerDetectionDistanceFloat/*Vector3.Normalize(player.position - transform.position)*/;
        float distance = Vector3.Distance(player.position, transform.position - enemyFollowOffset);

        Ray playerDistanceRay = new Ray(transform.position, direction.normalized * playerDetectionDistanceFloat);
        Ray playerDetectionDistance = new Ray(transform.position, Vector3.forward.normalized * playerDetectionDistanceFloat);

        Debug.DrawRay(playerDistanceRay.origin, playerDistanceRay.direction * distance, Color.cyan, 1f);
        gameObject.transform.Translate(playerDistanceRay.direction * enemySpeed * Time.deltaTime);

        Debug.DrawRay(playerDetectionDistance.origin, Vector3.forward, Color.green);
        RaycastHit playerHit;
        if (Physics.Raycast(playerDetectionDistance, out playerHit, playerDetectionDistanceFloat, layerMask)){
            if (playerHit.collider.gameObject.tag == "Player")
            {
                enemySpeed+=1;
                //playerHit.collider.gameObject.SetActive(false);
                Toolbox.Instance.cameraState = 3;
            }
        }

        if (wacManPoweredUp == false/*distance < originalDistance / 2*/)//If WacMan has no PowerUp.
        {
            return;
            //enemySpeed --;
            //Debug.Log("Lowered Enemy speed");
        }
        else if (wacManPoweredUp == true)/*distance >= originalDistance)//If WacMan has PowerUp.*/
        {
            enemySpeed ++;
            Debug.Log("Raised Enemy speed.");
        }

            
        RaycastHit hit;
        if (Physics.Raycast(playerDistanceRay, out hit, distance, layerMask))
        {
            if (hit.collider.gameObject.GetComponent<Renderer>() != null)
            {
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                Debug.Log("YES");
            }
        }
        else
        {

            Debug.Log("NO");
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag=="Player" && playerPoweredUp)
        {
            gameObject.SetActive(false);
            Debug.Log("Player killed WacMan.");
        }
        else if (collision.gameObject.tag == "Player" && !playerPoweredUp)
        {
            collision.gameObject.SetActive(false);
            playerLives -= 1;
            Debug.Log("WacMan killed player.");
        }
    }
}
