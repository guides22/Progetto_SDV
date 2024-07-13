using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private int maxHearts = 4;  // Numero massimo di cuori del giocatore
    private int currentHearts;  // Cuori attuali del giocatore
    private bool isInvincible = false;  // Indica se il giocatore è invincibile

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;                    

    void Start() {
        currentHearts = maxHearts;  // All'avvio, il giocatore ha il numero massimo di cuori
    }

    void Update() {
        foreach(Image img in hearts) {
            img.sprite = emptyHeart;
        }
        for (int i = 0; i < currentHearts; i++) {
            hearts[i].sprite = fullHeart;
        }
    }

    IEnumerator GetHurt() {
        GetComponent<Animator>().SetLayerWeight(1, 1);
        yield return new WaitForSeconds(3);
        GetComponent<Animator>().SetLayerWeight(1, 0);
    }

    // Metodo per prendere danno
    public void TakeDamage(int damage) {
        if(!IsInvincible()) {
            if (currentHearts > 0) {
                currentHearts -= damage;
                FindObjectOfType<AudioManager>().Play("TakeDamage");
                if (currentHearts <= 0) {
                Die();
            }
            StartCoroutine(GetHurt());
            }
        }
    }

    // Metodo chiamato quando il giocatore muore
    private void Die() {
        Time.timeScale = 0;
        SceneManager.LoadScene("Game Over");
    }

    public int GetCurrentHealth() {
        return currentHearts;
    }

    public void SetCurrentHealth(int currentHealth) {
        currentHearts = currentHealth;
    }

    public int GetMaxHealth() {
        return maxHearts;
    }

    public void MakeInvincible(int duration) {
        StartCoroutine(InvincibilityCoroutine(duration));
    }

    private IEnumerator InvincibilityCoroutine(int duration) {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }

    public bool IsInvincible() {
        return isInvincible;
    }
}

