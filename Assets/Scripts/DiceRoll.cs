using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    [SerializeField] private GameObject die;
    [SerializeField] private Transform dieDropTransform;
    [SerializeField] private Quaternion dieRotation;

    public bool wasTossed;
    private void Awake()
    {
        
    }
    private void Update()
    {

        if (Input.GetKey(KeyCode.R))
        {
            die.gameObject.transform.position = dieDropTransform.position;
            die.gameObject.transform.rotation = dieRotation;
            dieRotation = new Quaternion((Random.Range(-360, 360)), (Random.Range(-360, 360)), (Random.Range(-360, 360)), (Random.Range(-360, 360)));
        }
        
       
    }
    private void FixedUpdate()
    {
        
    }
}
