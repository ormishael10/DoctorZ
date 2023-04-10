using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGunEngine : MonoBehaviour
{
    public Transform Bullet;
    Transform player;
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        Destroy(gameObject,0.2f);
    }

    private void Start()
    {
        
       
        
    }
    private void Update()
    {
       // Bullet.localRotation = player.localRotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target" || other.gameObject.tag == "Wall")
        {
            
            Destroy(gameObject);
        }

    }

}
