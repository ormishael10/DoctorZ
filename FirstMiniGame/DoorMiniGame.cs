using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoorMiniGame : MonoBehaviour
{
    public GameObject door;
    public Text pressFText;
    bool canPress = false;
    public AudioClip openDoor;
    public AudioSource doorAudio;
    

    void Start()
    {
        
    }

    void Update()
    {
        if (pressFText.gameObject.activeSelf)
        {
            canPress = true;
        }
        else
        {
            canPress = false;
        }

        if (Input.GetKey(KeyCode.F) && canPress)
        {
            doorAudio.PlayOneShot(openDoor);
            door.transform.position = new Vector3(door.transform.position.x, 1.66f, door.transform.position.z);
            if (pressFText.gameObject.activeSelf)
            {
                pressFText.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pressFText.gameObject.SetActive(true);
           
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pressFText.gameObject.SetActive(false);
            
        }
        

    }
}

