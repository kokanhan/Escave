using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    public bool canMove;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Start()
    {
      canMove = true;
    }

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(!canMove)
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

        if(Input.GetKey(KeyCode.W))
        {
          targetVelocity = new Vector2(targetVelocity.x, targetVelocity.y + targetMovingSpeed);
        }

        if(Input.GetKey(KeyCode.A))
        {
          targetVelocity = new Vector2(targetVelocity.x - targetMovingSpeed, targetVelocity.y);
        }

        if(Input.GetKey(KeyCode.S))
        {
          targetVelocity = new Vector2(targetVelocity.x, targetVelocity.y - targetMovingSpeed);
        }

        if(Input.GetKey(KeyCode.D))
        {
          targetVelocity = new Vector2(targetVelocity.x + targetMovingSpeed, targetVelocity.y);
        }

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }
}
