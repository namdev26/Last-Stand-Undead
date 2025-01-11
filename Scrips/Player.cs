using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    public SpriteRenderer characterSR;
    public SpriteRenderer healthBarSR;
    public Transform zombieTarget; // Tham chiếu đến zombie để đối mặt

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
        else
        {
            Debug.LogWarning("ZombieAI not found in the scene!");
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
}