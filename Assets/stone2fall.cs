using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stone2fall : MonoBehaviour
{
    public float time2Appear;
    public GameObject[] stoneWall;
    public AudioSource audioSource;
    public AudioClip clip1;
    public AudioClip clip2;

    private float myTimer = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (GameObject st in stoneWall)
        {
            st.SetActive(false);
        }
    }

    void Start()
    {
        //audioSource.Play();
        audioSource.PlayOneShot(clip1, 0.65F);
        audioSource.PlayOneShot(clip2, 1F);
    }

    // Update is called once per frame
    void Update()
    {
        myTimer += Time.deltaTime;
        if(myTimer >= time2Appear)
        {
            foreach (GameObject st in stoneWall)
            {
                st.SetActive(true);
            }
        }
        if (!audioSource.isPlaying && myTimer >= 7f)
        {
            Destroy(gameObject);
        }
            //Object.Destroy(gameObject, 5.0f);
    }
}
