using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionChecker : MonoBehaviour
{
    public Image bombInstruction;
    public Image batteryInstruction;
    public Camera camera;

    private bool isOn = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetMouseButtonDown(0))
      {
          if(isOn)
          {
            bombInstruction.gameObject.SetActive(false);
            batteryInstruction.gameObject.SetActive(false);
            isOn = false;

            //GetComponentInParent<FirstPersonMovement>().canMove = true;

            return;
          }

          Ray ray = camera.ScreenPointToRay(Input.mousePosition);
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
              isOn = true;

              //GetComponentInParent<FirstPersonMovement>().canMove = false;
            }
            else if(hit.collider.name == "BatteryPaper")
            {
              batteryInstruction.gameObject.SetActive(true);
              isOn = true;

              //GetComponentInParent<FirstPersonMovement>().canMove = false;
            }
          }
      }
    }
}
