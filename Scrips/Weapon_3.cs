using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_3 : MonoBehaviour
{
    public GameObject bullet; // Prefab viên đạn
    public Transform[] firePos; // Danh sách vị trí bắn
    public GameObject muzzle; // Hiệu ứng bắn
    public GameObject fireEffect; // Hiệu ứng nổ khi bắn

    public float TimeBtwFire = 0.2f; // Thời gian giữa các phát bắn
    public float bulletForce; // Lực đẩy của viên đạn

    private float timeBtwFire;

    void Update()
    {
        FollowPlayer();
        RotateGun();
        timeBtwFire -= Time.deltaTime;
        if (Input.GetMouseButton(0) && timeBtwFire < 0)
        {
            FireBullet();
        }
    }
    void FollowPlayer()
    {   Vector3 offset = new Vector3(-1.6f,0, 0);
        if (transform.parent != null)
        {
            transform.position = transform.parent.position + offset;
        }
    }
    void RotateGun()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
            transform.localScale = new Vector3(1, -1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }
    void FireBullet()
    {
        foreach (Transform fire in firePos)
        {
            timeBtwFire = TimeBtwFire;

            GameObject bulletTmp = Instantiate(bullet, fire.position, Quaternion.identity);
            Instantiate(muzzle, fire.position, transform.rotation, transform);
            Instantiate(fireEffect, fire.position, transform.rotation, transform);

            Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
        }
    }
}
