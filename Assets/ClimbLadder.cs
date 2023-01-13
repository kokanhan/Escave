using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadder : MonoBehaviour
{
    public GameObject pos;
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ladder")&& Input.GetKey(KeyCode.Space))
        {
            transform.position = pos.transform.position;
        }
    }
}
