using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombSetUpArea : MonoBehaviour
{
    bool startPlaysound = false;
    AudioSource audios;
    // Start is called before the first frame update
    void Start()
    {
        audios = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(startPlaysound == true && !audios.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    void playsound()
    {
        audios.Play();
        startPlaysound = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playsound();
        }
    }
}
