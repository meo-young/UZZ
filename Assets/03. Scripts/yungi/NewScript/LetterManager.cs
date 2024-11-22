using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using static UnityEngine.GraphicsBuffer;



[System.Serializable]
public class Letter
{
    public int index;
    public int id;
    public string letter_Character;
    public string text_Kor;
    public string text_Eng;

   
}



public class LetterManager : MonoBehaviour
{
    [SerializeField]
    GameObject letterPrefab;
    [SerializeField]
    GameObject letterPopupPrefab;

    [SerializeField]
    GameObject panel;
    [SerializeField]
    GameObject letter_new;
    [SerializeField]
    GameObject letterLocker;
    [SerializeField]
    GameObject content;
    [SerializeField]
    GameObject contentLocker;

    [SerializeField]
    GameObject letterInventory;
    [SerializeField]
    GameObject newLetter;

    [SerializeField]
    GameObject letter2_exit_bujtton;



    [SerializeField]
    TMP_Text titleName;

    [SerializeField]
    GameObject chLetter;
    [SerializeField]
    GameObject chLocker;
    [SerializeField]
    Transform[] LockerRoom;

    public UZZ_DataTable UZZ_DataTable;

    public Letter[] letters;



    public List<Letter> letterDatas=new List<Letter>();


    public List<Letter> lockerDatas = new List<Letter>();


    [SerializeField]
    private bool isLetterLoad = true;
    [SerializeField]
    private bool isLockLoad = true;
    public void JsonLetterSave()
    {
        
        FileStream stream = new FileStream(Application.dataPath + "/letter.json", FileMode.Create);

        string jsonData = JsonConvert.SerializeObject(letterDatas, Formatting.Indented);
        Debug.Log(jsonData);
        
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        stream.Write(data, 0, data.Length);
        stream.Close();
 
    }
    public void JsonLockerSave()
    {


        FileStream stream2 = new FileStream(Application.dataPath + "/locker.json", FileMode.Create);

        string jsonData2 = JsonConvert.SerializeObject(lockerDatas, Formatting.Indented);


        byte[] data2 = Encoding.UTF8.GetBytes(jsonData2);
        stream2.Write(data2, 0, data2.Length);
        stream2.Close();
    }

