using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mines : MonoBehaviour
{
    private AudioSource hoeSound;
    public GameObject powder;
    float totalDownTime = 0;
    bool holding = false;
    bool HoldEnoughTime;
    // Start is called before the first frame update
    void Start()
    {
        powder.SetActive(false);
        HoldEnoughTime = false;
        totalDownTime = 0;
        hoeSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        // If a first click detected, and still clicking,
        // measure the total click time, and fire an event
        // if we exceed the duration specified
        if (holding && Input.GetKey(KeyCode.E))
        {
            if (!hoeSound.isPlaying)
            {
                hoeSound.Play();
            } 
            totalDownTime += Time.deltaTime;
            Debug.Log("timer:"+ totalDownTime);
            if (totalDownTime >= 3f)
            {
                HoldEnoughTime = true;
                Debug.Log("Long holding");
                totalDownTime = 0f;
            }
            if (HoldEnoughTime)
            {
                powder.SetActive(true);
                HoldEnoughTime = false;
                Destroy(gameObject);
            }
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "HoeHead")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("start dig");
                holding = true;
                
            }
            //if (HoldEnoughTime)
            //{
            //    powder.SetActive(true);
            //    HoldEnoughTime = false;
            //    Destroy(gameObject);
            //}

        }
    }
}
