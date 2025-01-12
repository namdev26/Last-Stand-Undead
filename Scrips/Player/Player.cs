using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour //,IDamageable
{
    public float moveSpeed;
    //public Animator animator;
    public SpriteRenderer characterSR;
    public SpriteRenderer healthBarSR;
    public Transform zombieTarget;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed;
    public Animator animator;
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;

    private void Start()
    {
        // Tìm đối tượng Zombie trong cảnh
        ZombieAI zombie = FindObjectOfType<ZombieAI>();
        if (zombie != null)
        {
            zombieTarget = zombie.transform;
        }
    }

    private void Update()
    {
        // Xử lý di chuyển
        playerMovement.HandleMoveMovement();
        // Luôn quay mặt về phía zombie
        if (zombieTarget != null)
        {
            FaceZombie(zombieTarget);
        }
    }
    public void TakeDamage(int damage)
    {
        playerHealth.TakeDam(damage);  // Gọi phương thức TakeDam của PlayerHealth để giảm máu

        // Gọi animation Hurt khi bị đánh
        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }

        // Nếu máu <= 0, nhân vật chết
        if (playerHealth.currentHealth <= 0)
        {
            if (animator != null)
            {
                animator.SetTrigger("Die");  // Gọi animation Die khi chết
            }
        }
    }
    private void FaceZombie(Transform zombieTransform)
    {
        Vector3 direction = zombieTransform.position - transform.position;
        // Xoay mặt trái/phải dựa trên hướng của zombie
        if (Mathf.Abs(direction.x) > 0.01f)
        {
            characterSR.transform.localScale = new Vector3(
                Mathf.Sign(direction.x) * Mathf.Abs(characterSR.transform.localScale.x),
                characterSR.transform.localScale.y,
                characterSR.transform.localScale.z
            );
        }
    }

    private void ShootBullet()
    {
        if (zombieTarget == null) return;
        // Tạo đạn tại vị trí spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        // Tính toán hướng từ nhân vật đến zombie
        Vector3 direction = (zombieTarget.position - bulletSpawnPoint.position).normalized;
        // Gắn vận tốc cho đạn
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
        }
        // Tùy chọn: Hủy đạn sau một thời gian
        Destroy(bullet, 5f);
    }

    // Triển khai phương thức từ IDamageable
    // public void TakeDamage(int damage)
    // {
    //     playerHealth.TakeDam(damage);  // Gọi phương thức TakeDam của PlayerHealth để giảm máu
    // }
}
