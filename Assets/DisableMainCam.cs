using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMainCam : MonoBehaviour
{
    public float sec2Wait = 7f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LateCall(sec2Wait));
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

            gameObject.SetActive(false);
        }
            

        

    }
}
