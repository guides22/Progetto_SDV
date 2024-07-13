using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeloxPill : MonoBehaviour
{
    private Transform player;
    private PlayerMovement playerMovement;

    private void Start() {
        // Trova il GameObject del player usando il tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = player.GetComponent<PlayerMovement>();
    }
    
    public void UseVeloxPill() {
        if (playerMovement!= null) {
            float currentSpeed = playerMovement.GetCurrentSpeed();
            if (currentSpeed >= 6f) {
                return;
            }
            else {
                playerMovement.SetCurrentSpeed(currentSpeed+3f);
                FindObjectOfType<AudioManager>().Play("EatPill");
                Destroy(gameObject);   
            }
        }
    }
}