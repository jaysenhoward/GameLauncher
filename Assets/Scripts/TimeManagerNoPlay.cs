using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeManagerNoPlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeString;

    private string amPm;
    private string textMinString;
    private string textHourString;
    int count;
    string currScene;
    int goodToGo = 0;
    int textHour;
    int textMin;
    void Start()
    {
        currScene = SceneManager.GetActiveScene().name;
        count = 0;
        DateTime thisDay = DateTime.Now;
    }
    void Update()
    {
        DateTime thisDay = DateTime.Now;
        string thisDayString = thisDay.DayOfWeek.ToString();
        int currHour = thisDay.Hour;
        int currMinute = thisDay.Minute;
        string path = Application.streamingAssetsPath + "/schedule.txt";
        string[] textFileArray = File.ReadAllLines(path);

        for (int i = 0; i < textFileArray.Length; i++)
        {
            string currentLine = textFileArray[i];
            string[] tempLineArray = currentLine.Split('/');

            if (tempLineArray[0] == thisDayString)
            {
                //Debug.Log(tempLineArray[0]);
                string[] tempTime = tempLineArray[1].Split(',');
                for (int j = 0; j < tempTime.Length; j++)
                {
                    //loop through the inside of the array
                    //this gives each time slot
                    string[] eachTime = tempTime[j].Split('-');

                    //this gives the start and end time of each slot
                    //Debug.Log("start time " + eachTime[0] + " end time " + eachTime[1]);
                    //grabbing the hours and minutes from the start time
                    string[] startTimes = eachTime[0].Split(':');
                    string firstHour = startTimes[0];
                    string firstMinute = startTimes[1];
                    //convert to int to compare ranges
                    int firstHourInt = int.Parse(firstHour);
                    int firstMinuteInt = int.Parse(firstMinute);

                    //grabbing the hours and minutes from the end time
                    string[] endTimes = eachTime[1].Split(':');
                    string endHour = endTimes[0];
                    string endMinute = endTimes[1];
                    //convert to int to compare ranges
                    int endHourInt = int.Parse(endHour);
                    int endMinuteInt = int.Parse(endMinute);

                    //if it's between the start and end time, go to the open screen
                    if ((currHour > firstHourInt) && (currHour < endHourInt))
                    {
                        //Debug.Log("OpenScreen....." + currHour + " " + firstHourInt + " " + currMinute + " " + firstMinuteInt);
                        //Debug.Log("OpenScreen....." + currHour + " " + endHourInt + " " + currMinute + " " + endMinuteInt);
                        //Debug.Log("Got here one");
                        NoPlaying();
                        goodToGo++;
                        break;
                    }/*
                    else if(currHour==endHourInt && currMinute<endMinuteInt) {
                        goodToGo++;
                        Debug.Log("Got here two");
                        //break;
                    }*/
                    else if(currHour==firstHourInt && currMinute>=firstMinuteInt && 
                        currHour==endHourInt && currMinute<endMinuteInt) {
                        goodToGo++;
                        //Debug.Log("in the same hour...");
                        //Debug.Log("got here three");
                        NoPlaying();
                        break;
                    }
                    else if(currHour==firstHourInt && currMinute>=firstMinuteInt &&
                        currHour<endHourInt)
                    {
                        //Debug.Log("got here new addition");
                        NoPlaying();
                        goodToGo++;
                        break;
                    }
                    else if(currHour>=firstHourInt && currHour==endHourInt && currMinute<endMinuteInt)
                    {
                        //Debug.Log("got here...");
                        NoPlaying();
                        goodToGo++;
                        break;
                    }
                    /*
                    else if (currHour > endHourInt)
                    {
                        //Debug.Log("Stopping now...");
                        goodToGo = 0;
                        //Debug.Log("got here four");
                    } */
                    else
                    {
                       //Debug.Log("got here five");
                        goodToGo = 0;
                        if (currHour < firstHourInt)
                        {
                            //Debug.Log("got here six");
                            //goodToGo = 0;
                            //if (currMinute < firstMinuteInt)
                            //{
                                //Debug.Log("got here seven");
                                count++;
                                if (count == 1)
                                {
                                    //Debug.Log("THE NEXT TIME IS..." + firstHourInt + ":" + firstMinuteInt);
                                    textHour = firstHourInt;
                                    textMin = firstMinuteInt;
                                    
                                }
                            //}
                        }
                        else if(currHour==firstHourInt)
                        {
                            if(currMinute<firstMinuteInt)
                            {
                                //Debug.Log("got here eight");
                                count++;
                                if (count == 1)
                                {
                                    //Debug.Log("THE NEXT TIME IS..." + firstHourInt + ":" + firstMinuteInt);
                                    textHour = firstHourInt;
                                    textMin = firstMinuteInt;
                                }
                            }
                        }
                    }
                }
            }
        }

        if (textHour > 12)
        {
            textHourString = textHour - 12 + "";
            amPm = "PM";
        }
        else
        {
            textHourString = textHour + "";
            amPm = "AM";
        }

        if (textMin < 10)
        {
            textMinString = "0" + textMin;
        }
        else
        {
            textMinString = textMin + "";
        }

        _timeString.text = "Come Back At " + textHourString + ":" + textMinString + " " + amPm + " to play!";
    }

    void NoPlaying()
    {
        SceneManager.LoadScene("Project Rock");
    }
}