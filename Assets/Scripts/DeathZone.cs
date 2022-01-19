using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private AudioClip winSound;
    public bool gameOver = false;
    public Text winText;


private void Awake()
    {
        winText.text = "";
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        player.SetActive(false);
        SoundManager.source.Stop();
        SoundManager.instance.PlaySound(winSound);
        gameOver = true;
        Countdown.Instance.playerDead = true;
        winText.text = "You Won! \n Press R to Restart, \n Or ESC to Exit the Game! \n \n Game Created by \nChristopher Loso";
    }

void FixedUpdate()
        {
         if (Input.GetKey(KeyCode.R))
            {
            if (gameOver == true)
                {
              SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        if (Input.GetKeyDown(KeyCode.Escape)) 
                {
                Application.Quit();
                }
        }
}