using UnityEngine;

//I've added this using in so now this class is spelled correctly and you can use UCharacterController when referring to the Unity class
using UCharacterController = UnityEngine.CharacterController;

public class CharacterController : MonoBehaviour
{
    //variables

     
    public float movementSpeed = 5f;
    public float JumpHeight = 2f;
   
    //falling
    public float fallGravityMultiplier = 2f;

    private float forwardInputValue;
    private float strafeInputValue;
    private bool jumpInput;

    private float terminalVelocity = 53f;
    private float verticalVelocity;

    private UCharacterController characterController;

    void Awake()
    {
        characterController = GetComponent<UCharacterController>();
    }

    void Update()
    {
        forwardInputValue = Input.GetAxisRaw("Vertical");
        strafeInputValue = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");
        Movement();
        JumpAndGravity();
    }

    void Movement()
    {
        Vector3 direction = (transform.forward * forwardInputValue
                            + transform.right * strafeInputValue).normalized
                            * movementSpeed * Time.deltaTime;
        direction += Vector3.up * verticalVelocity * Time.deltaTime;

        characterController.Move(direction);
    }

    void JumpAndGravity()
    {
        if (characterController.isGrounded)
        {
            if (verticalVelocity < 0.0f)
            {
                verticalVelocity = -2f;
            }

            if (jumpInput)
            {
                verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y);
            }
        }
        else
        {
            if (verticalVelocity < terminalVelocity)
            {
                float gravityMultiplier = 1;
                if (characterController.velocity.y < -1)
                {
                    gravityMultiplier = fallGravityMultiplier;
                }
                verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
            }
        }
    }
}
