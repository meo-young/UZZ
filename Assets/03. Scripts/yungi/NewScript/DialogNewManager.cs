using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using KoreanTyper;

[System.Serializable]
public class Dialog
{
    

    public int index;
    public string name;
    public DType dtype;
    [TextArea]
    public string dialog;
    public string dialog2;

    public int nextIndex;
    public int nextIndex2;
    public string bgm;
    public string sfx;
    public string transition;
    public int background;
    public float bgsize;

    public float transform_x;
    public float transform_y;
    public float size;

    public float des_x;
    public float des_y;
    public float des_speed;
    public float zoom_size;
    public float zoom_speed;

    public int font_size;
    public float delay;
}

public enum DType
{
    Dialog,
    Choice
};
public class DialogNewManager : MonoBehaviour
{

    public Dialog[] dialog;
    [SerializeField]    private DialogNew dialogNew;
    [SerializeField]    private int currentIndex = -1;
    [SerializeField]    private GameObject textBox;
    [SerializeField]    private TMP_Text dialogText;
    [SerializeField]    private TMP_Text nameText;
    [SerializeField]    private TMP_Text[] ChoiceText;
    [SerializeField]    private GameObject[] ChoiceBtn;
    [SerializeField]    private Camera camera;

    [SerializeField]    private GameObject[] story;
    [SerializeField]    private float timerForCharacter;        //0.08초가 기본
    [SerializeField]    private float timerForCharacter_Fast;   //0.03초가 빠른 텍스트
    [SerializeField]    private float characterTime;            //실제 적용 문자열 속도

    public bool isCho;
    public bool isCMove;
    public bool isCZoom;
    public int branch;

    string originText;


    [SerializeField] private GameObject Story_Dialog;
    [SerializeField] private Transform storyPos;
    [SerializeField] private CameraController cameraContoller;
    private void Awake()
    {
        //dialog = new Dialog[dialogNew.Entities.Count];
        isTypingEnd = true;
        characterTime = timerForCharacter;

    }
   
    private void Start()
    {
        DialogUpdate();

        

        foreach (Transform child in story[0].transform)
        {
            child.gameObject.SetActive(false);
        }

       
                                   
    }

    int bCount = 0;
    void DialogUpdate()
    {
        int dIndex = 0;

        bCount = 0;

      
        for (int i = 0; i < dialogNew.Entities.Count; i++)
        {
            if (dialogNew.Entities[i].branch == branch)
            {
                bCount++;
            }
        }

        
        for (int i = 0; i < dialogNew.Entities.Count; i++)
        {
            if(dialogNew.Entities[i].branch == branch)
            {
                dialog[dIndex].index = dialogNew.Entities[i].index;
                dialog[dIndex].name = dialogNew.Entities[i].name;
                dialog[dIndex].dtype = dialogNew.Entities[i].type;
                dialog[dIndex].dialog = dialogNew.Entities[i].dialog;
                dialog[dIndex].dialog2 = dialogNew.Entities[i].dialog2;

                dialog[dIndex].nextIndex = dialogNew.Entities[i].nextIndex;
                dialog[dIndex].nextIndex2 = dialogNew.Entities[i].nextIndex2;

                dialog[dIndex].bgm = dialogNew.Entities[i].bgm;
                dialog[dIndex].sfx = dialogNew.Entities[i].sfx;
                dialog[dIndex].transition = dialogNew.Entities[i].transition;
                dialog[dIndex].background = dialogNew.Entities[i].background;
                dialog[dIndex].bgsize = dialogNew.Entities[i].bgsize;

                dialog[dIndex].transform_x = dialogNew.Entities[i].transform_x;
                dialog[dIndex].transform_y = dialogNew.Entities[i].transform_y;
                dialog[dIndex].size = dialogNew.Entities[i].size;

                dialog[dIndex].des_x = dialogNew.Entities[i].des_x;
                dialog[dIndex].des_y = dialogNew.Entities[i].des_y;
                dialog[dIndex].des_speed = dialogNew.Entities[i].des_speed;

                dialog[dIndex].zoom_size = dialogNew.Entities[i].zoom_size;
                dialog[dIndex].zoom_speed = dialogNew.Entities[i].zoom_speed;

                dialog[dIndex].font_size = dialogNew.Entities[i].font_size;
                dialog[dIndex].delay = dialogNew.Entities[i].delay;
                dIndex++;
            }

               
                
        }

         
          
            
        
    }

