using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float[] _timesArr;
    
    private DateTime time;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        time = DateTime.Now;
        for (int i = 0; i < _timesArr.Length - 1; i++)
        {
            if (i % 2 == 0)
            {
                if (time.Hour + time.Minute / 60.0f > _timesArr[i] && 
                    time.Hour + time.Minute / 60.0f < _timesArr[i + 1])
                {
                    SceneManager.LoadScene("No Playing Screen");
                }
            }
            else
            {
                if (time.Hour + time.Minute / 60.0f > _timesArr[i] && 
                    time.Hour + time.Minute / 60.0f < _timesArr[i + 1])
                {
                    SceneManager.LoadScene("Project Rock");
                }
            }
        }
    }
}