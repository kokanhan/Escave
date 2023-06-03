using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PushCart : MonoBehaviour
{
    public GameObject startCartOBJ;
    public GameObject cart;
    public GameObject smokeFX;
    private bool effectPlayed;

    public GameObject hintLayout;
    public GameObject hintBar;
    public GameObject hintText;

    private Vector2 hintOriginPos;
    private float hintCurDegree;
    private float hintDistant;
    private float hintSpd;

    private Vector3 startPos = new Vector3(-79.586f, 144.23f, 125.361f);
    private Vector3 startRot = new Vector3(-0.774f, 23.571f, -1.718f);

    private Vector3 midRot = new Vector3(-47.285f, 50.665f, -2.221f);

    private Vector3 endPos = new Vector3(-81.56246f, 138.6243f, 120.042f);
    private Vector3 endRot = new Vector3(7.293f, 51.61f, 0);

    private bool isIn;
    private bool isFocus;
    private float PushPushPushValue = 0;
    private bool isAnimating;

    private float curTime = 0;
    private float time = 2.5f;

    private bool theEnd;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
      cart.transform.position = startPos;
      cart.transform.localEulerAngles = startRot;

      hintOriginPos = hintLayout.GetComponent<RectTransform>().position;
    }

    // Update is called once per frame
    void Update()
    {
      checkFocus();

      if(isIn && isFocus && !theEnd)
      {
        if(Input.GetKeyDown(KeyCode.F))
        {
          PushPushPushValue += 5;

          hintDistant = Mathf.Min(hintDistant + 7, 40);
          hintSpd = Mathf.Min(hintSpd + 3f, 7f);

          if(PushPushPushValue >= 100)
          {
            isAnimating = true;
            theEnd = true;
            Destroy(hintLayout);
          }
        }
      }

      if(!theEnd)
      {
        PushPushPushValue = Mathf.Max(0, PushPushPushValue - 5f * Time.deltaTime);
        hintDistant = Mathf.Max(0, hintDistant - 40 * 0.3f * Time.deltaTime);
        hintSpd = Mathf.Max(0, hintSpd - 6f * 0.3f * Time.deltaTime);

        hintBar.GetComponent<Image>().material.SetFloat("_Percentage", PushPushPushValue / 100f);
        animateUI();
      }

      if (isAnimating)
      {
        animate();

        if(curTime > time)
        {
          isAnimating = false;

          cart.transform.position = endPos;
          cart.transform.localEulerAngles = endRot;
        }
      }
    }

    private void animate()
    {
      Vector3 curPos = new Vector3();

      if(curTime < time * 0.8f)
      {
        curPos = Vector3.Lerp(startPos, endPos, easeInOutQuint(curTime / (time * 0.8f)));
      }
      else
      {
        curPos = endPos;
      }

      if(curTime < time * 0.4f)
      {
        curPos.y = startPos.y;
      }
      else if(curTime < time * 0.8f)
      {
        curPos.y = Mathf.Lerp(startPos.y, endPos.y, ((curTime - time * 0.4f) / (time * 0.4f)));
      }

      cart.transform.position = new Vector3(curPos.x, curPos.y, curPos.z);

      if(curTime / time < 0.8f)
      {
        cart.transform.localEulerAngles = Vector3.Lerp(startRot, midRot, easeInQuad(curTime / (time * 0.8f)));
      }
      else
      {
        cart.transform.localEulerAngles = Vector3.Lerp(midRot, endRot, easeInQuart((curTime - 0.8f * time) / (time * 0.2f)));

        if(!effectPlayed)
        {
          effectPlayed = true;
          smokeFX.GetComponent<ParticleSystem>().Play();

        }
            startCartOBJ.GetComponent<StartCart>().cartOnRail = true;
      }

      curTime += Time.deltaTime;
    }

    private void animateUI()
    {
      hintCurDegree += hintSpd * Time.deltaTime;
      hintLayout.GetComponent<RectTransform>().position = hintOriginPos + new Vector2(Mathf.Sin(hintCurDegree), Mathf.Cos(hintCurDegree)) * hintDistant;
    }

    private void checkFocus()
    {
      if(theEnd || !isIn)
      {
        return;
      }

      if(player != null)
      {
        Vector3 targetDir = new Vector3(
          cart.transform.position.x - player.transform.position.x,
          0,
          cart.transform.position.z - player.transform.position.z
        );

        float angle = Vector3.Angle(targetDir, player.transform.forward);

        isFocus = angle < 30;
      }

      hintLayout.SetActive(isIn & isFocus);

      if(!(isIn & isFocus))
      {
        PushPushPushValue = 0;
        hintCurDegree = 0;
        hintDistant = 0;
        hintSpd = 0;
      }
    }

    private float easeInOutQuint(float a)
    {
      return a < 0.5f? 16 * a * a * a * a * a : 1 - Mathf.Pow(-2 * a + 2f, 5) / 2f;
    }

    private float easeInQuad(float a)
    {
      return a * a;
    }

    private float easeInQuart(float a)
    {
      return a * a * a * a;
    }

    void OnTriggerEnter(Collider other)
    {
      if(other.tag == "Player")
      {
        isIn = true;

        player = other.gameObject;
      }
    }

    void OnTriggerExit(Collider other)
    {
      if(other.tag == "Player")
      {
        isIn = false;

        hintCurDegree = 0;
        hintDistant = 0;
        hintSpd = 0;
      }
    }
}
