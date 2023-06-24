using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallRock : MonoBehaviour
{
    AudioSource audioSource;


    public float upPower = 5.0F;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = transform.parent.transform.gameObject.GetComponent<AudioSource>();
        //给rigidbody body 施加一个力，让它向四周散开

        Vector3 explosionPos = transform.position;
        Collider colliders = gameObject.GetComponent<Collider>();
        
            Rigidbody rb = colliders.GetComponent<Rigidbody>();

        //if (rb != null)
        //    Debug.Log("rb!= null");
        // rb.AddForce(Vector3.forward * fasan, ForceMode.Impulse);

        //Vector3 force = transform.forward;
        //force = new Vector3(force.x, 1, force.z);
        //rb.AddForce(force * 10f);
        //rb.AddForce(Vector3.up * upPower, ForceMode.Impulse);
        rb.AddForce(new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f))*Random.Range(100,200));

        //rb.AddForce(new Vector3(0,1f,, ForceMode.Impulse);


    }

    // Update is called once per frame
    void Update()
    {
        
       
    }


    void OnCollisionEnter(Collision collision)
    {
        //碰到地面 发出声音
        //如果parent的auidoSource正在播放就不发声音
        if (collision.gameObject.tag == "Ground" && !audioSource.isPlaying)
        {
            Debug.Log("Hit Ground");
            //if (!audioSource.isPlaying)
            //{
                audioSource.Play();
            //}
        }
     
    }
}
