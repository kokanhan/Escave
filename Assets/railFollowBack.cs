using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class railFollowBack : MonoBehaviour
{
    public GameObject obj;
    public GameObject[] pathPoints;
    public int numberOfPoints;
    public float speed=10f;

    private Vector3 actualPosition;
    private int x;




    //Hide the player and rest its position
    [Header("======Player to Hide and destination position======")]
    public GameObject player;
    public GameObject Dest;


    AudioSource railSound;

    // Start is called before the first frame update
    void Start()
    {
        x = 1;
        PlayerTakeRide();

        
        railSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        actualPosition = obj.transform.position;
        obj.transform.position = Vector3.MoveTowards(actualPosition, pathPoints[x].transform.position, speed * Time.deltaTime);

        if (actualPosition == pathPoints[x].transform.position && x != numberOfPoints - 1)
        {
            x++;
        }

        //arrival 
        if (actualPosition == pathPoints[x].transform.position && x == numberOfPoints - 1)
        {
            Debug.Log("Hello!");
            railSound.Stop();
            StartBackCart.Instance.arrival = true;
            
        }
    }

    void PlayerTakeRide()
    {
        player.transform.position = Dest.transform.position;
        player.SetActive(false);   
    }
}
