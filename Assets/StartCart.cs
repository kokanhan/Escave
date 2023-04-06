using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCart : MonoBehaviour
{
  private bool isIn;
  public Camera camera;

  void Update()
  {
    if(Input.GetKeyDown(KeyCode.R))
    {
      Ray ray = camera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit))
      {
        if(Vector3.Distance(hit.transform.position, camera.transform.position) > 5.0f)
        {
          return;
        }


        if(hit.collider.tag == "Go")
        {
          GetComponent<railFollow>().startCartNow(isIn);
        }
      }
    }
  }

  void OnTriggerEnter(Collider other)
  {
      if (other.CompareTag("Player"))
      {
          isIn = true;
      }
  }

  void OnTriggerExit(Collider other)
  {
      if (other.CompareTag("Player"))
      {
          isIn = false;
      }
  }
}
