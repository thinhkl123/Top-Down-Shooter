using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    public GameObject heart;
    public Camera mainCamera;
    public float spawnTime;
    private void Start()
    {
        InvokeRepeating("SpawnHeart", 20f, spawnTime);
    }

    void SpawnHeart()
    {
        Vector2 pos = new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
        Vector3 mouseSpawnPos = pos;
        mouseSpawnPos.z = 10;
        Vector3 spawnPos = mainCamera.ScreenToWorldPoint(mouseSpawnPos);
        Instantiate(heart, spawnPos, Quaternion.identity);
    }
}
