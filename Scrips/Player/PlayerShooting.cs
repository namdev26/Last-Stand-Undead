// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// using UnityEngine;

// public class PlayerShooting : MonoBehaviour
// {
//     [SerializeField] private GameObject bulletPrefab;
//     [SerializeField] private Transform bulletSpawnPoint;
//     [SerializeField] private float bulletSpeed;
//     private Transform target;

//     private void Start()
//     {
//         // Tìm mục tiêu (Zombie)
//         ZombieAI zombie = FindObjectOfType<ZombieAI>();
//         if (zombie != null)
//         {
//             target = zombie.transform;
//         }
//     }

//     public void HandleShooting()
//     {
//         if (Input.GetKeyDown(KeyCode.Space))
//         {
//             ShootBullet();
//         }
//     }

//     private void ShootBullet()
//     {
//         if (target == null) return;
//         GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
//         Vector3 direction = (target.position - bulletSpawnPoint.position).normalized;
//         Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
//         if (rb != null)
//         {
//             rb.velocity = direction * bulletSpeed;
//         }
//         Destroy(bullet, 5f);
//     }
// }
