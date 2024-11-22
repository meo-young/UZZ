using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    // Start is called before the first frame update
    public int index;
    public string fName;
    public float speed = 1.0f;
    public int touch;
    public int score;

   
    
    void OnMouseDown()
    {
        touch--;
        //PoolManager.Instance.GetFromPool<Transform>().transform.position = transform.localPosition;

    }
    public void Init(int index, string name, float speed,int touch,int score  )
    {
        this.index = index;
        this.fName = name;
        this.speed = speed;
        this.touch = touch;
        this.score = score;
    }
  

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.OgeulaeMiniGame.isStart)
            gameObject.SetActive(false);
        transform.position += Vector3.right * speed * Time.deltaTime;

        if (touch <= 0)
        {
            gameObject.SetActive(false);
            GameManager.Instance.Catch(score);
        }
           
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DestroyWall"))
        {
            gameObject.SetActive(false);
        }
    }
}
