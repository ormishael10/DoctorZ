using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    Transform player;
    float speed;
   
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        
        speed = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        Destroy(gameObject, 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
