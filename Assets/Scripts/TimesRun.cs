using System;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class TimesRun: MonoBehaviour
{
    private string[] gameNames;
    [HideInInspector]public int[] gamePlays;
    public string path;
    private void Awake()
    {
        string[] textFileArray = File.ReadAllLines(path);
        gameNames=new string[textFileArray.Length];
        gamePlays=new int[textFileArray.Length];;
        for (int i = 0; i < textFileArray.Length; i++)
        {
            string[] temp = textFileArray[i].Split(" ");
            gameNames[i] = temp[0];
            gamePlays[i]= Convert.ToInt32(temp[1]);

        }
    }

    

   public void ResetFile()
   {
       string[] names = new string[SceneManager.sceneCountInBuildSettings-1];
       for (int i = 0; i < names.Length; i++)
       {
           string temp = SceneManager.GetSceneAt(i).name;
           names[i] = temp.Replace(" ", String.Empty);
       }
       File.WriteAllText(path, String.Empty);
       StreamWriter writer = new StreamWriter(path, true);
       for (int i = 0; i < names.Length; i++)
       {
           if(i<gamePlays.Length) 
           {
               writer.WriteLine(names[i] + gamePlays[i]);
           }
           else
           {
               writer.WriteLine(names[i] + 0);
           }
       }
       writer.Close();
   }

  
 
}
