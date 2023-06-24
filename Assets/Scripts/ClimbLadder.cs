using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadder : MonoBehaviour
{
    public GameObject camera;

    private bool isIn;
    private bool isDownstair;
    private bool isTurning;
    private bool isClimbing;

    private Vector3 exitRotation = new Vector3(19.419f, 144.723f, 0f);
    private Vector3 exitStart = new Vector3(-57.19f, 155.15f, 12.22f);//面向的开始位置
    private Vector3 exitEnd = new Vector3(-54.47f, 167.5f, 8.38f);//面向的结束位置

    private Vector3 rotation = new Vector3(19.5f, 12.83f, 0f);
    private Vector3 start = new Vector3(-83.44f, 140.68f, 121.67f);
    private Vector3 end = new Vector3(-82.95f, 146.23f, 121.67f);

    private float a;

    private float turningCurTime;
    private float turningTime;
    private Vector3 turningV1;

    private float turningSpeed = 30;
    private float turningMaxTime = 2.5f;
    private float climbingSpeed = 0.25f;
    private bool Exitladder;



    void Update()
    {
        //假如已经在梯子上并且快到达顶部，就不进行if内操作
        //if (isIn && !isClimbing && Input.GetKey(KeyCode.W))
        if (isIn && !isClimbing && Input.GetKey(KeyCode.F))
        {
            GetComponent<FirstPersonMovement>().canMove = false;
            isTurning = true;
            if (Exitladder == true)
            {
                initExitTurningParameter();
            }
            if (Exitladder == false)
            {
                initTurningParameter();
            }


            if (isDownstair)
            {
                a = 0;
            }
            else
            {
                a = 1;
            }
        }

        if (isTurning && Exitladder == true)
        {
            camera.transform.localEulerAngles = new Vector3(Mathf.Lerp(turningV1.x, exitRotation.x, turningCurTime / turningTime), 0, 0);
            transform.localEulerAngles = new Vector3(0, Mathf.Lerp(turningV1.y, exitRotation.y, turningCurTime / turningTime), 0);//转动玩家身体，因为脚本绑在player controller身上

            if (isDownstair)
            {
                transform.position = Vector3.Lerp(transform.position, exitStart, turningCurTime / turningTime);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, exitEnd, turningCurTime / turningTime);
            }

            camera.GetComponent<FirstPersonLook>().forceUpdate();

            turningCurTime += Time.deltaTime;

            if (turningCurTime >= turningTime)
            {
                isTurning = false;
                isClimbing = true;
            }
        }


        if (isTurning && Exitladder == false)
        {
            camera.transform.localEulerAngles = new Vector3(Mathf.Lerp(turningV1.x, rotation.x, turningCurTime / turningTime), 0, 0);
            transform.localEulerAngles = new Vector3(0, Mathf.Lerp(turningV1.y, rotation.y, turningCurTime / turningTime), 0);

            if (isDownstair)
            {
                transform.position = Vector3.Lerp(transform.position, start, turningCurTime / turningTime);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, end, turningCurTime / turningTime);
            }

            camera.GetComponent<FirstPersonLook>().forceUpdate();

            turningCurTime += Time.deltaTime;

            if (turningCurTime >= turningTime)
            {
                isTurning = false;
                isClimbing = true;
            }
        }



        if (isClimbing && Exitladder == true)
        {
            camera.transform.localEulerAngles = new Vector3(exitRotation.x, 0, 0);
            transform.localEulerAngles = new Vector3(0, exitRotation.y, 0);
            camera.GetComponent<FirstPersonLook>().forceUpdate();

            if (Input.GetKey(KeyCode.W))
            {
                a += climbingSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.S))
            {
                a -= climbingSpeed * Time.deltaTime;
            }

            transform.position = Vector3.Lerp(exitStart, exitEnd, a);

            if (a < 0 || 1 < a)
            {
                GetComponent<FirstPersonMovement>().canMove = true;
                isClimbing = false;
            }
        }



        if (isClimbing && Exitladder == false)
        {
            camera.transform.localEulerAngles = new Vector3(rotation.x, 0, 0);
            transform.localEulerAngles = new Vector3(0, rotation.y, 0);
            camera.GetComponent<FirstPersonLook>().forceUpdate();

            if (Input.GetKey(KeyCode.W))
            {
                a += climbingSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.S))
            {
                a -= climbingSpeed * Time.deltaTime;
            }

            transform.position = Vector3.Lerp(start, end, a);

            if (a < 0 || 1 < a)
            {
                GetComponent<FirstPersonMovement>().canMove = true;
                isClimbing = false;
            }
        }


    }

    private void initExitTurningParameter()
    {
        turningCurTime = 0;
        turningTime = Mathf.Min(Mathf.Abs(transform.localEulerAngles.y - exitRotation.y) / turningSpeed, turningMaxTime);
        turningV1 = new Vector3(camera.transform.localEulerAngles.x > 180 ? -(360 - camera.transform.localEulerAngles.x) : camera.transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
    }

    private void initTurningParameter()
    {
        turningCurTime = 0;
        turningTime = Mathf.Min(Mathf.Abs(transform.localEulerAngles.y - rotation.y) / turningSpeed, turningMaxTime);
        turningV1 = new Vector3(camera.transform.localEulerAngles.x > 180 ? -(360 - camera.transform.localEulerAngles.x) : camera.transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ExitLadderStart")
        {
            isIn = true;
            isDownstair = true;
            Exitladder = true;
        }
        else if (other.tag == "ExitLadderEnd")
        {
            isIn = true;
            isDownstair = false;
            Exitladder = true;
        }
        else if (other.tag == "LadderStart")
        {
            isIn = true;
            isDownstair = true;
            Exitladder = false;
        }
        else if (other.tag == "LadderEnd")
        {
            isIn = true;
            isDownstair = false;
            Exitladder = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "ExitLadderStart" || other.tag == "LadderStart")
        {
            isIn = false;
            Debug.Log("not in");
        }
        else if (other.tag == "ExitLadderEnd" || other.tag == "LadderEnd")
        {
            isIn = false;
            //Debug.Log("not in");
        }
    }
}
