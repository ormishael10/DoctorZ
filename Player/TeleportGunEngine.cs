using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TeleportGunEngine : MonoBehaviour
{
    public  Transform redTP;
    public Transform blueTP;
    public GameObject player;
    PlayerMovement PlayerMovement;
    public AudioSource teleportAudio;
    public Camera camera;    
    

    private void Start()
    {
        
        PlayerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        redTP = GameObject.Find("RedPortal(Clone)").transform;
        
        
        
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Teleport")
        {
            
            StartCoroutine(RedTeleport());
            
        }
        else
        {
            StopAllCoroutines();
        }
            
    }

    IEnumerator RedTeleport()
    {
        teleportAudio.Play();
        PlayerMovement.disabled = true;
        yield return new WaitForSeconds(0.1f);
        player.transform.position = new Vector3
            (
            redTP.transform.position.x,
            redTP.transform.position.y,
            redTP.transform.position.z
            );
        yield return new WaitForSeconds(0.1f);
        PlayerMovement.disabled = false;
    }

    

}
