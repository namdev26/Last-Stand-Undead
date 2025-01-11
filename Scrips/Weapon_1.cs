using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_1 : MonoBehaviour
{
    public GameObject bullet;
    public Transform[] firePos;
    public GameObject muzzle;
    public GameObject fireEffect;

    public float TimeBtwFire = 0.2f;
    public float bulletForce;

    private float timeBtwFire;
    private Transform zombieTarget; // Tham chiếu đến zombie

    private void Start()
    {
        // Tìm đối tượng Zombie trong cảnh
        ZombieAI zombie = FindObjectOfType<ZombieAI>();
        if (zombie != null)
        {
            zombieTarget = zombie.transform;
        }
        else
        {
            Debug.LogWarning("ZombieAI not found in the scene!");
        }
    }

    void Update()
    {
        RotateGun();
        timeBtwFire -= Time.deltaTime;
        bool autoFire = true;

        if (autoFire && timeBtwFire < 0)
        {
            FireBullet();
        }
    }

    void RotateGun()
    {
        if (zombieTarget == null) return;

        // Tính hướng từ súng tới zombie
        Vector3 directionToZombie = zombieTarget.position - transform.position;
        float angle = Mathf.Atan2(directionToZombie.y, directionToZombie.x) * Mathf.Rad2Deg;

        // Quay súng về phía zombie
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;

        Vector3 zombieScale = zombieTarget.localScale;
        // Điều chỉnh lật súng để phù hợp hướng
        if (transform.eulerAngles.z >= 90 && transform.eulerAngles.z < 270)
        {
            transform.localScale = new Vector3(-2, -2 ,1);
            //transform.localScale = new Vector3(1 * playerScale.x, -1, 1);
        }
        else
            transform.localScale = new Vector3(2, 2, 1);
    }

    void FireBullet()
    {
        foreach (Transform fire in firePos)
        {
            timeBtwFire = TimeBtwFire;

            GameObject bulletTmp = Instantiate(bullet, fire.position, Quaternion.identity);

            Rigidbody2D rb = bulletTmp.GetComponentInChildren<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(fire.right * bulletForce, ForceMode2D.Impulse);
            }

            if (muzzle != null)
                Instantiate(muzzle, fire.position, fire.rotation, transform);

            if (fireEffect != null)
                Instantiate(fireEffect, fire.position, fire.rotation, transform);
        }
    }
}