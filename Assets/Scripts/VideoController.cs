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
    }

    void Awake()
    {
        StartCoroutine(videoDelay());
    }

    void Update()
    {
        currentTime = vidPlayer.time;
        if (currentTime >= time)
        {
            screen.SetActive(false);
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
