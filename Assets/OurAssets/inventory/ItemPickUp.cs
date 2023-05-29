using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    public GameObject hoeInHand;
    
    public void Pickup()
    {
        InventoryManager.Instance.Add(item);
        hoeInHand.SetActive(true);
        GameObject.Find("First Person Controller").GetComponent<FirstPersonMovement>().hoePicked = true;
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Pickup();
    }

}
