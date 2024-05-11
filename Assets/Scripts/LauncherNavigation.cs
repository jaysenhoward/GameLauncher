using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
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
    public Text nextLevel;
    public Text previousLevel;
    public TextMeshProUGUI plays;

    private int sceneNumber;
    private string[] scenes;


    void Awake()
    {
        //Set Input System
        UIControls = new UI_Input();
       
            string gameName = SceneManager.GetActiveScene().name;
            plays.text = "Plays: " + PlayerPrefs.GetInt(gameName);
        
    
        sceneNumber= SceneManager.sceneCountInBuildSettings;
        scenes= new string[sceneNumber];
        for (int i = 0; i < sceneNumber; i++)
        {
            scenes[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
        }

    }

    private void Start()
    {
        sceneCount = SceneManager.sceneCountInBuildSettings;
        Debug.Log(sceneCount);
        if (SceneManager.GetActiveScene().name == scenes[scenes.Length-2])
        {
            nextLevel.text = "To " + scenes[0];
        }
        else
        {
            nextLevel.text = "To " + scenes[SceneManager.GetActiveScene().buildIndex + 1];//SceneManager.GetActiveScene().buildIndex+1).name;
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            previousLevel.text = "To " + scenes[scenes.Length-2];
        }
        else
        {
            previousLevel.text = "To "+ scenes[SceneManager.GetActiveScene().buildIndex - 1];
        }
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
            plays.text = "Plays: " + PlayerPrefs.GetInt(gameName);
      
        
        //Process.Start(tempPath);
        Process.Start(path);
        
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
