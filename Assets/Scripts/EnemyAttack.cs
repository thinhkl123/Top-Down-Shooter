using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Animator enemy_ani;
    public EnemyDamage eneny;
    PlayerHealth playerHealth;

    private void Start()
    {
        //enemy_ani = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth = collision.GetComponent<PlayerHealth>();
            InvokeRepeating("DamagePlayer", 0f, 1f);
            enemy_ani.SetTrigger("Hit");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth = null;
            CancelInvoke("DamagePlayer");
            //enemy_ani.SetBool("Hit", false) ;
        }
    }

    private void DamagePlayer()
    {
        if (playerHealth)
        {
            playerHealth.TakeDamage(eneny.damage);
        }
    }
}
