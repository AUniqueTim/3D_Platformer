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


    private void Awake()
    {
        bulletInstantiated = false;
    }
    private void Update()
    {
        //if (bulletInstantiated == true) {
        //    bulletClone = Instantiate(bullet/*, bulletCloneParent, instantiateInWorldSpace: true*/);
        //    bulletClone.transform.Translate(forwardDirection.normalized * bulletSpeed * Time.deltaTime);
        //}
        


        Debug.DrawRay(transform.position, forwardDirection * maxDistance, Color.red);

        //bulletClone.AddForce(forwardDirection.normalized * bulletSpeed * Time.deltaTime);

        SpawnBullet();

        //for (int i = 0; i < maxBullets; i++)
        //{
        //    Rigidbody bulletRBClone = Instantiate(bullet, bulletSpawn.position, transform.rotation, bulletCloneParent);
        //    bulletRBClone.AddForce(Vector3.forward.normalized * bulletSpeed);
        //}
    }
    public void FixedUpdate()
    {
      

        //Ray bulletRay = new Ray(transform.position, forwardDirection);

        //RaycastHit bulletHit;

        //if (Physics.Raycast(bulletRay, out bulletHit, maxDistance) && !bulletInstantiated)
        //{
        //    if (bulletInstantiated)
        //    {
        //        return;
        //    }
        //    else if (bulletHit.collider.gameObject.name == "Player" || bulletHit.collider.gameObject.name == "WacMan")
        //    {

        //        bulletClone = Instantiate(bullet);
        //        if (!bulletClone.gameObject.activeInHierarchy)
        //        {
        //            bulletClone.gameObject.SetActive(true);
        //        }
        //        bulletClone.AddForce(forwardDirection * bulletSpeed * Time.deltaTime); //particle effect. use raycast from walls?
        //        bulletInstantiated = true;
        //        Debug.Log("Fired bullet.");
        //    }
            
            

        //}

        //else if (Physics.Raycast(bulletRay, out bulletHit, maxDistance))
        //{
        //    if (bulletHit.collider.gameObject.name!="Player" || bulletHit.collider.gameObject.name != "WacMan" && bulletInstantiated == true)
        //    {
        //        bulletInstantiated = false;            }
        //}
       
        
    }
    public void SpawnBullet()
    {
        Ray bulletRay = new Ray(transform.position, forwardDirection);

        RaycastHit bulletHit;

        if (Physics.Raycast(bulletRay, out bulletHit, maxDistance) && !bulletInstantiated)
        {
            if (bulletHit.collider.gameObject.name == "Player" || bulletHit.collider.gameObject.name == "WacMan" && !bulletInstantiated)
            {

                bulletClone = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation,bulletCloneParent);
                if (!bulletClone.gameObject.activeInHierarchy)
                {
                    bulletClone.gameObject.SetActive(true);
                }
                bulletClone.AddForce(forwardDirection * bulletSpeed * Time.deltaTime); //particle effect. use raycast from walls?
                bulletInstantiated = true;
                Debug.Log("Fired bullet.");
            }
        }
    }
}


