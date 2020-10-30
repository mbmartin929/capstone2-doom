using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce;
    public float movementSpeed = 1f;

    public float dashForce = 5f;
    public float dashDuration = 0.4f;
    public float smashForce = 5f;
    public float smashDuration = 0.5f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [SerializeField] private Vector3 velocity;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float jumpRaycastDistance = 1.0f;

    private bool isDashing;
    [SerializeField] private int dashAmount = 0;
    [SerializeField] private int smashAmount = 0;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isDashing = false;
        // defaultGravity = gravity;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity;

        // isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // if (isGrounded && velocity.y < 0)
        // {
        //     velocity.y = -2f;
        // }

        // if (!isGrounded)
        // {
        //     currentSpeed = jumpSpeed;
        // }
        // else if (isGrounded)
        // {
        //     currentSpeed = baseSpeed;

        // }



        // if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        // {
        //     Debug.Log("Jump");
        //     velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        // }

        // if (Input.GetKeyDown(KeyCode.LeftAlt) && !isGrounded)
        // {
        //     Debug.Log("Dash");
        //     StartCoroutine(Dash());
        // }

        // velocity.y += gravity * Time.deltaTime;

        //CharacterControllerMove();
        //RigidbodyMove();

        // Jump();
        // Dash();
        // Smash();
    }

    private void FixedUpdate()
    {
        RigidbodyMove();
    }

    private void RigidbodyMove()
    {
        if (IsGrounded())
        {
            float hAxis = Input.GetAxisRaw("Horizontal");
            float vAxis = Input.GetAxisRaw("Vertical");
            Vector3 movement = new Vector3(hAxis, 0, vAxis) * movementSpeed * Time.fixedDeltaTime;
            Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement);
            rb.MovePosition(newPosition);
        }
        else if (!IsGrounded())
        {
            float hAxis = Input.GetAxisRaw("Horizontal");
            float vAxis = Input.GetAxisRaw("Vertical");
            Vector3 movement = new Vector3(hAxis, 0, vAxis) * (movementSpeed - 4.0f) * Time.fixedDeltaTime;
            Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement);
            rb.MovePosition(newPosition);
        }
    }

    private IEnumerator StartSmash()
    {
        if (smashAmount == 1)
        {
            rb.mass = 0f;

            smashAmount--;
            rb.useGravity = false;
            yield return new WaitForSeconds(smashDuration);

            rb.velocity = Vector3.zero;
            rb.AddForce(transform.up * -dashForce, ForceMode.VelocityChange);
            rb.mass = 1f;
            rb.useGravity = true;
        }
    }

    private void Smash()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(StartSmash());
        }
    }

    // private IEnumerator FinishSmash()
    // {

    // }

    #region  Dash
    private void Dash()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                Debug.Log("Pressed FORWARD DASH");
                if (!IsGrounded())
                {
                    StartCoroutine(DashForward());
                }
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                if (!IsGrounded())
                {
                    Debug.Log("Pressed BACKWARD DASH");
                    StartCoroutine(DashBackward());
                }
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                Debug.Log("Pressed LEFT DASH");
                if (!IsGrounded())
                {
                    StartCoroutine(DashLeft());
                }
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                Debug.Log("Pressed RIGHT DASH");
                if (!IsGrounded())
                {
                    StartCoroutine(DashRight());
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (!IsGrounded())
            {
                Debug.Log("Pressed DEFAULT DASH");
                StartCoroutine(DashForward());
            }
        }
    }

    private IEnumerator DashBackward()
    {
        if (dashAmount == 1)
        {
            rb.AddForce(Camera.main.transform.forward * -dashForce, ForceMode.VelocityChange);

            // Debug.Log("X" + (Camera.main.transform.forward * -dashForce).x);
            // Debug.Log("Y" + (Camera.main.transform.forward * -dashForce).y);
            // Debug.Log("Z" + (Camera.main.transform.forward * -dashForce).z);

            dashAmount--;
            yield return new WaitForSeconds(dashDuration);
            rb.velocity = Vector3.zero;
        }
    }

    private IEnumerator DashForward()
    {
        if (dashAmount == 1)
        {

            rb.AddForce(Camera.main.transform.forward * dashForce, ForceMode.VelocityChange);
            dashAmount--;
            yield return new WaitForSeconds(dashDuration);
            rb.velocity = Vector3.zero;
        }
    }

    private IEnumerator DashLeft()
    {
        if (dashAmount == 1)
        {

            rb.AddForce(Camera.main.transform.right * -dashForce, ForceMode.VelocityChange);
            dashAmount--;
            yield return new WaitForSeconds(dashDuration);
            rb.velocity = Vector3.zero;
        }
    }

    private IEnumerator DashRight()
    {
        if (dashAmount == 1)
        {

            rb.AddForce(Camera.main.transform.right * dashForce, ForceMode.VelocityChange);
            dashAmount--;
            yield return new WaitForSeconds(dashDuration);
            rb.velocity = Vector3.zero;
        }
    }
    #endregion 

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Pressed JUMP");
            if (IsGrounded())
            {
                dashAmount = 1;
                smashAmount = 1;
                rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            }
        }
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position, Vector3.down * jumpRaycastDistance, Color.blue);
        return Physics.Raycast(transform.position, Vector3.down, jumpRaycastDistance);

        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
}