    Vector3 targetPosition_2;
    private void Update()
    {
        if (isCMove) // 이동명령이 떨어지면 이동
        {
            float step = dialog[currentIndex].des_speed * Time.deltaTime; // 한 프레임당 이동할 거리
            Vector3 camPos = new Vector3(camera.transform.position.x, camera.transform.position.y, -10);
         
            camera.transform.position = Vector3.MoveTowards(camPos, targetPosition_2, step);
        
        }
        
        if (isCZoom) // 이동명령이 떨어지면 이동
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, dialog[currentIndex].zoom_size, dialog[currentIndex].zoom_speed * Time.deltaTime);
        
        }

    }

    //텍스트 라이팅 효과 시작 --
    bool isTypingEnd;
    IEnumerator _typing()
    {
        isTypingEnd = false;
        originText = dialog[currentIndex].dialog;
        int typingLength = originText.GetTypingLength();
        for (int i = 0; i <= typingLength; i++)
        {
            dialogText.text = originText.Typing(i);
            if (!isTypingEnd)
            {
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                dialogText.text = dialog[currentIndex].dialog;
                break;
            }
                

           
        }

        //dialogText.text = string.Empty;
        //characterTime = timerForCharacter;
        //for (int i = 0; i <= dialog[currentIndex].dialog.Length; i++)
        //{
        //    dialogText.text = dialog[currentIndex].dialog.Substring(0, i);

        //    yield return new WaitForSeconds(characterTime);
        //}
        yield return new WaitForSeconds(0.15f);
        isTypingEnd = true;
    }

    public void GetInputDown()
    {

        
        if(isTypingEnd && !isCho)
        {
            Next();
        }
        else
        {
            
            isTypingEnd = true;

        }
            
        
    }

   

    //텍스트 라이팅 효과 끝 --

    //아직 작성중ㅑ샤ㅐㅜ채ㅜ
    void Next()
    {
        ChoiceBtn[0].gameObject.SetActive(false);
        ChoiceBtn[1].gameObject.SetActive(false);
        textBox.SetActive(false);
        nameText.gameObject.SetActive(false);

        if (currentIndex >= bCount) 
        {//끝남
            isCho = true;
            cameraContoller.goWindow();
            camera.orthographicSize = 18;
            return;

        }

        ++currentIndex;

        

        

        if (dialog[currentIndex].transition !=null)  // 트랜지션 체크
        {
            switch (dialog[currentIndex].transition)
            {
                case "Fade_B":
                    TransitionController.Play(GlobalValue.Transition.Fade_B);
                    break;
                case "FadeIn_B":
                    TransitionController.Play(GlobalValue.Transition.FadeIn_B);
                    break;
                case "FadeOut_B":
                    TransitionController.Play(GlobalValue.Transition.FadeOut_B);
                    break;
                case "Fade_W":
                    TransitionController.Play(GlobalValue.Transition.Fade_W);
                    break;
                case "FadeIn_W":
                    TransitionController.Play(GlobalValue.Transition.FadeIn_W);
                    break;
                case "FadeOut_W":
                    TransitionController.Play(GlobalValue.Transition.FadeOut_W);
                    break;
                default:
                    break;
            }
        }

        if (dialog[currentIndex].background != 0) //배경 체크
        {
            foreach (Transform child in story[0].transform)
            {
                // child.gameObject.SetActive(false);\
                if (child.gameObject.activeSelf)
                {
                    child.gameObject.GetComponent<StoryBG>().Off();
                }
                
            }

            story[0].transform.GetChild(dialog[currentIndex].background-1).gameObject.SetActive(true);
            story[0].transform.GetChild(dialog[currentIndex].background - 1).gameObject.transform.localScale
                = new Vector3(dialog[currentIndex].bgsize, dialog[currentIndex].bgsize, 1);

        }

        if (dialog[currentIndex].font_size != 0) //폰트 체크
        {

            dialogText.fontSize = dialog[currentIndex].font_size;

        }





        if (dialog[currentIndex].dtype == DType.Dialog)   //다이아로그일때
        {
            isCho = false;
            textBox.SetActive(true);
            nameText.gameObject.SetActive(true);
            StartCoroutine(_typing());
            nameText.text = dialog[currentIndex].name;
            if (dialog[currentIndex].nextIndex != 0)
                currentIndex = dialog[currentIndex].nextIndex - 1;
        }
        else if (dialog[currentIndex].dtype == DType.Choice) //선택지일때
        {
            isCho = true;
            ChoiceText[0].text = dialog[currentIndex].dialog;
            ChoiceText[1].text = dialog[currentIndex].dialog2;

            if (dialog[currentIndex].dialog.Length !=0)
            {
                ChoiceBtn[0].GetComponentInChildren<TMP_Text>().text = dialog[currentIndex].dialog;
                ChoiceBtn[0].gameObject.SetActive(true);
            }
            if (dialog[currentIndex].dialog2.Length!=0)
            {
               ChoiceBtn[1].GetComponentInChildren<TMP_Text>().text = dialog[currentIndex].dialog2;
               ChoiceBtn[1].gameObject.SetActive(true);
            }

          
        }

        if (dialog[currentIndex].transform_x != 0 || dialog[currentIndex].transform_y != 0) //카메라 이동
        {
            Vector3 targetPosition = new Vector3(dialog[currentIndex].transform_x, dialog[currentIndex].transform_y,-10);
            camera.transform.position = targetPosition;

           
        }
        if(dialog[currentIndex].size != 0)
        {
            camera.orthographicSize = dialog[currentIndex].size;
        }

        //카메라 이동 2
        if (dialog[currentIndex].des_x != 0 || dialog[currentIndex].des_y!= 0 ) //카메라 이동
        {
            isCMove = true;
            targetPosition_2 = new Vector3(dialog[currentIndex].des_x, dialog[currentIndex].des_y,-10);   
        }
        else
        {
            isCMove = false;
        }

        if (dialog[currentIndex].zoom_size!=0)
        {
            isCZoom = true;
        }
        else
        {
            isCZoom = false;
        }

        //넥스트 지정이 되어있으면



        // 대사가 남아있을 경우 다음 대사 진행

        if (dialog[currentIndex].delay != 0)
        {
            StartCoroutine(Delay(dialog[currentIndex].delay));
            textBox.SetActive(false);
            
        }

        if (dialog[currentIndex].nextIndex != 0)
                currentIndex = dialog[currentIndex].nextIndex - 1;

        








    }

    private IEnumerator Delay(float delay)
    {

        
        yield return new WaitForSeconds(delay);

        Next();


    }

    public void NextIndex(int i) //버튼에 넣으면됨
    {
        if(i==0)
        {
            currentIndex = dialog[currentIndex].nextIndex - 1;
            Next();
        }
        else if(i==1)
        {
            currentIndex = dialog[currentIndex].nextIndex2 - 1;
            Next();
        }
        

        
    }

    public void TestStory()
    {
        Story_Dialog.SetActive(true);
        camera.transform.position = storyPos.transform.position;
        Next();

    }
}
