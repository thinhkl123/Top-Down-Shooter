using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar;
    public float maxHealth = 100;
    public SpriteRenderer character;
    float currentHealth;
    Animator m_ani;
    Collider playerCollider;
    int countDie = 0;

    public GameObject gameOverDialog;
    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<Collider>();
        m_ani = character.GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <=0)
        {
            countDie++;
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy_Bullet"))
        {
            EnemyDamage enemyTemp = collision.GetComponent<EnemyDamage>();
            Destroy(collision.gameObject);
            TakeDamage(enemyTemp.damage);
        }

        if (collision.CompareTag("Blood"))
        {
            Heal();
            Destroy(collision.gameObject);
        }
    }
    

    public void TakeDamage(float damage)
    {
        if (AudioController.Ins)
        {
            AudioController.Ins.PlayTakeDameSound();
        }
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        m_ani.SetTrigger("Hurt");
    }

    void Heal()
    {
        if (AudioController.Ins)
        {
            AudioController.Ins.PlayRollSound();
        }
        currentHealth += 10f;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    private void Die()
    {
        m_ani.SetTrigger("Die");
        //playerCollider.enabled = false;
        if (countDie == 1)
        {
            Invoke("SetActiveDiaLog", 1f);
        }
    }

    private void SetActiveDiaLog()
    {
        Time.timeScale = 0f;
        if (AudioController.Ins)
        {
            AudioController.Ins.PlayFinishSound();
        }
        if (gameOverDialog)
        {
            gameOverDialog.SetActive(true);
        }
    }
}
