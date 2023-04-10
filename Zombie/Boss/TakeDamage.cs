using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class TakeDamage : MonoBehaviour
{
    //heath
    public int maxHeath;
    public AudioSource dieAudio;
    public AudioClip dieSound;
    public ParticleSystem ps;

    //animition
    public Animator animator;

    //trun on and off the attack
    bool meleeRange;
    bool longRange;

    //AI
    public Transform Player;
    public Transform zombie;
    public NavMeshAgent zombieNavMesh;

    //the end door open
    public Transform endDoor;
    bool endDoorIsOpen;

    //RangeAttack
    public GameObject fireBallBullet;
    public Transform fireBallPosition;
    bool inRange;
    float shotingRate;


    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
        endDoorIsOpen = false;
        zombieNavMesh= GetComponent<NavMeshAgent>();
        longRange = true;
        meleeRange = false;
        shotingRate = 0;
        maxHeath = 1000;

        
    }

    // Update is called once per frame
    void Update()
    {
        AttackByOrder();

        OpenDoor();
        FireBall();
        print(inRange);

    }


    public void OpenDoor()
    {
        if (endDoorIsOpen)
        {
            endDoor.transform.position = new Vector3(3.9f,1.9f,6.15f);
        }
    }


    public void AttackByOrder()
    {
        if (Vector3.Distance(zombie.position, Player.position) <= 55)
        {
            inRange = false;
            transform.LookAt(Player.position);
            zombieNavMesh.destination = Player.position;
            zombieNavMesh.isStopped = false;
            zombieNavMesh.speed = 5f;
            animator.SetBool("LongAtt", false);
            animator.SetBool("Walking", true);

            if (maxHeath == 0)
            {
                endDoorIsOpen = true;
                zombieNavMesh.enabled = false;
                animator.SetBool("Death", true);
                Destroy(gameObject, 5);
                print("dead");
            }

            if (Vector3.Distance(zombie.position, Player.position) <= 45)
            {
                inRange = true;  
               // transform.LookAt(Player.position);
                zombieNavMesh.isStopped = true;
                zombieNavMesh.speed = 0;
                animator.SetBool("LongAtt", true);

                if (maxHeath == 0)
                {
                    endDoorIsOpen = true;
                    zombieNavMesh.enabled = false;
                    animator.SetBool("Death", true);
                    Destroy(gameObject, 5);
                    print("dead");
                }

                if (Vector3.Distance(zombie.position,Player.position) <= 25)
                {
                    inRange = false;
                   // transform.LookAt(Player.position);
                    zombieNavMesh.destination = Player.position;
                    zombieNavMesh.isStopped = false;
                    zombieNavMesh.speed = 5f;
                    animator.SetBool("LongAtt", false);
                    animator.SetBool("Walking", true);
                    animator.SetBool("MeleeAtt", false);

                    if (maxHeath == 0)
                    {
                        endDoorIsOpen = true;
                        zombieNavMesh.enabled = false;
                        animator.SetBool("Death", true);
                        Destroy(gameObject, 5);
                        print("dead");
                    }

                    if (Vector3.Distance(zombie.position,Player.position) < 6.5f)
                    {
                        inRange = false;
                        zombieNavMesh.stoppingDistance = 7;
                        zombieNavMesh.isStopped = true;
                        zombieNavMesh.speed = 0;
                        print(zombieNavMesh.speed);
                        animator.SetBool("MeleeAtt", true);
                        animator.SetBool("Walking", false);

                        if (maxHeath == 0)
                        {
                            endDoorIsOpen = true;
                            zombieNavMesh.enabled = false;
                            animator.SetBool("Death", true);
                            Destroy(gameObject, 5);
                            print("dead");
                        }
                    }
                    else
                    {
                        inRange = false;
                        animator.SetBool("MeleeAtt", false);
                        animator.SetBool("Walking", true);
                    }
                }
                
            }
        }
    }

 
    void FireBall()
    {
        if (inRange == true)
        {
            
            Instantiate(fireBallBullet, fireBallPosition.transform.position, fireBallBullet.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "RayBullet")
        {
            ps.Play(true);
            maxHeath -= 10;
            if (maxHeath == 0)
            {
                dieAudio.PlayOneShot(dieSound);
                endDoorIsOpen = true;
                zombieNavMesh.enabled = false;
                animator.SetBool("Death", true);
                Destroy(gameObject,5);
                print("dead");
            }
        }
    }
}
