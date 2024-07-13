using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPillMax : MonoBehaviour
{
    private Transform player;
    private PlayerHealth playerHealth;

    private void Start() {
        // Trova il GameObject del player usando il tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
    }
    
    public void UseHealthPillMax() {
        if (playerHealth!= null) {
            int currentHealth = playerHealth.GetCurrentHealth();
            int maxHearts = playerHealth.GetMaxHealth();
            if (currentHealth >= maxHearts) {
                return;
            }
            else {
                playerHealth.SetCurrentHealth(maxHearts);
                FindObjectOfType<AudioManager>().Play("EatPill");
                Destroy(gameObject);   
            }
        }
    }
}