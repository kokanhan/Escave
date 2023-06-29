
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class safeArea : MonoBehaviour
{
    public GameObject dynamite;
    public bool hurt = false;
    public GameObject blackScreen;
    //public Image blackScreen;
    public GameObject player;
    public GameObject UIBlur;

    public int interpolationFrames = 5000;
    FirstPersonMovement fm;
    float targetAlpha = 1.0f;
    Color curColor;
    float elapsedFrames = 0f;
    float rate;
    float elapsed = 0f;
    float lastTime = 0f;
    bool frozen;

    GameObject image;
    
    // Start is called before the first frame update
    void Start()
    {
        //image = blackScreen.GetComponent<Image>();
        curColor = blackScreen.GetComponent<Image>().color;
        //curColor = blackScreen.color;
        //curColor = image.color;
        blackScreen.SetActive(false);
        lastTime = Time.realtimeSinceStartup;
        frozen = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hurt == true)
        {
            StartCoroutine("Wait2Sec");
            //Debug.Log("fade out");
            blackScreen.SetActive(true);
            StartCoroutine("goDeadEnd");
            //Time.deltaTime is the interval in seconds from the last frame to the current one (Read Only).
            //if(Time.timeScale>0f)
            //{
            //    Time.timeScale -= 0.5f*Time.deltaTime;
            //}
            if(frozen ==true){
                elapsed += Time.realtimeSinceStartup - lastTime;
                lastTime = Time.realtimeSinceStartup;
                Time.timeScale = Mathf.Lerp(1f, 0f, 0.07f * elapsed);
                Debug.Log("elapsed: " + elapsed + " lastTime: " + lastTime);
            }
                
            


        }
    }



    //在爆炸之前如果trigger了 就触发存活结局
    //或者换种逻辑
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            dynamite.GetComponent<Exploder>().PlayerisSafe = true;
            //可以设置一个变量 传输给expoloder 代表玩家已逃离 ，如果未逃离就在exploder那边进入死亡结局 先是屏幕血迹 延迟4秒后死亡
            //这个gameobject在玩家点燃炸弹后出现
        }
    }
    //给炸飞到地面上这个过程留充足的时间
    IEnumerator Wait2Sec()
    {
        yield return new WaitForSeconds(3f);
        frozen = true;
        UIBlur.SetActive(true);
    }

    IEnumerator goDeadEnd()
    {
        yield return new WaitForSeconds(3f);
        //fm = player.GetComponent<FirstPersonMovement>();
        //fm.enabled = false;//freeze player input
        //player.GetComponent<Jump>().enabled = false;
        //player.GetComponentInChildren<FirstPersonLook>().enabled = false;


        while (Mathf.Abs(curColor.a - targetAlpha) > 0.0001f)
        //while (rate < 1f) ;
        {
            elapsedFrames += Time.realtimeSinceStartup - lastTime;
            Debug.Log("now the elapsedFrames is" + elapsedFrames);
            rate = (float)elapsedFrames / interpolationFrames;
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, rate*0.001f);
            //Debug.Log("Now curColor.a is" + curColor.a);
            //blackScreen.color = curColor;
            blackScreen.GetComponent<Image>().color = curColor;
            
            //image.color = curColor;
            yield return null;
        }
        SceneManager.LoadScene("dead end", LoadSceneMode.Single);
        Time.timeScale = 1f;
    }
}
