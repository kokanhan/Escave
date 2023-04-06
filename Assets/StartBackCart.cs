using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBackCart : MonoBehaviour
{
    public static StartBackCart Instance;
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
            cartModel.SetActive(false);
            playerOnCart.SetActive(true);
        }

    }
    private void Update()
    {

        coroutine = GetOutCart(2.0f);
        StartCoroutine(coroutine);//without coroutine, this would be disaster


    }

    private IEnumerator GetOutCart(float v)
    {
        if (arrival == true && Input.GetKey(KeyCode.R)&& playerOnCart.activeInHierarchy)
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
