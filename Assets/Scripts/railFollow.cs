using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class railFollow : MonoBehaviour
{
    public GameObject cart;
    public GameObject[] pathPoints;

    private Vector3[] positions;
    private Vector3[] rotations;
    private float[] time;
    private float curTime;
    private bool goDownstair;
    private bool isRunning;

    public float speed=5f;

    AudioSource railSound;
    //Hide the player and rest its position
    [Header("======Player to Hide and destination position======")]
    public GameObject player;
    public Collider[] colliders;

    bool playerReady =false;

    // Start is called before the first frame update
    void Start()
    {
        railSound = GetComponent<AudioSource>();

        positions = new Vector3[pathPoints.Length];
        rotations = new Vector3[pathPoints.Length];
    }

    // Update is called once per frame
    void Update()
    {
      if (!isRunning)
      {
        return;
      }

      if(!cart.GetComponent<AudioSource>().isPlaying)
      {
        cart.GetComponent<AudioSource>().Play();
      }
      else
      {
        cart.GetComponent<AudioSource>().volume = Mathf.Lerp(1f, 0.05f, Mathf.Clamp(Vector3.Distance(cart.transform.position, player.transform.position) / 25f, 0f, 1f));//这代码干嘛的
      }

      if(player.transform.parent != null)
      {
        player.transform.localPosition = new Vector3(0, 1, 0);//y代表player在cart所处的高度
      }

      // luzhuo please explain
      for (int i = 0; i < time.Length; i += 1)
      {
        if(curTime < time[i])
        {
          cart.transform.position = Vector3.Lerp(positions[i - 1], positions[i], 1 - (time[i] - curTime) / (time[i] - time[i - 1]));
          cart.transform.localEulerAngles = Vector3.Lerp(rotations[i - 1], rotations[i], 1 - (time[i] - curTime) / (time[i] - time[i - 1]));

          break;
        }

        if(i == time.Length - 1)
        {
          isRunning = false;

          if(player.transform.parent != null)
          {
                    player.transform.localPosition = new Vector3(0, 1.2f, -3f);
                    player.transform.parent = null;
          }

          for(int j = 0; j < colliders.Length; j += 1)
          {
            colliders[j].enabled = false;
          }
        }
      }

      curTime += Time.deltaTime;
    }

    public void startCartNow(bool playerIsIn)
    {
        Vector3 originRotation = player.transform.localEulerAngles;
        player.transform.parent = cart.transform.GetChild(1).gameObject.transform.GetChild(0).transform;
        player.transform.localPosition = new Vector3(0, 1.2f, 0);

        player.transform.eulerAngles = originRotation;
        playerReady = true;

        if (isRunning)
      {
        return;
      }

      isRunning = true;
      curTime = 0;

      if(playerIsIn)
      {
        //Vector3 originRotation = player.transform.localEulerAngles;
        //player.transform.parent = cart.transform.GetChild(1).gameObject.transform.GetChild(0).transform;
        //player.transform.localPosition = new Vector3(0, 1.2f, 0);

        //player.transform.eulerAngles = originRotation;
        //    playerReady = true;
            
        }

      if(goDownstair && playerReady)
      {
            Debug.Log("playerReady");
            for (int i = pathPoints.Length - 1, index = 0; i >= 0; i -= 1, index += 1)
        {
          positions[index] = pathPoints[i].transform.position;
          rotations[index] = new Vector3(
            pathPoints[i].transform.localEulerAngles.x > 180? -(360 - pathPoints[i].transform.localEulerAngles.x) : pathPoints[i].transform.localEulerAngles.x,
            pathPoints[i].transform.localEulerAngles.y > 180? -(360 - pathPoints[i].transform.localEulerAngles.y) : pathPoints[i].transform.localEulerAngles.y,
            pathPoints[i].transform.localEulerAngles.z > 180? -(360 - pathPoints[i].transform.localEulerAngles.z) : pathPoints[i].transform.localEulerAngles.z
          );
        }
      }
      else if(playerReady)
      {
        for (int i = 0; i < pathPoints.Length; i += 1)
        {
          positions[i] = pathPoints[i].transform.position;
          rotations[i] = new Vector3(
            pathPoints[i].transform.localEulerAngles.x > 180? -(360 - pathPoints[i].transform.localEulerAngles.x) : pathPoints[i].transform.localEulerAngles.x,
            pathPoints[i].transform.localEulerAngles.y > 180? -(360 - pathPoints[i].transform.localEulerAngles.y) : pathPoints[i].transform.localEulerAngles.y,
            pathPoints[i].transform.localEulerAngles.z > 180? -(360 - pathPoints[i].transform.localEulerAngles.z) : pathPoints[i].transform.localEulerAngles.z
          );
        }
      }

      time = new float[positions.Length];
      time[0] = 0;

      for (int i = 1; i < positions.Length; i += 1)
      {
        time[i] = time[i - 1] + Vector3.Distance(positions[i - 1], positions[i]) / speed;
      }

      goDownstair = !goDownstair;

      for(int i = 0; i < colliders.Length; i += 1)
      {
            if (isRunning)
            {
                colliders[i].enabled = true;
            }
            else
            {
                colliders[i].enabled = false;
            }
        
      }
    }
}
