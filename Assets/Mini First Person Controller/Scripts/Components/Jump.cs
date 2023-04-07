using UnityEngine;
using UnityEngine.UI;
public class Jump : MonoBehaviour
{
    Rigidbody rigidbody;
    public float jumpStrength = 2;
    public event System.Action Jumped;

    GameObject backpack;

    void Start()
    {
        // Get rigidbody.
        rigidbody = GetComponent<Rigidbody>();


        //backpack = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
    }

    void LateUpdate()
    {
        // Jump when the Jump button is pressed and we are on the ground.
        if (Input.GetButtonDown("Jump") && GetComponent<GroundCheck>().isGrounded)
        {
            rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
        }

        //if (Input.GetKey(KeyCode.B))
        //{
        //    backpackSwitch();
        //}
    }

    //void backpackSwitch()
    //{
    //    if (backpack.activeSelf)
    //    {
    //        //backpack.SetActive(false);//gameObject.GetComponent<Button>().onClick.Invoke();
    //        backpack.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.Invoke();

    //    }
    //    else
    //    {
    //        backpack.SetActive(true);
    //        GameObject.Find("Canvas").transform.GetChild(1).gameObject.GetComponent<Button>().onClick.Invoke();
    //    }
    //}
}
