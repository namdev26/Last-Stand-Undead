using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int minDamage ;
    public int maxDamage ;
    public bool myBullet;
    PlayerHealth enemyHealth ;

    private void Start()
    {
        enemyHealth = GetComponent<PlayerHealth>();
    }
    public void TakeDamage(int damage){
        enemyHealth.TakeDam(damage);
        //Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player") &&  !myBullet){
            int damage = Random.Range(minDamage, maxDamage);
            collision.GetComponent<Player>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        if (collision.CompareTag("Enemy") &&  myBullet){
            int damage = Random.Range(minDamage, maxDamage);
            collision.GetComponent<Player>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
    
}
