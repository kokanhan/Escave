using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonMovement : MonoBehaviour
{
    public GameObject testTelPos;
    public float speed = 5;

    public bool canMove;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    public GameObject hoe;

    public bool hoePicked;
    public bool digging;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    GameObject backpack;
    Camera camera;

    void Start()
    {
        canMove = true;
        hoePicked = false;
        digging = false;
    }

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();

        if(GameObject.Find("Canvas") != null)
        {
            backpack = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        }
        camera = this.transform.GetChild(0).gameObject.GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        // Debug use
        if (Input.GetKey(KeyCode.L))
        {
            this.transform.position = testTelPos.transform.position;
        }
        /////////////////////////////////////////////////////
      

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


        if (Input.GetKey(KeyCode.F))
        {
            //pick up item
            Ray ray = camera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            GameObject hitObject;
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);

            if (Physics.Raycast(ray, out hit))
            {
                if (Vector3.Distance(hit.transform.position, transform.position) > 3.0f)
                {
                    digging = false;
                    return;
                }

                if (hit.collider.name == "Hoe2Pick")
                {
                    hitObject = hit.transform.gameObject;
                    hitObject.GetComponent<ItemPickUp>().Pickup();
                    
                }
                else if (hit.collider.tag == "Mine")
                {
                    hitObject = hit.transform.gameObject;
                    hitObject.GetComponent<MineItemPickUp>().Pickup();
                    
                }
                else if (hit.collider.name == "Battery2Pick")
                {

                    hitObject = hit.transform.gameObject;
                    hitObject.GetComponent<MineItemPickUp>().Pickup();
                   
                }
                else if (hit.collider.name == "bomb")
                {

                    hitObject = hit.transform.gameObject;
                    hitObject.GetComponent<MineItemPickUp>().Pickup();
                }
                if (Vector3.Distance(hit.transform.position, transform.position) < 3f && (hit.collider.tag == "ChaMine2Dig"|| hit.collider.tag == "NiMine2Dig"|| hit.collider.tag == "SuMine2Dig"))//目前还不知道射线的最佳距离是多少 暂且设定2f
                {
                    //开挖
                    gameObject.GetComponent<HoeAction>().MineName = hit.collider.tag;
                    if(hit.collider != null)
                    {
                        digging = true;
                        gameObject.GetComponent<HoeAction>().hintLayout.SetActive(true);
                        
                    }
                    else
                    {
                        digging = false;
                    }
                    
                }
                //else if(Vector3.Distance(hit.transform.position, transform.position) > 2.5f)
                //{
                //    digging = false;
                //}
                else if(hit.collider.tag != "ChaMine2Dig" || hit.collider.tag != "NiMine2Dig" || hit.collider.tag != "SuMine2Dig")
                {
                    digging = false;
                    gameObject.GetComponent<HoeAction>().isIn = false;
                }

            }
        }
    }
    
    public bool returnDigging()
    {
        return digging;
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
            yield return new WaitForSeconds(0.3f);
            hoe.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
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
