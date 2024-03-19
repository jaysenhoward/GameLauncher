using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
   public Animator transition;
   public float transitionTime = 1f;
   public void LoadNextLevel(string sceneName)
   {
      StartCoroutine(LoadLevel(sceneName));
   }
   public void LoadNextLevel(int sceneNum)
   {
      StartCoroutine(LoadLevel(sceneNum));
   }

   IEnumerator LoadLevel(string levelName)
   {
      transition.SetTrigger("Start");
      yield return new WaitForSeconds(transitionTime);
      SceneManager.LoadScene(levelName);
   } 
   IEnumerator LoadLevel(int levelNum)
   {
      transition.SetTrigger("Start");
      yield return new WaitForSeconds(transitionTime);
      SceneManager.LoadScene(levelNum);
   } 
}
