using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //PlayerRotation
    float mouseX;
    float mouseY;
    public Transform camera;
    float cameraX;
    public float lookSpeed;


    //PlayerMove
    float xAxis;
    float zAxis;
    public float moveSpeed;
    public CharacterController cc;
    Vector3 v;
    bool isCrouch;
    

    //Gravity
    public float gravity;
    public LayerMask ground;
    public Transform groundCheck;
    public bool isGround;
    float radius;
    Vector3 velocity;

    //Shot
    public AudioSource shotAudio;
    public AudioClip shoting;
    public GameObject rayGunWeapon;
    public GameObject rayGunBullet;
    public Transform bulletPosition;
    public float bulletSpeed;
    int amountOfBullets;
    int maxBullets;
    public Text Ammo;
    float shotingRate;
    bool rayGun;
    public Text reloadText;
    public AudioClip reload;
    public AudioSource reloadAudio;

    //Teleport
    public Transform tpBulletPosition;
    public Transform blueTeleportBullet;
    public Transform redTeleportBullet;
    public int amountOfBlueBullets;
    public int amountOfRedBullets;
    public bool disabled;
    bool teleportGun;
    public GameObject tpGun;
    float blueShotRate;
    float redShotRate;


    public Transform finalStage;





    private void Awake()
    {
        disabled = false;
    }

    void Start()
    {
        //PlayerRotation
        lookSpeed = 300f;
        cameraX = 0;

        //PlayerMove
        moveSpeed = 6f;
        isCrouch = false;

        //Gravity
        radius = 0.6f;
        isGround = false;
        gravity = -0.3f;

        //Shot
        rayGunWeapon.SetActive(true);
        rayGun = true;
        maxBullets = 40;
        amountOfBullets = maxBullets;
        bulletSpeed = 20000f;
        shotingRate = 0;
        Ammo.text = "Ammo: " + amountOfBullets.ToString();

        //Teleport
        //amountOfRedBullets = 1;
        //amountOfBlueBullets= 2;
        blueShotRate = 0f;
        redShotRate = 0f;

        finalStage.gameObject.GetComponent<Transform>();

    }

   
    void Update()
    {
        if(disabled == false)
        {

            PlayerRotation();
            PlayerMove(); 
            Shot();
            Gravity();
            TeleportCreator();
            Mouse();
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene("DoctorZ");
        }
    }


    void Mouse()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            Cursor.visible = !Cursor.visible;
        }
    }

    void PlayerRotation()
    {
        mouseX = Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime;
        transform.Rotate(0, mouseX, 0);
        mouseY = Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;
        cameraX -= mouseY;
        cameraX = Mathf.Clamp(cameraX, -90, 90);
        camera.localRotation = Quaternion.Euler(cameraX, 0, 0);//return the rotation value
    }

    void PlayerMove()
    {
        
        xAxis = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        zAxis = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        v = transform.forward * zAxis + transform.right * xAxis;
        cc.Move(v);
        
        if(Input.GetKey(KeyCode.LeftShift))
        {
            
            moveSpeed = 12f;
            
        }
        else
        {
            moveSpeed = 6f;
        }
        
        if(Input.GetKeyDown(KeyCode.C))
        {
            if(isCrouch == false)
            {
                Crouch();
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                rayGunWeapon.transform.localScale = new Vector3(50, 50, 50);     
                tpGun.transform.localScale = new Vector3(0.07f, 0.06f, 0.06f);
                isCrouch = false;
            }
        }


        


    }

    void Crouch()
    {
        isCrouch = true;
        transform.localScale = new Vector3(1, 0.5f, 1f);
        rayGunWeapon.transform.localScale = new Vector3(50, 100, 50);
        tpGun.transform.localScale = new Vector3(0.07f, 0.12f, 0.06f);
    }


    void Gravity()
    {
        if(Physics.CheckSphere(groundCheck.position, radius, ground)) 
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
        if(isGround == false)
        {
            velocity.y += gravity + Time.deltaTime;
        }
        else
        {
            velocity.y = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            velocity.y += 10;
        }
        cc.Move(velocity * Time.deltaTime);
    }


    void Shot()
    {
        
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            rayGunWeapon.SetActive(true);
            tpGun.SetActive(false);
            rayGun = true;
            teleportGun= false;

        }
        
        if (Time.time >= shotingRate && rayGun)
        {
            if (Input.GetMouseButton(0) && amountOfBullets > 0)
            {
                var bulletSpawn = Instantiate(rayGunBullet, bulletPosition.transform.position, rayGunBullet.transform.rotation);// Create Bullets
                bulletSpawn.GetComponent<Rigidbody>().velocity = bulletPosition.forward * bulletSpeed * Time.deltaTime;// Shot the Bullets Forward  
                amountOfBullets--;
                Ammo.text = "Ammo: " + amountOfBullets.ToString();
                shotAudio.PlayOneShot(shoting);
            }
                          // shots per second
            shotingRate = Time.time + 1f / 10;

            if (amountOfBullets == 0)
            {
                reloadText.gameObject.SetActive(true);
                if(Input.GetKey(KeyCode.R))//Reload
                {
                    reloadAudio.PlayOneShot(reload);
                    reloadText.gameObject.SetActive(false);
                    
                    StartCoroutine(Reload());
                }
            }
        }
    }

    IEnumerator Reload()
    {

        yield return new WaitForSeconds(3.5f);
        amountOfBullets = maxBullets;
        Ammo.text = "Ammo: " + amountOfBullets.ToString();
        reloadText.gameObject.SetActive(false);
    }

    void TeleportCreator()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))//Teleport Weapon On
        {
            rayGunWeapon.SetActive(false);
            tpGun.SetActive(true);
            rayGun = false;
            teleportGun= true;
        }

        
        {
            if (Input.GetMouseButtonDown(0) && teleportGun == true) // Create Blue TP Bullet
            {
                var blue = Instantiate(blueTeleportBullet, tpBulletPosition.transform.position, blueTeleportBullet.transform.rotation);
                blue.GetComponent<Rigidbody>().velocity = tpBulletPosition.forward * bulletSpeed * Time.deltaTime;
               
            }
            
        }

        
        {
            if (Input.GetMouseButtonDown(1) && teleportGun == true) //Create Red TP Bullet
            {
                var red = Instantiate(redTeleportBullet, tpBulletPosition.transform.position, redTeleportBullet.transform.rotation);
                red.GetComponent<Rigidbody>().velocity = tpBulletPosition.forward * bulletSpeed * Time.deltaTime;
                
            }
            
        }
        

        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Win")
        {
            Cursor.visible = Cursor.visible;
            SceneManager.LoadScene("Victory");

        }

        if(other.gameObject.tag == "BossFinalDoor")
        {
           finalStage.transform.position = new Vector3(finalStage.transform.position.x,finalStage.transform.position.y + 5f,finalStage.transform.position.z);
        }
    }
}
