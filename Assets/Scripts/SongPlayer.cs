using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Attach this component to objects that you want to keep alive (e.g. theme songs) in certain scene transitions. 
/// For reusability, this component uses the scene names as strings to decide whether it survives or not after a scene is loaded 
/// </summary>
public class SongPlayer : MonoBehaviour
{
    
    public List<string> sceneNames;
    public string instanceName;
    
    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        // subscribe to the scene load callback
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // delete any potential duplicates that might be in the scene already, keeping only this one 
        CheckForDuplicateInstances();

        // check if this object should be deleted based on the input scene names given 
        CheckIfSceneInList();
    }

    void CheckForDuplicateInstances()
    {
        // cache all objects containing this component
        SongPlayer[] collection = FindObjectsOfType<SongPlayer>();

        // iterate through the objects with this component, deleting those with matching identifiers
        foreach (SongPlayer obj in collection)
        {
            if(obj != this) // avoid deleting the object running this check
            {
                if (obj.instanceName == instanceName)
                {
                    Debug.Log("Duplicate object in loaded scene, deleting now...");
                    DestroyImmediate(obj.gameObject);
                }
            }
        }
    }

    void CheckIfSceneInList()
    {
        // check what scene we are in and compare it to the list of strings 
        string currentScene = SceneManager.GetActiveScene().name;

        if (sceneNames.Contains(currentScene))
        {
            // keep the object alive 
        }
        else
        {
            // unsubscribe to the scene load callback
            SceneManager.sceneLoaded -= OnSceneLoaded;

            DestroyImmediate(this.gameObject);
        }
    }
}