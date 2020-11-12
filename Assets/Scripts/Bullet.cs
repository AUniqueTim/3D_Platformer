using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [SerializeField] private GameObject wacMan;
    


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Toolbox.Instance.m_PlayerManager.playerLives -= 1;
            //particle effect.
            Debug.Log("Bullet hit Player.");
            gameObject.SetActive(false);
            Toolbox.Instance.m_BulletTrap.bulletInstantiated = false;
            Toolbox.Instance.m_BulletTrap.SpawnBullet();
        }
        else if (collision.gameObject.tag == "WacMan")
        {
            wacMan.transform.position = new Vector3(Random.Range(-100, 100), 0);
            gameObject.SetActive(false);//Destroy(gameObject); //particle effect.
            Debug.Log("Bullet hit WacMan.");
            Toolbox.Instance.m_BulletTrap.bulletInstantiated = false;
            Toolbox.Instance.m_BulletTrap.SpawnBullet();
        }
        else if (collision.gameObject.tag == "WorldGeometry")
        {
            Toolbox.Instance.m_BulletTrap.bulletInstantiated = false;
            Toolbox.Instance.m_BulletTrap.SpawnBullet();
            gameObject.SetActive(false);
        } 
        //else if (collision.gameObject)
        //{
           
        //    Toolbox.Instance.m_BulletTrap.bulletInstantiated = false;
        //    Toolbox.Instance.m_BulletTrap.SpawnBullet();
        //    gameObject.SetActive(false);//Destroy(gameObject); //Particle Effect.
        //}

    }
}
