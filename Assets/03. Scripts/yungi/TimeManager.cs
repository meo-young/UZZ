using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using System.Data;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public LightColorController lightColorController;
    public string url = "https://www.google.com/";

    [SerializeField]
    TMP_Text dateText;
    [SerializeField] TMP_Text dateText_2;
    [SerializeField] TMP_Text dataText_3;

    DateTime dateTime;
    bool isPaused = false;

    void Start()
    {
        StartCoroutine(WebChk());
    }
    IEnumerator WebChk()
    {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string date = request.GetResponseHeader("date"); //이곳에서 반송된 데이터에 시간 데이터가 존재
                Debug.Log("받아온 시간" + date); // GMT로 받아온다.
                dateTime = DateTime.Parse(date).ToLocalTime(); // ToLocalTime() 메소드로 한국시간으로 변환시켜 준다.
                Debug.Log("한국시간으로변환" + dateTime);
                int year = dateTime.Year;
                int month = dateTime.Month;
                int day = dateTime.Day;
                int hour = dateTime.Hour;
                int minute = dateTime.Minute;
                int second = dateTime.Second;
                Debug.Log("Year: " + year);
                Debug.Log("Month: " + month);
                Debug.Log("Day: " + day);
                
                Debug.Log("Hour: " + hour);
                Debug.Log("Minute: " + minute);
                Debug.Log("Second: " + second);

                lightColorController.time = NormalizeTime(hour, minute, second);

                //dateText.text = year.ToString() + month.ToString() + day.ToString();
                //dateText_2.text = year.ToString() + month.ToString() + day.ToString();


                //dateText.text = dateTime.ToString("MM") + "/" + dateTime.ToString("dd hh:mm ss tt");
                //dateText_2.text = dateTime.ToString("MM") + "/" + dateTime.ToString("dd hh:mm ss tt");
                //dataText_3.text = dateTime.ToString("MM") + "/" + dateTime.ToString("dd hh:mm ss tt");
            }
        }
    }

    private void FixedUpdate()
    {
        
        dateTime=dateTime.AddSeconds(Time.deltaTime);
        //dateText.text = dateTime.ToString("MM") + "/" + dateTime.ToString("dd hh:mm ss tt");
       // dateText_2.text = dateTime.ToString("MM") + "/" + dateTime.ToString("dd hh:mm ss tt");
       // dataText_3.text = dateTime.ToString("MM") + "/" + dateTime.ToString("dd hh:mm ss tt");
    }

    
    float NormalizeTime(int hour, int minute, int second)
    {
        // 총 시간인 24시간으로 나누어 정규화
        return (hour + (minute / 60f) + (second / 3600f)) / 24f;
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            isPaused = true;

        }
        else
        {
            if (isPaused)
            {
                isPaused = false;
                StartCoroutine(WebChk());
            }
        }
    }
}