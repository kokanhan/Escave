using System.Collections;
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
          if(isOn)
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

          if (Physics.Raycast(ray, out hit))
          {
            if(Vector3.Distance(hit.transform.position, transform.position) > 2.5f)
            {
              return;
            }

            if(hit.collider.name == "BombPaper")
            {
              bombInstruction.gameObject.SetActive(true);
              bombObj.SetActive(false);
              marker.gameObject.SetActive(false);
              isOn = true;

              //GetComponentInParent<FirstPersonMovement>().canMove = false;
            }
            else if(hit.collider.name == "BatteryPaper")
            {
              batteryInstruction.gameObject.SetActive(true);
              batteryObj.SetActive(false);
              marker.gameObject.SetActive(false);
              isOn = true;

              //GetComponentInParent<FirstPersonMovement>().canMove = false;
            }
          }
      }
    }
}
