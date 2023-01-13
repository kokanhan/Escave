using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    //To-do:
    // 1. Wirte a function to change the FPS's camera to a position, so player can see the rocks are blasted by the bomb
    // 2. May create an animation that rocks falling down, play the animation after set the rocks objects are disabled

    private ParticleSystem ps;
    private List<Item> NewItems = new List<Item>();
    public GameObject[] rocks;
    public GameObject bomb;
    private bool SetUp;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        SetUp = false;
    }

    void BlastRock()
    {
        ps.Play();
        Debug.Log("why?");
        //if (!ps.isPlaying)//no need for now
       // {
            Debug.Log("now you should blast");
            foreach (var rock in rocks)
            {
                rock.SetActive(false);
            }
            //set rocks disabled

            Destroy(gameObject,3f);
       // }
    }

    public void RemoveItem(Item item)
    {

        // call the script of item prefab that destory itself
        // get InventoryItems from Inventory manager
        InventoryManager.Instance.Remove(item);

    }
    public void SetUpBomb()
    {
        foreach (var item in NewItems)
        {
            switch (item.itemType)
            {
                case Item.ItemType.Bomb:
                    Debug.Log("You get Bomb!");
                    //bomb.SetActive(true);
                    RemoveItem(item);
                    break;
            }
        }
;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("the bomb is on the ground or you are getting too close!");

        if (Input.GetKey(KeyCode.R)&& other.tag=="Bomb")
        {
            BlastRock();
        }

        if (Input.GetKey(KeyCode.P))
        {
            Debug.Log("place bomb!");//this is not real set up, doen't check the inventory
            bomb.SetActive(true);
            SetUp = true;
            NewItems = InventoryManager.Instance.Items;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R) && SetUp)
        {
            
            BlastRock();
        }
    }

}