    public void JsonLetterLoad()
    {
        string filePath = Application.dataPath + "/letter.json";
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            letterDatas = JsonConvert.DeserializeObject<List<Letter>>(jsonData);
            if (letterDatas == null)
            {
                isLetterLoad = false;
                return;
            }
            Debug.Log("Loaded letters: " + jsonData);

            if(letterDatas !=null)
            {
                for (int i = 0; i < letterDatas.Count; i++)
                {
                    CreateLetter(letterDatas[i].index-1);

                }
            }
           
        }
        isLetterLoad = false;
    }

    public void JsonLockLoad()
    {
        string filePath = Application.dataPath + "/locker.json";
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            lockerDatas = JsonConvert.DeserializeObject<List<Letter>>(jsonData);
            if (lockerDatas == null)
            {
                isLockLoad = false;
                return;
            }
            Debug.Log("Loaded letters: " + jsonData);

            if (lockerDatas != null)
            {
                for (int i = 0; i < lockerDatas.Count; i++)
                {
              
                    CreateLockLetter(lockerDatas[i].index);

                }
            }

        }
        isLockLoad = false;
    }



  
    public void TestGetLetter()
    {
        Debug.Log(letters.Length);
        int index = UnityEngine.Random.Range(0,letters.Length);
        CreateLetter(index);
    }


    private void Awake()
    {
        for (int i = 0; i < UZZ_DataTable.LetterTextTable.Count; i++)
        {
            letters[i].index = UZZ_DataTable.LetterTextTable[i].index;
            letters[i].id = UZZ_DataTable.LetterTextTable[i].id;
            letters[i].letter_Character = UZZ_DataTable.LetterTextTable[i].letter_Character;
            letters[i].text_Kor = UZZ_DataTable.LetterTextTable[i].text_Kor;
            letters[i].text_Eng = UZZ_DataTable.LetterTextTable[i].text_Eng;



        }
     

        LockerRoom = new Transform[chLocker.transform.childCount];
        for (int i = 0; i < chLocker.transform.childCount; i++)
        {
            LockerRoom[i] = chLocker.transform.GetChild(i);
        }
      

        JsonLetterLoad();
        JsonLockLoad();
       
    }

    public void CreateLetter(int index)
    {
        GameObject tmpObject = GameObject.Instantiate(letterPrefab);
        tmpObject.GetComponentsInChildren<TMP_Text>()[0].text = letters[index].letter_Character;
        tmpObject.GetComponentsInChildren<TMP_Text>()[1].text = letters[index].text_Kor;
        tmpObject.transform.SetParent(content.transform, false);
        tmpObject.transform.GetComponent<Button>().onClick.AddListener(() => { OnClickBox(tmpObject, index); });
        
        if(!isLetterLoad)
        {
            letterDatas.Add(letters[index]);
            JsonLetterSave();
        }
    
        
        
        
        
    }
   
    public void OnClickBox(GameObject clickedObject,int index)
    {
        GameObject tmpObject = Instantiate(letterPopupPrefab);
       
        tmpObject.GetComponentsInChildren<TMP_Text>()[0].text = clickedObject.GetComponentsInChildren<TMP_Text>()[0].text;
        tmpObject.GetComponentsInChildren<TMP_Text>()[1].text = clickedObject.GetComponentsInChildren<TMP_Text>()[1].text;
       
        tmpObject.transform.SetParent(panel.transform, false);
        tmpObject.GetComponentInChildren<Button>().onClick.AddListener(() => { CheckLetter(tmpObject,clickedObject,index); });


    }

    public void CheckLetter(GameObject letter,GameObject clickedObject,int index)
    {
        
        clickedObject.SetActive(false);
        letter.SetActive(false);
        Debug.Log(index + "Letters");
        
        for (int i = 0; i < letterDatas.Count; i++)
        {
            if (letterDatas[i].index == letters[index].index && letterDatas[i].id == letters[index].id)
            {
                letterDatas.RemoveAt(i);
                break; // 첫 번째 요소를 제거한 후 루프 종료
            }
        }
        JsonLetterSave();
       
        CreateLockLetter(index);
        
    }
    public void CreateLockLetter(int index)
    {
        Debug.Log("확인완료)");
        Debug.Log("에러인덱스" + index);
        Debug.Log(letters[index].id);
        int targetID = letters[index].id;
        ID[] childIDs = chLocker.GetComponentsInChildren<ID>();

        // ID가 일치하는 자식 오브젝트 찾기
     
        foreach (ID childID in childIDs)
        {
            if (childID.id == targetID)
            {
                
                Debug.Log("같은거찾음");
                // 새로운 오브젝트 생성
                GameObject tmpObject = GameObject.Instantiate(letterPrefab);
                tmpObject.GetComponentsInChildren<TMP_Text>()[0].text = letters[index].letter_Character;
                tmpObject.GetComponentsInChildren<TMP_Text>()[1].text = letters[index].text_Kor;
                tmpObject.transform.GetComponent<Button>().onClick.AddListener(() => { OnClickBox(tmpObject, index); });
                tmpObject.transform.SetParent(childID.transform, false);
                
                break;

            }
        }

        if (!isLockLoad)
        {
            lockerDatas.Add(letters[index]);
            JsonLockerSave();
        }


    }

    public void OnLockerBtn()
    {
        letterLocker.SetActive(true);
        titleName.text = "편지 보관함";
        newLetter.SetActive(true);
        letterInventory.SetActive(false);

        letterLocker.SetActive(true);
        letter_new.SetActive(false);
        chLetter.SetActive(true);

    }
   
    public void OnLockerCHBtn(int index)
    {
        chLocker.SetActive(true);
        LockerRoomOff();
        LockerRoom[index].gameObject.SetActive(true);
        letter2_exit_bujtton.SetActive(true);
        newLetter.SetActive(false);

        chLetter.SetActive(false);


    }
    public void OffLockerCHBtn()
    {
        chLocker.SetActive(false);
        LockerRoomOff();
        newLetter.SetActive(true);

        chLetter.SetActive(true);
    }
    private void LockerRoomOff()
    {
        for (int i = 0; i < LockerRoom.Length; i++)
        {
            LockerRoom[i].gameObject.SetActive(false);
        }
        letter2_exit_bujtton.SetActive(false);
    }
    public void OnNewLetterBtn()
    {
        letterLocker.SetActive(false);
        titleName.text = "새로 온 편지";
        newLetter.SetActive(false);
        letterInventory.SetActive(true);
        letter_new.SetActive(false);


        letterLocker.SetActive(false);
        letter_new.SetActive(true);

    }

    public void OffPanelBtn()
    {
        panel.SetActive(false);
        LockerRoomOff();
    }

    public void OnPanelBtn()
    {
        panel.SetActive(true);
        OnNewLetterBtn();
        chLocker.SetActive(false);
    }

}
