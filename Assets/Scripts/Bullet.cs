using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[SerializeField] private Transform player;
    //[SerializeField] private Transform bulletSpawn;
    //[SerializeField] private Transform bulletCloneParent;
    //[SerializeField] private Rigidbody bullet;
    //[SerializeField] private int maxBullets;
    //[SerializeField] private int bulletSpeed;


    //bool bulletInstantiated;

    //public void Awake()
    //{
    //    maxBullets = 5;
    //}
    //public void Update()
    //{
    //    Vector3 forwardDirection = Vector3.forward;


    //    if (Input.GetButtonDown("Fire1") && !bulletInstantiated)
    //    {
    //        Rigidbody bulletClone = Instantiate(bullet, player.position, player.rotation, bulletCloneParent);
    //        bulletClone.AddForce(Vector3.forward.normalized * bulletSpeed * Time.deltaTime); //particle effect. use raycast from walls?
    //        Debug.Log("Fired bullet.");
    //        bulletInstantiated = true;

    //        //for (int i = 0; i < maxBullets; i++)
    //        //{
    //        //    Rigidbody bulletRBClone = Instantiate(bullet, bulletSpawn.position, transform.rotation, bulletCloneParent);
    //        //    bulletRBClone.AddForce(Vector3.forward.normalized * bulletSpeed);
    //        //}
    //    }
    //    else { bulletInstantiated = false; }



    //}
    //private void OnCollisionEnter()
    //{
    //    Destroy(gameObject);
    //    //particle effect.
    //}

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
