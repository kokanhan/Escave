using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMainCam : MonoBehaviour
{
    GameObject originalPlayerObject;
    GameObject playerCam;
    public GameObject minerTalk;

    public float sec2Wait = 7f;
    // Start is called before the first frame update
    void Start()
    {
        minerTalk.SetActive(false);
        //Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(LateCall(sec2Wait));
        originalPlayerObject = GameObject.Find("First Person Controller");
        playerCam = originalPlayerObject.transform.GetChild(0).gameObject;
        originalPlayerObject.GetComponent<Rigidbody>().isKinematic = true;
        playerCam.GetComponent<AudioListener>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LateCall(float sec2Wait)
    {
        //if (gameObject.activeInHierarchy)
        //    gameObject.SetActive(false);

        //yield return new WaitForSeconds(sec2Wait);

        //gameObject.SetActive(true);
        ////Do Function here...

        if (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(sec2Wait);
            //Cursor.lockState = CursorLockMode.None;

            //设置player camera的位置和ingame true
            originalPlayerObject.transform.position = new Vector3(transform.position.x-1, transform.position.y - 1, transform.position.z - 3);
            playerCam.transform.localEulerAngles = new Vector3(0, 0, 0);
            playerCam.GetComponent<AudioListener>().enabled = true;
            playerCam.GetComponent<FirstPersonLook>().inGame = true;
            minerTalk.SetActive(true);
            //不需要设置playercontroller的角度，因为在player cam的FirstPersonLook脚本里受x,y的鼠标输入值控制
            //originalPlayerObject.transform.localEulerAngles = new Vector3(-12.263f, 158.852646f, 0);
            //originalPlayerObject.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);


            gameObject.SetActive(false);
        }
            

        

    }
}
