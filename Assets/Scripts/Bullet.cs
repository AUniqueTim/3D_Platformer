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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //lose health, particle effect.
            Debug.Log("Bullet hit Player.");
        }
        else if (collision.gameObject.tag == "WacMan")
        {
            //Spawn new WacMan
            Destroy(gameObject); //particle effect.
            Debug.Log("Bullet hit WacMan.");
        }
        else if (collision.gameObject)
        {
            Destroy(gameObject); //Particle Effect.
        }

    }
}
