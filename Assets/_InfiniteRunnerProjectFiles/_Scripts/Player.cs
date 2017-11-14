using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : GameElement{
    public TextMeshProUGUI healthText;
    public int health = 100;

    private void Awake()
    {
        healthText.text = "Health: " + health;  
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            healthText.text = "Health: " + health;
        }
    }
}
