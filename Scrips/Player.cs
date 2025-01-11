using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public Animator animator;
    public SpriteRenderer characterSR;
    public SpriteRenderer healthBarSR;
    public Transform zombieTarget; // Tham chiếu đến zombie để đối mặt
    public GameObject bulletPrefab; // Prefab của đạn
    public Transform bulletSpawnPoint; // Điểm xuất phát của đạn
    public float bulletSpeed ; // Tốc độ của đạn
    private Vector3 moveInput;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
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
        HandleMovement();
        // Luôn quay mặt về phía zombie
        if (zombieTarget != null)
        {
            FaceZombie(zombieTarget);
        }
        // Xử lý bắn đạn
        if (Input.GetKeyDown(KeyCode.Space)) // Nhấn phím Space để bắn
        {
            ShootBullet();
        }
    }

    private void HandleMovement()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        transform.position += moveInput * moveSpeed * Time.deltaTime;
        animator.SetFloat("Speed", moveInput.sqrMagnitude);
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
}
