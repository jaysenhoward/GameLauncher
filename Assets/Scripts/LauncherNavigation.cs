using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using Debug = UnityEngine.Debug;


public class LauncherNavigation : MonoBehaviour
{
    private PlayerInput input;
    private int sceneCount;
    private InputAction UpKey;
    private InputAction DownKey;
    private InputAction PlayKey;

    public LevelLoader levelLoader;
    public UI_Input UIControls;
    public string path;
    public string tempPath;
    public Text nextLevel;
    public Text previousLevel;
    public TextMeshProUGUI plays;


    void Awake()
    {
        //Set Input System
        UIControls = new UI_Input();
        string temp = SceneManager.GetActiveScene().name;
        plays.text = "Plays: " + PlayerPrefs.GetInt(temp);


    }

    private void Start()
    {
        sceneCount = SceneManager.sceneCountInBuildSettings;
        Debug.Log(sceneCount);
        /*if (SceneManager.GetActiveScene().buildIndex == sceneCount - 2)
        {
            nextLevel.text = "To " + SceneManager.GetSceneAt(0).name;
        }
        else
        {
            nextLevel.text = "To " + (SceneManager.GetActiveScene().buildIndex + 1.ToString());//SceneManager.GetActiveScene().buildIndex+1).name;
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            previousLevel.text = "To " + SceneManager.GetSceneAt(sceneCount-2).name;
        }
        else
        {
            previousLevel.text = "To "+ (SceneManager.GetSceneAt(SceneManager.GetActiveScene().buildIndex-1).name);
        }*/
    }


    private void Up(InputAction.CallbackContext context)
    {
        //Show the next game in the launcher. If we are at the last game in the launcher return to Project Rock (Default)
        if (SceneManager.GetActiveScene().buildIndex == sceneCount - 2)
        {
            levelLoader.LoadNextLevel(0);
        }
        else
        {
            levelLoader.LoadNextLevel(SceneManager.GetActiveScene().buildIndex+1);
            
        }

    }

    private void Down(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            levelLoader.LoadNextLevel(sceneCount-2);
            
        }
        else
        {
            levelLoader.LoadNextLevel(SceneManager.GetActiveScene().buildIndex-1);
            
        }
    }
    private void Play(InputAction.CallbackContext context)
    {
        //Launch Game
        string gameName = SceneManager.GetActiveScene().name;
        int temp = PlayerPrefs.GetInt(gameName);
        PlayerPrefs.SetInt(gameName,temp+1 );
        Process.Start(tempPath);
        //Process.Start(path);
        plays.text = "Plays: " + PlayerPrefs.GetInt(gameName);
        Application.Quit();
        
    }

    private void OnEnable()
    {
        //Enable the controls and add functions to them for when they are pressed
        UpKey = UIControls.UI.Up;
        UpKey.Enable();
        UpKey.performed += Up;
        DownKey = UIControls.UI.Down;
        DownKey.Enable();
        DownKey.performed += Down;
        PlayKey = UIControls.UI.Play;
        PlayKey.performed += Play;
        PlayKey.Enable();
    }
    
    private void OnDisable()
    {
        //Disable controls. Note: OnEnable and OnDisable are required when using new input system this way
        UpKey.Disable();
        DownKey.Disable();
        PlayKey.Disable();
    }

    
}
