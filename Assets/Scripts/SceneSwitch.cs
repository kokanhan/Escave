using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSwitch : MonoBehaviour
{
    public GameObject mask;

    private float startA;
    private float endA;
    private float curTime;
    private float time = 2;

    private bool isIn;

    // Update is called once per frame
    void Update()
    {
      if(isIn)
      {
        if(curTime < time)
        {
          mask.GetComponent<Image>().color = new Color(0f, 0f, 0f, Mathf.Lerp(startA, endA, curTime / time));
        }
        else
        {
          isIn = false;
        }

        curTime += Time.deltaTime;
      }

    }

    public void show(int mode)
    {
      isIn = true;

      curTime = 0;

      if(mode == 1)
      {
        startA = 0f;
        endA = 1f;
      }
      else
      {
        startA = 1f;
        endA = 0f;
      }
    }
}
