﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionChecker : MonoBehaviour
{
    public GameObject marker;

    public Image bombInstruction;
    public Image batteryInstruction;

    public Camera camera;

    private bool isOn = false;

    public GameObject bombObj;
    public GameObject batteryObj;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isOn)
            {
                bombInstruction.gameObject.SetActive(false);
                batteryInstruction.gameObject.SetActive(false);

                bombObj.SetActive(true);
                batteryObj.SetActive(true);
                marker.gameObject.SetActive(true);

                isOn = false;

                //GetComponentInParent<FirstPersonMovement>().canMove = true;

                return;
            }

            Ray ray = camera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);
            
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Distance is " + Vector3.Distance(hit.transform.position, transform.position));
                Debug.Log("hit.collider.name is "+hit.collider.name);
                if (Vector3.Distance(hit.transform.position, transform.position) > 2.8f)// && hit.collider.name != "BombPaper"
                {
                   // Debug.Log("wtf");
                    return;
                }

                else if (hit.collider.name == "BombPaper")
                {
                    Debug.Log("hitbomb");
                    bombInstruction.gameObject.SetActive(true);
                    bombObj.SetActive(false);
                    marker.gameObject.SetActive(false);
                    isOn = true;

                    //GetComponentInParent<FirstPersonMovement>().canMove = false;
                }
                else if (hit.collider.name == "BatteryPaper")
                {
                    batteryInstruction.gameObject.SetActive(true);
                    batteryObj.SetActive(false);
                    marker.gameObject.SetActive(false);
                    isOn = true;

                    //GetComponentInParent<FirstPersonMovement>().canMove = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = camera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);

            if (Physics.Raycast(ray, out hit))
            {
                if (Vector3.Distance(hit.transform.position, transform.position) > 2.8f)// && hit.collider.name != "BombPaper"
                {
                    return;
                }
                else
                {
                    //ItemPickUp();//已经写好了
                }
                
            }
                
        }
    }

}
