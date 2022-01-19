using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CameraDZ : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private AudioClip loseSound;
    public Text winText;
    public Text loseText;
    private bool gameOver = false;

    private void Awake()
    {
    winText.text = "";
    loseText.text = "";
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        player.SetActive(false);
        SoundManager.source.Stop();
        SoundManager.instance.PlaySound(loseSound);
        gameOver = true;
        Countdown.Instance.playerDead = true;
        winText.text = "";
        loseText.text = "You Lose! \n Press R to Restart!";
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