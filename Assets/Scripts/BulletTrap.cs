using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrap : MonoBehaviour
{
    [SerializeField] private Transform bulletFirePoint;
    [SerializeField] private Transform bulletCloneParent;
    [SerializeField] private float maxDistance;
    [SerializeField] private Rigidbody bullet;
    [SerializeField] private Rigidbody bulletClone;
    public int bulletSpeed;
    public bool bulletInstantiated;
    Vector3 forwardDirection = Vector3.forward;
    public bool bulletFired;

    private void Awake()
    {
        bulletInstantiated = false;
    }
    private void Update()
    {
        Debug.DrawRay(transform.position, forwardDirection * maxDistance, Color.red);
        SpawnBullet();
    }
    public void SpawnBullet()
    {
        Ray bulletRay = new Ray(transform.position, forwardDirection);

        RaycastHit bulletHit;

        if (Physics.Raycast(bulletRay, out bulletHit, maxDistance) && !bulletInstantiated)
        {
            if (bulletHit.collider.gameObject.name == "Player" || bulletHit.collider.gameObject.name == "WacMan" && !bulletInstantiated)
            {

                bulletClone = Instantiate(bullet, bulletCloneParent,false);
                if (!bulletClone.gameObject.activeInHierarchy)
                {
                    bulletClone.gameObject.SetActive(true);
                }
                bulletClone.AddForce(forwardDirection * bulletSpeed * Time.deltaTime); //particle effect. use raycast from walls?
                bulletInstantiated = true;
                Debug.Log("Fired bullet.");
                bulletFired = true;
            }
        }
    }
}


