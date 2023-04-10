using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiniBossManager : MonoBehaviour
{
    // AI chase parameter
    public Transform Player;
    public Transform zombie;
    public NavMeshAgent agent;
    public float chaseRadius = 5;

    //attking 
    bool isFirst;
    bool isSecond;

    //Particle
    public ParticleSystem ps;

    //animition controll
    Animator miniBossZombieAnimatorControll;

    //the zombie hp
    public int smallZombieHp;

    //checkin in the game when the zombie dead
    bool isDead;

    //Audio
    
    public AudioSource zombieAudio;

    // Start is called before the first frame update
    void Start()
    {
        smallZombieHp = 400;

        miniBossZombieAnimatorControll = GetComponent<Animator>();

        isDead = false;

        isFirst = true;
        isSecond = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChaseFatZombie();



    }

    public void ChaseFatZombie()
    {
        if (Vector3.Distance(zombie.position, Player.position) < chaseRadius)
        {
            agent.destination = Player.position;
            miniBossZombieAnimatorControll.SetBool("FirstAtt", true);

            miniBossZombieAnimatorControll.SetBool("Run", false);

            agent.isStopped = true;
            agent.speed = 0f;


            if (isDead)
            {
                agent.enabled = false;
                agent.isStopped = true;
                agent.speed = 0f;

                miniBossZombieAnimatorControll.SetBool("Run", false);
                miniBossZombieAnimatorControll.SetBool("isIdle", false);
                miniBossZombieAnimatorControll.SetBool("FirstAtt", false);
                miniBossZombieAnimatorControll.SetBool("Death", true);

                Destroy(gameObject, 5f);
            }

        }
        else if (Vector3.Distance(zombie.position, Player.position) < chaseRadius + 45 )
        {
            miniBossZombieAnimatorControll.SetBool("isIdle", false);
            miniBossZombieAnimatorControll.SetBool("Run", true);
            miniBossZombieAnimatorControll.SetBool("FirstAtt", false);
            agent.destination = Player.position;

            agent.isStopped = false;
            agent.speed = 17f;

            if (isDead)
            {
                agent.enabled = false;
                agent.isStopped = true;
                agent.speed = 0f;

                miniBossZombieAnimatorControll.SetBool("Run", false);
                miniBossZombieAnimatorControll.SetBool("isIdle", false);
                miniBossZombieAnimatorControll.SetBool("FirstAtt", false);
                miniBossZombieAnimatorControll.SetBool("Death", true);

                Destroy(gameObject, 5f);
            }

        }
        else
        {
            miniBossZombieAnimatorControll.SetBool("isIdle", true);
            miniBossZombieAnimatorControll.SetBool("Run", false);
            miniBossZombieAnimatorControll.SetBool("FirstAtt", false);
            

            agent.isStopped = true;
            agent.speed = 0f;

            if (isDead)
            {
                agent.enabled = false;
                agent.isStopped = true;
                agent.speed = 0f;

                miniBossZombieAnimatorControll.SetBool("Run", false);
                miniBossZombieAnimatorControll.SetBool("isIdle", false);
                miniBossZombieAnimatorControll.SetBool("FirstAtt", false);
               
                miniBossZombieAnimatorControll.SetBool("Death", true);

                Destroy(gameObject, 5f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RayBullet")
        {
            smallZombieHp -= 10;
            ps.Play(true);
            Destroy(other.gameObject);

            if (!isDead)
            {
                if (smallZombieHp <= 0)
                {
                    agent.enabled = false;
                    isDead = true;
                    agent.isStopped = true;
                    agent.speed = 0f;

                    miniBossZombieAnimatorControll.SetBool("Run", false);
                    miniBossZombieAnimatorControll.SetBool("isIdle", false);
                    miniBossZombieAnimatorControll.SetBool("FirstAtt", false);
                    miniBossZombieAnimatorControll.SetBool("SecondAtt", false);
                    miniBossZombieAnimatorControll.SetBool("Death", true);

                    Destroy(gameObject, 5);
                }

            }

        }
    }

   
}
