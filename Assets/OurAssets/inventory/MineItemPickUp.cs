using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineItemPickUp : MonoBehaviour
{
    public Material lightMat;

    GameObject parentObj;
    GameObject light;
    Material[] matCopy;

    public Item item;
    public void Pickup()
    {
        InventoryManager.Instance.Add(item);
        if(item.itemName == "Battery")
        {
            
            parentObj = GameObject.Find("BotLevelObjects");
            light = parentObj.transform.GetChild(1).gameObject;
            matCopy = light.GetComponent<Renderer>().materials;
            //matCopy[0] = Resources.Load("/Our Assets/Light/M_LightTower_NoLight.mat", typeof(Material)) as Material;
            //matCopy[0] = Resources.Load<Material>("OurAssets/Light/M_LightTower_NoLight");
            matCopy[0] = lightMat;
            light.GetComponent<Renderer>().materials = matCopy;


        }
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Pickup();
    }
}
