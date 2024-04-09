using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer vidPlayer;
    public GameObject screen;
    private double time;
    private double currentTime;
    void Start()
    {
        time = vidPlayer.clip.length;
        Debug.Log(time);
    }

    void Awake()
    {
        StartCoroutine(videoDelay());
    }

    void Update()
    {
        currentTime = vidPlayer.time;
        Debug.Log(currentTime);
        if (currentTime >= time-0.1f)
        {
            screen.SetActive(false);
            Debug.Log("End Vid");
            StartCoroutine(videoDelay());

        }
    }

    IEnumerator videoDelay()
    {
        yield return new WaitForSeconds(7f);
        screen.SetActive(true);
        vidPlayer.Play();
    }
}
