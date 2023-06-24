using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HoeUI : MonoBehaviour
{
    private bool theEnd = false;
    public bool isIn;



    public GameObject hintLayout;
    public GameObject hintBar;
    public GameObject hintText;
    private float PushPushPushValue = 0f;


    private Vector2 hintOriginPos;
    private float hintCurDegree;
    private float hintDistant;
    private float hintSpd;

    bool pressedF = false;
    private void Start()
    {
        hintOriginPos = hintLayout.GetComponent<RectTransform>().position;
        PushPushPushValue = 0f;


        //hintBar.GetComponent<Image>().material.SetFloat("_Percentage", 0.5f);
    }
    // Update is called once per frame
    void Update()
    {

            if (Input.GetKeyDown(KeyCode.F) && isIn)
            {
                pressedF = true;
                PushPushPushValue += 3;

                //Returns the smallest of two or more values.没看懂为啥
                hintDistant = Mathf.Min(hintDistant + 7, 40);
                hintSpd = Mathf.Min(hintSpd + 3f, 7f);

                if (PushPushPushValue >= 9)
                {
                    //已经有脚本控制矿石消失了
                    theEnd = true;
                //Destroy(hintLayout,1f);//改为延迟一秒后数据清零 然后隐藏layout
                StartCoroutine("CleanUpData");
                }
            }
        

        if ( isIn && (pressedF ==true))
        {
       
            hintDistant = Mathf.Max(0, hintDistant - 40 * 0.3f * Time.deltaTime);
            hintSpd = Mathf.Max(0, hintSpd - 6f * 0.3f * Time.deltaTime);

            hintBar.GetComponent<Image>().material.SetFloat("_Percentage", (PushPushPushValue*10+9.8f) / 100f);//记得把percent清零 结束的时候 

            //用来查找material在不在
            //Material temp = hintBar.GetComponent<Image>().material;
            //if (temp != null)
            //{
            //    Debug.Log("you"+ temp.name);
            //}
            Debug.Log("percent " + PushPushPushValue / 100f);
            animateUI();
        }

        
    }

    private IEnumerator CleanUpData()
    {
        yield return new WaitForSeconds(1f);
        PushPushPushValue = 0f;
        hintBar.GetComponent<Image>().material.SetFloat("_Percentage", 0f);
        hintDistant = 0f;
        hintSpd = 0f;
        hintLayout.SetActive(false);
        isIn = false;
    }

    private void animateUI()
    {
        hintCurDegree += hintSpd * Time.deltaTime;
        hintLayout.GetComponent<RectTransform>().position = hintOriginPos + new Vector2(Mathf.Sin(hintCurDegree), Mathf.Cos(hintCurDegree)) * hintDistant;
    }


    //这里要改成ray hit到mine
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isIn = true;
            Debug.Log("Hoe is in");
            hintLayout.SetActive(true);
        }
    }


}
