using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer character;
    public float moveSpeed = 5f;
    public Vector3 moveInput;

    //Dash
    public float dashBoost;
    public float dashTime;
    private float m_dashTime;
    private bool isDash = false;

    //Ghost Effect
    public GameObject ghostEffect;
    public float ghostDelayTime;
    private Coroutine dashGhostEffect;

    private Rigidbody2D m_rb;
    public Animator m_ani;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>(); 
        //m_ani = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        transform.position += moveInput*moveSpeed*Time.deltaTime;
        
        m_ani.SetFloat("Speed", Mathf.Abs(moveInput.x) + Mathf.Abs(moveInput.y));

        if (Input.GetMouseButtonDown(1) && m_dashTime <= 0)
        {
            if (AudioController.Ins)
            {
                AudioController.Ins.PlayRollSound();
            }
            moveSpeed += dashBoost;
            m_dashTime = dashTime;
            isDash = true;
            StartDashEffect();
            //m_ani.SetBool("Roll", true);
        }

        if (m_dashTime <= 0 && isDash)
        {
            moveSpeed -= dashBoost;
            isDash = false;
            StopDashEffect();
            //m_ani.SetBool("Roll", false);
        }
        else
        {
            m_dashTime -= Time.deltaTime;
        }

        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
            {
                character.transform.localScale = new Vector3(1, 1, 0);
            }
            else
            {
                character.transform.localScale = new Vector3(-1, 1, 0);
            }
        }
    }

    void StopDashEffect()
    {
        StopCoroutine(dashGhostEffect);
    }

    void StartDashEffect()
    {
        if (dashGhostEffect != null)
        {
            StopCoroutine(dashGhostEffect);
        }

        dashGhostEffect = StartCoroutine(GhostEffectCoroutine());
    }

    IEnumerator GhostEffectCoroutine()
    {
        while (true)
        {
            GameObject ghost = Instantiate(ghostEffect, transform.position, transform.rotation);
            Sprite currentSprite = character.sprite;
            ghost.GetComponentInChildren<SpriteRenderer>().sprite = currentSprite;
            Destroy(ghost,0.5f);
            yield return new WaitForSeconds(ghostDelayTime);
        }
    }
}
