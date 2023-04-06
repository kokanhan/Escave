using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class airwall : MonoBehaviour
{
    public GameObject player, airwall1, airwall2;
    // Start is called before the first frame update
    void Start()
    {
        airwall1.SetActive(true);
        airwall2.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.activeInHierarchy)
        {
            airwall1.SetActive(true);
            airwall2.SetActive(true);
        }
        else
        {
            airwall1.SetActive(false);
            airwall2.SetActive(false);
        }
    }
}
