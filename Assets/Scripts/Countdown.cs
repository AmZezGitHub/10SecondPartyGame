using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 10f;
    public GameObject player;
    [Header ("Text")]
    [SerializeField] private Text winText;
    [SerializeField] private Text loseText;
    [SerializeField] Text countdownText;
    [SerializeField] private AudioClip loseSound;
    private bool gameOver = false;
    public static Countdown Instance;
    public bool playerDead = false;


    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        currentTime = startingTime;
        winText.text = "";
        loseText.text = "";
    }

    void Update()
    {
        if (playerDead == false)
        {
    currentTime -=1 * Time.deltaTime;
    countdownText.text = currentTime.ToString ("0");
        if (currentTime <= 0)
            {
                currentTime = 0;
                player.SetActive(false);
                winText.text = "";
                SoundManager.source.Stop();
                SoundManager.instance.PlaySound(loseSound);
                gameOver = true;
                playerDead = true;
                loseText.text = "You Lose to Time! \n Press R to Restart!";
            }
        }
        
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
