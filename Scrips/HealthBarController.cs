using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    public Sprite[] listHealthBar; // Mảng chứa 7 hình ảnh thanh máu

    private SpriteRenderer spriteRenderer;
    private int index = 0; // Bắt đầu từ hình ảnh đầu tiên
    private Transform characterTransform;
    private Vector3 offset; // Lưu khoảng cách giữa thanh máu và nhân vật

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (listHealthBar.Length > 0)
        {
            spriteRenderer.sprite = listHealthBar[index]; // Gán hình ảnh đầu tiên khi bắt đầu
        }

        // Gán nhân vật (cha của thanh máu)
        
    }

    void Update()
    {
        // Cập nhật vị trí và giữ góc quay thanh máu không thay đổi
        

        // Cập nhật hình ảnh thanh máu theo các phím bấm
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (index < listHealthBar.Length - 1)
            {
                index++; // Tăng chỉ số
                spriteRenderer.sprite = listHealthBar[index]; // Cập nhật hình ảnh
            }
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            if (index > 0)
            {
                index--; // Giảm chỉ số
                spriteRenderer.sprite = listHealthBar[index]; // Cập nhật hình ảnh
            }
        }
    }

    
}
