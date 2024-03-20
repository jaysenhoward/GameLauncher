using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


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

    void Awake()
    {
        //Set Input System
        UIControls = new UI_Input();
        sceneCount =SceneManager.sceneCountInBuildSettings;
    }

   


    private void Up(InputAction.CallbackContext context)
    {
        //Show the next game in the launcher. If we are at the last game in the launcher return to Project Rock
        if (SceneManager.GetActiveScene().buildIndex == sceneCount - 1)
        {
            levelLoader.LoadNextLevel(0);
            //SceneManager.LoadScene(0);
        }
        else
        {
            levelLoader.LoadNextLevel(SceneManager.GetActiveScene().buildIndex+1);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    private void Down(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            levelLoader.LoadNextLevel(sceneCount-1);
            //SceneManager.LoadScene(sceneCount-1);
        }
        else
        {
            levelLoader.LoadNextLevel(SceneManager.GetActiveScene().buildIndex-1);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
    private void Play(InputAction.CallbackContext context)
    {
        //Launch Game
        //Process.Start(path);
        //need file location
        //Application.dataPath + /../.... (depending on how files are organized)
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
