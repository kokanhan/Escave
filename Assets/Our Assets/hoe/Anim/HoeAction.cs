using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoeAction : MonoBehaviour
{
    public Camera camera;
    public GameObject hoe;
    public GameObject detector;
    public GameObject hitEffect;

    private Vector3 start = new Vector3(0f, -286.491f, 11.457f);
    private Vector3 mid = new Vector3(0f, -286.491f, -20.457f);
    private Vector3 end = new Vector3(0f, -295.491f, 80.457f);

    private float midStartTime = 1.5f;
    private float midEndTime = 1.7f;
    private float endStartTime = 2.7f;

    private Vector3 curEnd;
    private float curTime;
    private bool isIn;

    void Start()
    {
      detector.transform.localPosition = new Vector3(-0.375f, 0.707f, 0.002568513f);
      curEnd = end;
    }

    // Update is called once per frame
    void Update()
    {
      // if(!GetComponent<FirstPersonMovement>().hoePicked)
      // {
      //   return;
      // }

      if (Input.GetKeyDown(KeyCode.F))
      {
        curTime = 0;
        isIn = true;
        curEnd = end;
      }

      if(isIn)
      {
        if(curTime < midStartTime)
        {
          hoe.transform.localEulerAngles = Vector3.Lerp(start, mid, easeOutcirc(curTime / midStartTime));
        }
        else if(curTime < midEndTime)
        {
          RaycastHit hit;

          if(Physics.Raycast(detector.transform.position, detector.transform.forward, out hit, 0.5f))
          {
            hitEffect.transform.position = hit.point;
            hitEffect.GetComponent<ParticleSystem>().Play();

            forceStop();

            return;
          }

          hoe.transform.localEulerAngles = Vector3.Lerp(mid, end, easeInCirc((curTime - midStartTime) / (midEndTime - midStartTime)));
        }
        else if(midEndTime < curTime && curTime < endStartTime)
        {
          hoe.transform.localEulerAngles = curEnd;
        }
        else if(endStartTime < curTime && curTime < endStartTime + 1)
        {
          hoe.transform.localEulerAngles = Vector3.Lerp(curEnd, start, (curTime - endStartTime) / 1f);
        }
        else if(endStartTime + 1 < curTime)
        {
          hoe.transform.localEulerAngles = start;
          isIn = false;
        }

        curTime += Time.deltaTime;
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
