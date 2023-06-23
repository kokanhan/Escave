using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPlacement : MonoBehaviour
{
    public Item item;
    public List<Item> NewItems = new List<Item>();

    public NMB[] InventoryItems;//script reference
    public GameObject Battery;
    public Material greenLight;
    public Material redLight;
    public Material greyLight;

    Renderer LightRenderer;

    GameObject Device;
    GameObject redlighter;
    GameObject greenlighter;
    Material[] matRedLighterCopy;// store materials and to replace them
    Material[] matGreenLighterCopy;

    Color noBatColor = new Color(0.98f, 0.086f, 0.086f, 1.0f);
    Color BatColor = new Color(0.0f, 0.96f, 0.02f, 0.81f);

    // Start is called before the first frame update
    void Awake()
    {
        //GetComponentInChildren 意思是从children object里找component
        Battery.SetActive(false);
        Device = this.transform.parent.Find("DeviceBox").gameObject;
        //Finds and assigns the child named "RedLighter".
        redlighter = Device.transform.Find("RedLighter").gameObject;
        greenlighter = Device.transform.Find("GreenLighter").gameObject;

        matRedLighterCopy = redlighter.GetComponent<MeshRenderer>().materials;
        matGreenLighterCopy = greenlighter.GetComponent<MeshRenderer>().materials;

        matRedLighterCopy[0] = redLight;
        matGreenLighterCopy[0] = greyLight;
        redlighter.GetComponent<MeshRenderer>().materials = matRedLighterCopy;
        greenlighter.GetComponent<MeshRenderer>().materials = matGreenLighterCopy;

        //if (GameObject.Find("MidLevelObjects").transform.Find("railDeviceBack").transform.Find("BatteryLight") != null)
        //{
        //    Debug.Log("wtf back!!");
        //}


    }
    public void PlaceBattery()
    {
        Battery.SetActive(true);
        matRedLighterCopy[0] = greyLight;
        matGreenLighterCopy[0] = greenLight;
        redlighter.GetComponent<MeshRenderer>().materials = matRedLighterCopy;
        greenlighter.GetComponent<MeshRenderer>().materials = matGreenLighterCopy;
        //更改light的颜色
        //LightRenderer = this.transform.parent.Find("BatteryLight").GetComponent<Renderer>();
        //LightRenderer.material.color = BatColor;
        //this.transform.parent.Find("BatteryLight").GetComponentInChildren<Light>().color = BatColor;



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
                    //Debug.Log("You get Battery!");
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
            //Debug.Log("hitcollier Bat!");
            UseBatteryItem();

        }
    }
}
