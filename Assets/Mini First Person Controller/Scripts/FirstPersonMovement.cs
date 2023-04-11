using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    public bool canMove;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    public GameObject hoe;

    public bool hoePicked;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    GameObject backpack;

    void Start()
    {
        canMove = true;
        hoePicked = false;
    }

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
        backpack = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            rigidbody.velocity = new Vector3();
            rigidbody.angularVelocity = new Vector3();
            return;
        }

        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        //Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);
        Vector2 targetVelocity = new Vector2();

        if (Input.GetKey(KeyCode.W))
        {
            targetVelocity = new Vector2(targetVelocity.x, targetVelocity.y + targetMovingSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            targetVelocity = new Vector2(targetVelocity.x - targetMovingSpeed, targetVelocity.y);
        }

        if (Input.GetKey(KeyCode.S))
        {
            targetVelocity = new Vector2(targetVelocity.x, targetVelocity.y - targetMovingSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            targetVelocity = new Vector2(targetVelocity.x + targetMovingSpeed, targetVelocity.y);
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (hoePicked == true)
            {
                //hoeSwtich();
                StartCoroutine(hoeSwtich());
            }
        }

        if (Input.GetKey(KeyCode.B))
        {
            //backpackSwitch();
            StartCoroutine(backpackSwitch());
        }
        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }

    IEnumerator backpackSwitch()
    {

        if (backpack.activeInHierarchy)
        {
            yield return new WaitForSeconds(0.3f);
            //backpack.SetActive(false);//gameObject.GetComponent<Button>().onClick.Invoke(); 
            backpack.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.Invoke();//close

        }
        else
        {
            yield return new WaitForSeconds(0.3f);

            GameObject.Find("Canvas").transform.GetChild(1).gameObject.GetComponent<Button>().onClick.Invoke();//open
            backpack.SetActive(true);
            //yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator hoeSwtich()
    {
        if (hoe.activeInHierarchy)
        {
            yield return new WaitForSeconds(0.5f);
            hoe.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            hoe.SetActive(true);
        }
    }


    void playRound()
    {
        StartCoroutine(PlayRoundRoutine());
    }

    IEnumerator PlayRoundRoutine()
    {

        yield return new WaitForSeconds(2f);

    }
}

