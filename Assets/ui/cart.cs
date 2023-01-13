using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cart : MonoBehaviour
{
    public GameObject txt;
    // Start is called before the first frame update
    void Start()
    {
        txt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");

        if (other.tag == "Player")
        {
            Debug.Log("Text Triggered");
            txt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        txt.SetActive(false);
    }
}
