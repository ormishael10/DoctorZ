using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    float maxHealth;
    public float currentHealth;
    public HealthBar healthBar;
    public static bool insidePotion;
    public AudioClip potion;
    public AudioSource potionAudio;


    void Start()
    {
        insidePotion = false;
        maxHealth = 150f;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

   
    void Update()
    {
       
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Cursor.visible = Cursor.visible;
            SceneManager.LoadScene("YouDied");
        }
    }


    public void Heal(float amountOfHeal)
    {
        currentHealth += amountOfHeal;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "FatZombieAtt")
        {
            TakeDamage(25);
            healthBar.SetHealth(currentHealth);
        }

        if (other.gameObject.tag == "SmallZombieAtt")
        {
            TakeDamage(5);
            healthBar.SetHealth(currentHealth);
        }

        if (other.gameObject.tag == "MiniBossAtt")
        {
            TakeDamage(35);
            healthBar.SetHealth(currentHealth);
        }

        if(other.gameObject.tag == "Potion")
        {
            potionAudio.PlayOneShot(potion);
            Heal(70);
            healthBar.SetHealth(currentHealth);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Spikes")
        {
            SceneManager.LoadScene("StartMenu");
        }

        if(other.gameObject.tag == "BossIceSword")
        {
            TakeDamage(80);
            healthBar.SetHealth(currentHealth);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "FireBall")
        {
            print("Oved");
            TakeDamage(1);
            healthBar.SetHealth(currentHealth);
        }
    }
}
