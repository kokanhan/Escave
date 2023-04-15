using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPlacement : MonoBehaviour
{
    public Item item;
    public List<Item> NewItems = new List<Item>();

    public NMB[] InventoryItems;//script reference

    public GameObject Battery;
    Renderer LightRenderer;
    Color noBatColor = new Color(0.98f, 0.086f, 0.086f, 1.0f);
    Color BatColor = new Color(0.0f, 0.96f, 0.02f, 0.81f);

    // Start is called before the first frame update
    void Awake()
    {
        Battery.SetActive(false);
        LightRenderer = this.transform.parent.Find("BatteryLight").GetComponent<Renderer>();

        //LightRenderer.material.SetColor("_Color", noBatColor);
        LightRenderer.material.color = noBatColor;
        this.transform.parent.Find("BatteryLight").GetComponentInChildren<Light>().color = noBatColor;

        if (GameObject.Find("MidLevelObjects") != null)
        {
            Debug.Log("yeah!");
        }
        if (GameObject.Find("MidLevelObjects").transform.Find("railDeviceBack") != null)
        {
            Debug.Log("find back!!");
        }
        if (GameObject.Find("MidLevelObjects").transform.Find("railDeviceBack").transform.Find("BatteryLight") != null)
        {
            Debug.Log("wtf back!!");
        }
    }
    public void PlaceBattery()
    {
            Battery.SetActive(true);
            LightRenderer.material.color = BatColor;
            this.transform.parent.Find("BatteryLight").GetComponentInChildren<Light>().color = BatColor;
            GameObject temp = GameObject.Find("MidLevelObjects").transform.Find("railDeviceBack").gameObject;
            temp.transform.Find("BatteryLight").GetComponentInChildren<Light>().color = BatColor;
       // GameObject.Find("MidLevelObjects").transform.Find("railDeviceBack").transform.Find("BatteryLight").GetComponentInChildren<Light>().color = BatColor;
        
        //railDeviceBack

    }
    public void RemoveItem(Item item)
    {

        // call the script of item prefab that destory itself
        // get InventoryItems from Inventory manager
        InventoryManager.Instance.Remove(item);

    }

    public void UseBatteryItem()
    {
        foreach (var item in NewItems)
        {
            switch (item.itemType)
            {
                case Item.ItemType.NanfuBattery:
                    Debug.Log("You get Battery!");
                    PlaceBattery();
                    RemoveItem(item);
                    break;
            }
        }
        //Destroy(gameObject);

    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("hey!");
        NewItems = InventoryManager.Instance.Items;

        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("hitcollier Bat!");
            UseBatteryItem();

        }
    }
}
