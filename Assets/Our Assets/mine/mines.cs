using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mines : MonoBehaviour
{
    private AudioSource hoeSound;
    public GameObject powder;

    bool holding = false;
    
    // Start is called before the first frame update
    void Start()
    {
        powder.SetActive(false);
        
      
        hoeSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        // If a first click detected, and still clicking,
        // measure the total click time, and fire an event
        // if we exceed the duration specified
        if (holding )
        {
            
            if (GameObject.Find("First Person Controller").GetComponent<HoeAction>().HoldEnoughTime == 3)
            {
                Debug.Log("hold enough time");
                powder.SetActive(true);
                GameObject.Find("First Person Controller").GetComponent<HoeAction>().HoldEnoughTime = 0;
                Destroy(gameObject);
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
                Debug.Log("start dig");
                holding = true;
                

        }
        
    }
}
