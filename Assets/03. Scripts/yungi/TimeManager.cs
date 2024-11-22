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
                string date = request.GetResponseHeader("date"); //�̰����� �ݼ۵� �����Ϳ� �ð� �����Ͱ� ����
                Debug.Log("�޾ƿ� �ð�" + date); // GMT�� �޾ƿ´�.
                dateTime = DateTime.Parse(date).ToLocalTime(); // ToLocalTime() �޼ҵ�� �ѱ��ð����� ��ȯ���� �ش�.
                Debug.Log("�ѱ��ð����κ�ȯ" + dateTime);
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
        // �� �ð��� 24�ð����� ������ ����ȭ
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