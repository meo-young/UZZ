using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenerManager : MonoBehaviour
{
    [SerializeField] private string name;       //�̸�
    [SerializeField] private int id;            //���� ��ȣ
    
    [SerializeField] private int age;           //����
    [SerializeField] private int exp;           //����ġ
    [SerializeField] private int intelligence;  //����
    [SerializeField] private int sensitivity;   //����
    [SerializeField] private int reason;        //�̼�

    [SerializeField] private DialogSystem dialogSystem;
    [SerializeField] private CharacterUIManager characterUIManager;
    [SerializeField] private GameObject restNotice;
    [SerializeField] private GameObject workNotice;

    [SerializeField] private GameObject gardener_inWindow;
    [SerializeField] private GameObject gardener_inWork;
    [SerializeField] private GameObject gardener_animObject;

    private bool isFirstConversation; //�ð��밡 �ٲ� ���� ù ��ȭ�ΰ�?

    [SerializeField]
    private int time;

    private SkeletonAnimation skeletonAnimation;
    [SerializeField] private SkeletonDataAsset skeletonData_idle;
    [SerializeField] private SkeletonDataAsset skeletonData_rest;

    [SerializeField] private GameObject gameObject_idle;
    [SerializeField] private GameObject gameObject_rest;

    private bool randomSystem;

    public enum State
    {
        Idle,
        Greet,
      
        Rest,
        Work
    }



    State state;

    void Start()
    {
        setGardnerState(State.Idle);

        skeletonAnimation = gardener_animObject.GetComponent<SkeletonAnimation>();

       

    }


    void Update()
    {
        /*
        if (state == State.Rest && randomSystem == false) 
        {
            randomSystem = true;
            int random = Random.Range(3, 6);

            StartCoroutine(set_random(random));
        }
        */
    }

    public void touchGardener()
    {
        Debug.Log("��ġ��");
        if (state == State.Idle)
        {
            //������ ���� ù��ȭ �ð������� ����ϰ� �״������� �ϻ��ȭ
            //if (isFirstConversation)
            //{
            //    isFirstConversation = false;
            //    dialogSystem.start_DialogSystem(id, time, DialogSystem.TalkType.Gardener);
            //}
            //else 
            //{
            //    dialogSystem.start_DialogSystem(id, 4, DialogSystem.TalkType.Gardener);
            //}

            if(dialogSystem.isEnd)
            {
                dialogSystem.start_DialogSystem(id, time, DialogSystem.TalkType.Gardener);
            }
            else
            {
                dialogSystem.UpdateDialog();
            }
            
                
            

        }
        else if (state==State.Greet)
        {
            if (isFirstConversation)
            {
                isFirstConversation = false;
                if (time == 0)
                {
                    dialogSystem.start_DialogSystem(id, 4, DialogSystem.TalkType.Gardener);
                }
                else if (time == 3)
                {
                    dialogSystem.start_DialogSystem(id, 5, DialogSystem.TalkType.Gardener);
                }    
            }
           
            
            setGardnerState(State.Idle);
        }
        
        else if (state == State.Rest) 
        {
            dialogSystem.start_DialogSystem(id, 5, DialogSystem.TalkType.Gardener);
        }
        else if (state == State.Work)
        {
            dialogSystem.start_DialogSystem(id, 6, DialogSystem.TalkType.Work);
        }
        
    }

    public void displayCharacterUI()
    {
        characterUIManager.set_CharacterInfo(name, age, 18, 300, 500, 3, 55, 100, 2, 85, 100, 1, 40, 100);
        characterUIManager.on_CharacterInfo();
    }

    public State getGardnerState()
    {
        return state;
    }

    public void setGardnerState(State _state) 
    {
        state = _state;

        if (state != State.Idle) 
        {
            //workNotice.SetActive(false);
            //restNotice.SetActive(false);
            //gameObject_idle.SetActive(false);
        }
        else
        {
           
            //gameObject_idle.SetActive(true);
            

            
            
        }

        if (state == State.Rest)
        {
            //gameObject_rest.SetActive(true);
            //skeletonAnimation.skeletonDataAsset = skeletonData_rest;
            //skeletonAnimation.Initialize(true);
            //skeletonAnimation.AnimationName = "animation";
        }
        else
        {
            //StopCoroutine("drink_Tea");
            //StopCoroutine("set_random");
           // gameObject_rest.SetActive(false);
        }

        if (state == State.Greet)
        {
            //ameObject_idle.SetActive(false);
            //skeletonAnimation.skeletonDataAsset = skeletonData_rest;
            //skeletonAnimation.Initialize(true);
            //skeletonAnimation.AnimationName = "animation";
        }
        else
        {
            
        }

        if (state == State.Work)
        {
            gardener_inWindow.SetActive(false);
            gardener_inWork.SetActive(true);
            //workNotice.SetActive(false);
        }
        else
        {
            gardener_inWindow.SetActive(true);
            gardener_inWork.SetActive(false);
        }
    }

    public void set_FirstConversation(bool val, int _time)
    {
        isFirstConversation = val;
        time = _time;
    }

    private IEnumerator drink_Tea()
    {
        skeletonAnimation.AnimationName = "2_Drinking tea";
        yield return new WaitForSeconds(3.33f);
        skeletonAnimation.AnimationName = "1_Tea_Idle";
        randomSystem = false;
    }

    private IEnumerator set_random(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(drink_Tea());
    }





    
}

