using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAI : MonoBehaviour
{
    public bool roaming;
    public float moveSpeed;
    public float nextWPDistance;
    bool reachtoDestination;
    public bool updateNewPath;

    public SpriteRenderer enemySR;
    public Seeker seeker;
    Path path;

    //Shoot
    public bool isShootable;
    public GameObject bullet;
    public float timeBtwGun;
    public float speedGun;
    private float fireCoolDown;

    Coroutine moveCoroutine;

    private void Start()
    {
        reachtoDestination=true;

        InvokeRepeating("CalculatePath",0f,0.5f);
    }

    private void Update()
    {
        fireCoolDown -= Time.deltaTime;

        if (fireCoolDown < 0 )
        {
            fireCoolDown = timeBtwGun;
            if (isShootable) 
                EnemyGun();
        }
    }

    void EnemyGun()
    {
        var bulletTemp = Instantiate(bullet, transform.position, Quaternion.identity);

        Rigidbody2D enemy_rb = bulletTemp.GetComponent<Rigidbody2D>();
        Vector3 playerPos = FindObjectOfType<Player>().transform.position;
        Vector3 direction = playerPos - transform.position;
        enemy_rb.AddForce(direction.normalized * speedGun, ForceMode2D.Impulse);

    }

    Vector2 FindTarget()
    {
        Vector3 playerPos = FindObjectOfType<Player>().transform.position;
        if (roaming)
        {
            return (Vector2) playerPos + Random.Range(10f, 20f)* new Vector2(Random.Range(-1,1), Random.Range(-1,1)).normalized;
        }
        else
        {
            return playerPos; 
        }
    }

    void CalculatePath()
    {
        Vector2 target = FindTarget();
        //Debug.Log(roaming + " " + (reachtoDestination || updateNewPath));
        if (seeker.IsDone() && (reachtoDestination || updateNewPath))
        {
            seeker.StartPath(transform.position, target, OnPathCallBack);
        }
    }

    void OnPathCallBack(Path p)
    {
        if (p.error) return;

        path = p;

        MoveToTarget();
    }

    void MoveToTarget()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int curWP = 0;
        reachtoDestination = false;
        while (curWP < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[curWP] - (Vector2)transform.position).normalized;
            Vector3 force = direction * moveSpeed * Time.deltaTime;
            transform.position += force;

            float distance = Vector2.Distance(transform.position, path.vectorPath[curWP]);
            if (distance < nextWPDistance)
            {
                curWP++;
            }

            if (force.x != 0)
            {
                if (force.x < 0)
                {
                    enemySR.transform.localScale = new Vector3(-1, 1, 0);
                }
                else
                {
                    enemySR.transform.localScale = new Vector3(1, 1, 0);
                }
            }

            yield return null;
        }

        reachtoDestination = true;
    }
}

