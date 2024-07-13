using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPill : MonoBehaviour
{
    private Transform player;
    private PlayerHealth playerHealth;

    private void Start() {
        // Trova il GameObject del player usando il tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
    }
    
    public void UseShieldPill() {
        playerHealth.MakeInvincible(4);
        FindObjectOfType<AudioManager>().Play("EatPill");
        Destroy(gameObject);
    }
}
