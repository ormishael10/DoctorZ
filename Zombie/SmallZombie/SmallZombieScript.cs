using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class SmallZombieScript : MonoBehaviour
{
    // AI chase parameter
    public Transform Player;
    public Transform zombie;
    public NavMeshAgent agent;
    public float chaseRadius = 2;
    

    //Particle
    public ParticleSystem ps;

    //animition controll
    Animator smallZombieAnimatorControll;

    //the zombie hp
    public int smallZombieHp ;

    //checkin in the game when the zombie dead
    bool isDead;


    // Start is called before the first frame update
    void Start()
    {
        smallZombieHp = 100;

      smallZombieAnimatorControll = GetComponent<Animator>();

        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChaseSmallZombie();


    }

    public void ChaseSmallZombie()
    {
        if (Vector3.Distance(zombie.position,Player.position) < chaseRadius )
        {
            
            agent.isStopped = true;
            agent.speed = 0f;

            smallZombieAnimatorControll.SetBool("Punch", true);
            smallZombieAnimatorControll.SetBool("Run", false);
            smallZombieAnimatorControll.SetBool("isIdle", false);

            if (isDead)
            {
                agent.isStopped = true;
                agent.speed = 0f;

                smallZombieAnimatorControll.SetBool("Death", true);
                smallZombieAnimatorControll.SetBool("Run", false);
                smallZombieAnimatorControll.SetBool("isIdle", false);
                smallZombieAnimatorControll.SetBool("Punch", false);

                Destroy(gameObject, 5);
            }

        }
        else if(Vector3.Distance(zombie.position, Player.position) <= chaseRadius + 22 )
        {
            transform.LookAt(Player.position);
            if (isDead)
            {
                agent.enabled = false;
                agent.isStopped = true;
                agent.speed = 0f;

            }
            else if(!isDead)
            {
                agent.destination = Player.position;
                agent.isStopped = false;
                agent.speed = 9.5f;
            }
            
            smallZombieAnimatorControll.SetBool("Run", true);
            smallZombieAnimatorControll.SetBool("isIdle", false);
            smallZombieAnimatorControll.SetBool("Punch", false);

            if (isDead)
            {
                agent.enabled = false;
                agent.isStopped = true;
                agent.speed = 0f;

                smallZombieAnimatorControll.SetBool("Death", true);
                smallZombieAnimatorControll.SetBool("Run", false);
                smallZombieAnimatorControll.SetBool("isIdle", false);
                smallZombieAnimatorControll.SetBool("Punch", false);

                Destroy(gameObject, 5);
            }
        }
        else
        {
            
            smallZombieAnimatorControll.SetBool("Run", false);
            smallZombieAnimatorControll.SetBool("isIdle", true);
            smallZombieAnimatorControll.SetBool("Punch", false);

            agent.isStopped = true;
            agent.speed = 0f;
            if (isDead)
            {
                agent.enabled = false;
                
                agent.isStopped = true;
                agent.speed = 0f;

                smallZombieAnimatorControll.SetBool("Death", true);
                smallZombieAnimatorControll.SetBool("Run", false);
                smallZombieAnimatorControll.SetBool("isIdle", false);
                smallZombieAnimatorControll.SetBool("Punch", false);

                Destroy(gameObject, 7);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "RayBullet")
        {
            smallZombieHp -= 15;
            ps.Play(true);
            Destroy(other.gameObject);
            
            if (!isDead)
            {
                if(smallZombieHp <= 0)
                {
                    agent.enabled = false;
                    isDead = true;
                      agent.isStopped = true;
                      agent.speed = 0f;

                      smallZombieAnimatorControll.SetBool("Death", true);
                      smallZombieAnimatorControll.SetBool("Run", false);
                      smallZombieAnimatorControll.SetBool("isIdle", false);
                      smallZombieAnimatorControll.SetBool("Punch", false);

                      Destroy(gameObject,5);
                }

            }
            
        }
    }

}
