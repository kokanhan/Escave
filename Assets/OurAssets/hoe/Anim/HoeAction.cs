using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HoeAction : MonoBehaviour
{
    public Camera camera;
    public GameObject hoe;
    public GameObject detector;
    public GameObject hitEffect;
    public GameObject[] smallRock;

    private Vector3 start = new Vector3(0f, -286.491f, 11.457f);
    private Vector3 mid = new Vector3(0f, -286.491f, -20.457f);
    private Vector3 end = new Vector3(0f, -295.491f, 80.457f);

    private float midStartTime = 1.5f;
    private float midEndTime = 1.7f;
    private float endStartTime = 2.7f;

    private Vector3 curEnd;
    private float curTime;
    public bool isIn;
    public int DigEnoughTime;
    public GameObject[] Mines;
    public GameObject[] Powders;
    public string MineName;




    //***************************************************************************************************
    // Hoe Action UI Part
    private bool theEnd = false;


    public GameObject hintLayout;
    public GameObject hintBar;
    public GameObject hintText;
    private float PushPushPushValue = 0f;


    private Vector2 hintOriginPos;
    private float hintCurDegree;
    private float hintDistant;
    private float hintSpd;

    bool pressedF = false;

    //***************************************************************************************************



    void Start()
    {
        detector.transform.localPosition = new Vector3(-0.375f, 0.707f, 0.002568513f);
        curEnd = end;
        camera = this.transform.GetChild(0).gameObject.GetComponent<Camera>();



        //***************************************************************************************************
        // Hoe Action UI Part
        hintOriginPos = hintLayout.GetComponent<RectTransform>().position;
        PushPushPushValue = 0f;

        //***************************************************************************************************
    }


void Update()
    {
        Debug.DrawRay(detector.transform.position, detector.transform.forward * 10, Color.red);
        if (!GetComponent<FirstPersonMovement>().hoePicked)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {

            curTime = 0;
            if (gameObject.GetComponent<FirstPersonMovement>().digging == true)
            {
                isIn = true;//暂时设定成遇到矿石才开始isIn 这里要读取FirstPersonMovement的digging是否为true
            }
            curEnd = end;
        }

        if (isIn)
        {
            //*******************************************************************************************************
            //UI part
            //if (gameObject.GetComponent<FirstPersonMovement>().digging == true)//设置成玩家必须对矿物发出射线再显示UI 这样可以解决isIn条件因为update的延迟关系 导致UI在一些帧出现 未成功关闭
            //{
            //    hintLayout.SetActive(true);
            //}
                
            theEnd = false;
            Debug.Log("theEnd  " + theEnd);
            //*******************************************************************************************************


            if (curTime < midStartTime)
            {
                hoe.transform.localEulerAngles = Vector3.Lerp(start, mid, easeOutcirc(curTime / midStartTime));
            }
            else if (curTime < midEndTime)
            {
                RaycastHit hit;
                //Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);
                if (Physics.Raycast(detector.transform.position, detector.transform.forward, out hit, 0.5f))
                {
                    
                    Instantiate(hitEffect, hit.point, Quaternion.identity);
                    hitEffect.transform.position = hit.point;
                    hitEffect.GetComponent<ParticleSystem>().Play();

                    Vector3 p = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));//不好 还是从锄头附近生成合理

                    GameObject tempO = spawnSmallRock(MineName);
                    //根据raycast hit的标签来判断该生成什么种类碎石
                    if (tempO != null)
                    {
                        Instantiate(tempO, new Vector3(hit.point.x - 0.05f, hit.point.y - 0.15f, hit.point.z - 0.3f), Quaternion.identity);//先改射线

                        hoe.GetComponent<AudioSource>().time = 0.65f;
                        hoe.GetComponent<AudioSource>().Play();
                        DigEnoughTime += 1;
                    }
                    
                    if (DigEnoughTime >= 3)
                    {
                        Debug.Log("DigEnoughTime == 3");
                        Debug.Log("hit name" + hit.collider.tag);
                        //destoryMine(MineName);
                        tempO = null;
                    }

                    forceStop();


                    ////***************************************************************************************************
                    //// Hoe Action UI Part
                    //pressedF = true;
                    //PushPushPushValue += 3;
                    //hintDistant = Mathf.Min(hintDistant + 7, 40);
                    //hintSpd = Mathf.Min(hintSpd + 3f, 7f);

                    //if (PushPushPushValue >= 9)
                    //{
                    //    //已经有脚本控制矿石消失了
                    //    theEnd = true;
                    //    //Destroy(hintLayout,1f);//改为延迟一秒后数据清零 然后隐藏layout
                    //    StartCoroutine("CleanUpData");
                    //}

                    //if (isIn && (pressedF == true))
                    //{

                    //    hintDistant = Mathf.Max(0, hintDistant - 40 * 0.3f * Time.deltaTime);
                    //    hintSpd = Mathf.Max(0, hintSpd - 6f * 0.3f * Time.deltaTime);

                    //    hintBar.GetComponent<Image>().material.SetFloat("_Percentage", (PushPushPushValue * 10 + 9.8f) / 100f);//记得把percent清零 结束的时候 

                    //    //用来查找material在不在
                    //    //Material temp = hintBar.GetComponent<Image>().material;
                    //    //if (temp != null)
                    //    //{
                    //    //    Debug.Log("you"+ temp.name);
                    //    //}
                    //    Debug.Log("percent " + PushPushPushValue / 100f);
                    //    animateUI();
                    //}
                    ////***************************************************************************************************

                    pressedF = true;//UI part
                    PushPushPushValue += 3;
                    return;
                }
                
                hoe.transform.localEulerAngles = Vector3.Lerp(mid, end, easeInCirc((curTime - midStartTime) / (midEndTime - midStartTime)));
            }



            

            else if (midEndTime < curTime && curTime < endStartTime)
            {
                hoe.transform.localEulerAngles = curEnd;
            }
            else if (endStartTime < curTime && curTime < endStartTime + 1)
            {
                hoe.transform.localEulerAngles = Vector3.Lerp(curEnd, start, (curTime - endStartTime) / 1f);
            }
            else if (endStartTime + 1 < curTime)
            {
                hoe.transform.localEulerAngles = start;
                isIn = false;
            }

            curTime += Time.deltaTime;
        }


        //***************************************************************************************************
        // Hoe Action UI Part
        //pressedF = true;
        if (pressedF && !theEnd)
        {
            //PushPushPushValue += 3;
            hintDistant = Mathf.Min(hintDistant + 7, 40);
            hintSpd = Mathf.Min(hintSpd + 3f, 7f);

            if (PushPushPushValue >= 9)
            {
               
                StartCoroutine("CleanUpData");
                //return;
            }

            if (isIn)
            {

                hintDistant = Mathf.Max(0, hintDistant - 40 * 0.3f * Time.deltaTime);
                hintSpd = Mathf.Max(0, hintSpd - 6f * 0.3f * Time.deltaTime);

                hintBar.GetComponent<Image>().material.SetFloat("_Percentage", (PushPushPushValue * 10 + 9.8f) / 100f);//记得把percent清零 结束的时候 

                //用来查找material在不在
                //Material temp = hintBar.GetComponent<Image>().material;
                //if (temp != null)
                //{
                //    Debug.Log("you"+ temp.name);
                //}
                //Debug.Log("percent " + PushPushPushValue / 100f);
                animateUI();
            }
            //return;
        }


        if (!isIn)
        {
            StartCoroutine("CleanProgress");
        }

        //***************************************************************************************************
    }




    //***************************************************************************************************
    // Hoe Action UI Part
    private IEnumerator CleanUpData()
    {
        yield return new WaitForSeconds(1f);
        PushPushPushValue = 0f;
        hintBar.GetComponent<Image>().material.SetFloat("_Percentage", 0f);
        hintDistant = 0f;
        hintSpd = 0f;
        hintLayout.SetActive(false);
        theEnd = true;
        pressedF = false;

        destoryMine(MineName);
        //yield return null;
    }

    private void animateUI()
    {
        hintCurDegree += hintSpd * Time.deltaTime;
        hintLayout.GetComponent<RectTransform>().position = hintOriginPos + new Vector2(Mathf.Sin(hintCurDegree), Mathf.Cos(hintCurDegree)) * hintDistant;
    }
    //***************************************************************************************************

    private IEnumerator CleanProgress()
    {
        
        PushPushPushValue = 0f;
        hintBar.GetComponent<Image>().material.SetFloat("_Percentage", 0f);
        hintDistant = 0f;
        hintSpd = 0f;
        hintLayout.SetActive(false);
        pressedF = false;
        yield return null;
    }


    private void destoryMine(string tagName)
    {
        DigEnoughTime = 0;
        Debug.Log("now stich");
        switch (tagName)
        {
            case "ChaMine2Dig":
                //显示碎屑
                Powders[0].SetActive(true);
                Destroy(Mines[0]);
                //isIn = false;
                Debug.Log("des mine");
                break;
            case "NiMine2Dig":
                Powders[1].SetActive(true);
                Destroy(Mines[1]);
                //isIn = false;
                break;
            case "SuMine2Dig":
                Powders[2].SetActive(true);
                Destroy(Mines[2]);
                //isIn = false;
                break;
            default:
                Debug.Log("no macth");
                break;
        }
    }

    private GameObject spawnSmallRock(string name)
    {
        switch (name)
        {
            case "ChaMine2Dig":
                return smallRock[0];
                break;
            case "NiMine2Dig":
                return smallRock[1];
                break;
            case "SuMine2Dig":
                return smallRock[2];
                break;
            default:
                return null;
                break;
        }
    }

    private float easeOutcirc(float a)
    {
        return Mathf.Sqrt(1f - Mathf.Pow(a - 1f, 2));
    }

    private float easeInCirc(float a)
    {
        return 1f - Mathf.Sqrt(1f - Mathf.Pow(a, 2));
    }

    public void forceStop()
    {
        curEnd = Vector3.Lerp(end, start, 1f - easeInCirc((curTime - midStartTime) / (midEndTime - midStartTime)));

        curTime = midEndTime;
    }

    public float getAnimationTime()
    {
        return curTime;
    }




}



