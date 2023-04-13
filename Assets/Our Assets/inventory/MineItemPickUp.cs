using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineItemPickUp : MonoBehaviour
{
    public Item item;
    public void Pickup()
    {
        InventoryManager.Instance.Add(item);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Pickup();
    }
}
