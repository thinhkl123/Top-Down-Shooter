using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPb;
    public Transform firePos;
    public Transform fireTarget;
    public float TimeBtwGun;
    public float bulletForce;

    private float timeBtwGun;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RotateGun();
        timeBtwGun -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && timeBtwGun < 0)
        {
            if (AudioController.Ins)
            {
                AudioController.Ins.PlayGunSound();
            }
            FireGun();
        }
    }

    void RotateGun()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
        if (transform.eulerAngles.z >= 90 && transform.eulerAngles.z <= 270)
        {
            transform.localScale = new Vector3(1, -1, 0);
        } 
        else
        {
            transform.localScale = new Vector3(1,1,0);
        }
    }

    void FireGun()
    {
        timeBtwGun = TimeBtwGun;
        GameObject bullet = Instantiate(bulletPb, firePos.position, Quaternion.identity);

        Instantiate(fireTarget, firePos.position, transform.rotation, transform);

        Rigidbody2D bullet_rb = bullet.GetComponent<Rigidbody2D>();
        bullet_rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
    }
}
