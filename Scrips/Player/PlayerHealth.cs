using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;  // Sức khỏe tối đa của nhân vật
    [SerializeField] private float safeTime;  // Thời gian bảo vệ sau khi bị sát thương
    [SerializeField] private HealthBar healthBar;  // Thanh sức khỏe hiển thị trên màn hình
    [SerializeField] private UnityEvent onDeath;  // Sự kiện khi nhân vật chết

    public int currentHealth;  // Biến lưu trữ sức khỏe hiện tại
    private float safeTimeCooldown;  // Biến thời gian cooldown sau khi bị sát thương
    private Animator animator;  // Biến lưu trữ Animator để điều khiển các animation

    private void Start()
    {
        // Khởi tạo giá trị ban đầu cho sức khỏe
        currentHealth = maxHealth;  // Gán sức khỏe hiện tại bằng sức khỏe tối đa
        healthBar.UpdateHealth(currentHealth, maxHealth);  // Cập nhật thanh sức khỏe
        animator = GetComponent<Animator>();  // Lấy Animator từ GameObject
    }

    private void OnEnable()
    {
        onDeath.RemoveListener(Death);  // Đảm bảo chỉ có một listener tại một thời điểm
    }

    // Nhận sát thương và xử lý giảm sức khỏe
    public void TakeDam(int damage)
{
    if (safeTimeCooldown <= 0)
    {
        animator.SetLayerWeight(1, 1);  // Ưu tiên layer Hurt
        animator.SetTrigger("Hurt");

        Invoke(nameof(ResetLayerWeight), 0.5f);  // Giảm layer weight sau animation
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
        }
        healthBar.UpdateHealth(currentHealth, maxHealth);
        safeTimeCooldown = safeTime;
    }
}

private void ResetLayerWeight()
{
    animator.SetLayerWeight(1, 0);  // Đưa layer Hurt về trạng thái bình thường
}


private void ResetToIdle()
{
    animator.SetTrigger("Idle");  // Chuyển về trạng thái Idle
    animator.ResetTrigger("Hurt");  // Đảm bảo reset trigger Hurt
}


    // Xử lý khi nhân vật chết
    private void Death()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die");  // Gọi animation "Die" (đảm bảo rằng animator có trigger "Die")
            // Hủy đối tượng sau khi animation kết thúc (ví dụ: giả sử animation "Die" dài 2 giây)
            Destroy(gameObject, 2f);
            //Destroy(Weapon_1);
              // Thời gian này cần phải khớp với thời gian dài của animation
        }
        else
        {
            Debug.LogWarning("Animator not found!");
        }
    }

    private void Update()
    {
        // Giảm thời gian bảo vệ
        safeTimeCooldown -= Time.deltaTime;
    }
}
