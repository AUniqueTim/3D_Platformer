using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastExample : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform enemy;

    private void Update()
    {
        Vector3 direction = Vector3.Normalize(enemy.position - transform.position);
        float distance = Vector3.Distance(enemy.position, transform.position);

        Ray ray = new Ray(this.transform.position, direction);

        Debug.DrawRay(ray.origin, ray.direction * distance, Color.cyan, 1f);


        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance, layerMask))
        {
            hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
            Debug.Log("YES");
        }
        else 
        {

            Debug.Log("NO");
        }
    }
}
