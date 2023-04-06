using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCart : MonoBehaviour
{
    public static StartCart Instance;
    public GameObject player;
    public GameObject playerOnCart;
    public GameObject cartModel;

    public GameObject DestCart;
    public bool arrival;


    private IEnumerator coroutine;

    void Awake()
    {
        Instance = this;
        arrival = false;
        DestCart.SetActive(false);
        cartModel.SetActive(true);

    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.R))
        {
            //player.SetActive(false);
            cartModel.SetActive(false);
            playerOnCart.SetActive(true);
        }
        
    }
    private void Update()
    {
        coroutine = GetOutCart(2.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator GetOutCart(float v)
    {
        if (arrival == true && Input.GetKey(KeyCode.R) && playerOnCart.activeInHierarchy)
        {
            Debug.Log("Go Bot!");
            player.SetActive(true);

            playerOnCart.SetActive(false);
            DestCart.SetActive(true);

            player.SetActive(true);
            arrival = false;
            yield return new WaitForSeconds(v);
            

        }


    }
}
