using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BootTimer : MonoBehaviour
{
    private float _timerMax = 120;

    private float _timer = 0;
    // Start is called before the first frame update
    void Awake()
    {
        _timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _timerMax)
        {
            Process.Start("/Users/2290-avpb/Desktop/GameLauncher.app");
            Application.Quit();
            Debug.Log("Closing Game");
        }
    }
}
