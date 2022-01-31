using System;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 0;

    public TMP_Text textHealth = null;

    private bool _isAlive = false;

    public GameObject gameOver = null;
    
    private void Start()
    {
        health = 3;
        _isAlive = true;
        gameOver.SetActive(false);
    }

    private void Update()
    {
        textHealth.text = "Lives: " + health;
        
        if (health == 0)
        {
            Time.timeScale = 0;
            _isAlive = false;
        }

        if (!_isAlive)
        {
            gameOver.SetActive(true);
        }
    }
    
}
