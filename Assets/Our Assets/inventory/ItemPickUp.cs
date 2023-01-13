using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    public GameObject hoeInHand;
    void Pickup()
    {
        InventoryManager.Instance.Add(item);
        hoeInHand.SetActive(true);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Pickup();
    }

}
