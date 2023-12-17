using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemy;
    public float spawnTime;
    public Camera mainCamera;
    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("EnemySpawner", 0f, spawnTime);
    }

    private void EnemySpawner()
    {
        int enemyPosIdx = Random.Range(0, 4);
        switch (enemyPosIdx)
        {
            case 0:
                pos = new Vector2(0, 0);
                break;
            case 1:
                pos = new Vector2(Screen.width, Screen.height);
                break;
            case 2:
                pos = new Vector2(0, Screen.height);
                break;
            case 3:
                pos = new Vector2(Screen.width, 0);
                break;
        }
        Vector3 mousePosition = pos;
        mousePosition.z = 10;
        Vector3 spawnPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        int enemyIdx = Random.Range(0, enemy.Length);
        Instantiate(enemy[enemyIdx], spawnPosition, Quaternion.identity);
    }
}
