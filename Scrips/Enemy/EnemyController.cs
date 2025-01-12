using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    Player playerS;
    public int minDamage, maxDamage;
    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            playerS = collision.GetComponent<Player>();
            InvokeRepeating("DamagePlayer", 0.1f, 0.1f);
        }
    }

    public void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            playerS = null;
            CancelInvoke("DamagePlayer");
        }
    }

    public void DamagePlayer() {
        int damage = UnityEngine.Random.Range(minDamage, maxDamage);
        //Debug.Log("DamagePlayer" + damage);
        playerS.TakeDamage(damage);
    }
}
