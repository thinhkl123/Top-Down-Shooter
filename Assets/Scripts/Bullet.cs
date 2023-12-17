using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (AudioController.Ins)
            {
                AudioController.Ins.PlayExploSound();
            }
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.37f);
            Destroy(gameObject);
            if (GUIManager.Ins)
            {
                GUIManager.Ins.UpdateScore();
            }
        }
    }
}
