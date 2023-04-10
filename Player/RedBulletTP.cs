using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBulletTP : MonoBehaviour
{
    public GameObject redTeleport;
    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {

            Destroy(gameObject);
            transform.position = new Vector3(transform.position.x -0.5f , transform.position.y + 1f, transform.position.z - 0.5f);
            Instantiate(redTeleport, transform.position, redTeleport.transform.rotation);


        }

    }
}
