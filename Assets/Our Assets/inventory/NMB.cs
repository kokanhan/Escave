using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMB : MonoBehaviour
{
    Item item;
    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        Destroy(gameObject);
    }
    public void AddItem(Item newItem)
    {
        item = newItem;
    }
}
