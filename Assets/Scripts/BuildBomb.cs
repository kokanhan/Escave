using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBomb : MonoBehaviour
{
    public Item item;
    public List<Item> NewItems = new List<Item>();


    public int BombProgress=0;
    //public int coalMat;
    //public int sulfurMat;
    //public int niterMat;


    public GameObject bomb;

    public NMB[] InventoryItems;
    public void AddMaterials(int value)
    {
        //BombProgress += value;

        BombProgress += 1;
        if (BombProgress >= 3)
        {
            bomb.SetActive(true);
            Destroy(gameObject);
        }
    }

    public void RemoveItem(Item item)
    {

        // call the script of item prefab that destory itself
        // get InventoryItems from Inventory manager
        InventoryManager.Instance.Remove(item);

    }

    public void UseItem() {
        foreach (var item in NewItems)
        {
            switch (item.itemType)
            {
                case Item.ItemType.Mine:
                    Debug.Log("You get Mine!");
                    AddMaterials(item.value);
                    RemoveItem(item);
                    break;
            }
        }
        
        //    if (){//check type is mine
        //        AddMaterials(item.value);
        //    }
        //    RemoveItem();
    }

    //write a function that when the player press R, call UseItem
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("hey!");
        NewItems = InventoryManager.Instance.Items;

        if (Input.GetKey(KeyCode.R))
        {
            Debug.Log("create bomb");
            UseItem();

        }
    }
}
