using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

  
    Vector2 velocity;
    Vector2 frameVelocity;
    public bool inGame;

    //这样镜头才是瞄准了开头的方向
    private float x = 2.12f;
    private float y = 160f;

    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        // Lock the mouse cursor to the game screen.
        //Cursor.lockState = CursorLockMode.Locked;

        inGame = false;


    }

    void Update()
    {
        if(!GetComponentInParent<FirstPersonMovement>().canMove)
        {
          return;
        }


        // // Get smooth velocity.
        // Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        // Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        // frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        // velocity += frameVelocity;
        // velocity.y = Mathf.Clamp(velocity.y, -90, 90);
        //
        // // Rotate camera up-down and controller left-right from velocity.
        // transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        // character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
        if(inGame)
        {
            transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            //Debug.Log("x is " + x + "y is " + y);
            x -= Input.GetAxis("Mouse Y") * 1.25f;
            x = Mathf.Clamp(x, -90, 90);
            y += Input.GetAxis("Mouse X") * 1.25f;

            transform.localEulerAngles = new Vector3(x, 0, 0);
            transform.parent.transform.localEulerAngles = new Vector3(0, y, 0);
        }
        
    }

    //private float x = 0;
    //private float y = 0;

    public void forceUpdate()
    {
        x = transform.localEulerAngles.x;
        y = transform.parent.transform.localEulerAngles.y;
    }
}
