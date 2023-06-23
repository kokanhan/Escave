using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool isIn;
    public int DigEnoughTime;
    public GameObject[] Mines;
    public GameObject[] Powders;
    public string MineName; 

    void Start()
    {
        detector.transform.localPosition = new Vector3(-0.375f, 0.707f, 0.002568513f);
        curEnd = end;
        camera = this.transform.GetChild(0).gameObject.GetComponent<Camera>();
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
                        destoryMine(MineName);
                        tempO = null;
                    }

                    forceStop();
                    return;
                }
                //Destroy(hit.collider.gameObject);
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
                Debug.Log("des mine");
                break;
            case "NiMine2Dig":
                Powders[1].SetActive(true);
                Destroy(Mines[1]);
                break;
            case "SuMine2Dig":
                Powders[2].SetActive(true);
                Destroy(Mines[2]);
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



