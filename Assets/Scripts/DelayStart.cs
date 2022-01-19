using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayStart : MonoBehaviour
{
    public GameObject countDown;
    public GameObject infoTip;
    //private AudioSource source;
    [SerializeField] private AudioClip introSound;
    [SerializeField] private AudioClip backSound;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine ("StartDelay");
        SoundManager.instance.PlaySound(introSound);
    }

    IEnumerator StartDelay ()
    {
        Time.timeScale = 0;
        float pauseTime = Time.realtimeSinceStartup + 3f;
        while (Time.realtimeSinceStartup < pauseTime)
            yield return 0;
        SoundManager.instance.PlaySound(backSound);
        countDown.gameObject.SetActive (false);
        infoTip.gameObject.SetActive (false);
        Time.timeScale = 1;
    }
}
