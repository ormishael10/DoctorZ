using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BlueBulletEngine : MonoBehaviour
{
   
    public GameObject blueTeleport;
    
   
    private void Start()
    {
        Destroy(gameObject, 0.5f);

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            
            Destroy(gameObject);
            transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y + 1f, transform.position.z - 0.5f);
            Instantiate(blueTeleport, transform.position, blueTeleport.transform.rotation);
           
        }
        
    }
}
