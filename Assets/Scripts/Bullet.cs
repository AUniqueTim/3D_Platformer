using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private Transform bulletCloneParent;
    [SerializeField] private Rigidbody bullet;
    [SerializeField] private int maxBullets;
    [SerializeField] private int bulletSpeed;

    bool bulletInstantiated;

    public void Awake()
    {
        maxBullets = 5;
    }
    public void Update()
    {
        if (Input.GetButtonDown("Fire1") && !bulletInstantiated)
        {
            Rigidbody bulletClone = Instantiate(bullet, player.position, player.rotation, bulletCloneParent);
            bulletClone.AddForce(Vector3.forward.normalized * bulletSpeed * Time.deltaTime);
            Debug.Log("Fired bullet.");
            bulletInstantiated = true;

            //for (int i = 0; i < maxBullets; i++)
            //{
            //    Rigidbody bulletRBClone = Instantiate(bullet, bulletSpawn.position, transform.rotation, bulletCloneParent);
            //    bulletRBClone.AddForce(Vector3.forward.normalized * bulletSpeed);
            //}
        }
        else { bulletInstantiated = false; }

      

    }
    private void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
}
