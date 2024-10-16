using UnityEngine;
//Made by Rama, modified by others
//I've added this using in so now this class is spelled correctly and you can use UCharacterController when referring to the Unity class
using UCharacterController = UnityEngine.CharacterController;

public class CharacterController : MonoBehaviour
{
    //Variables
    public float movementSpeed = 5f; //Speed of character movement
    public float JumpHeight = 2f;    //Height the character will jump

    //Falling and gravity
    public float fallGravityMultiplier = 2f; //Extra gravity force applied when falling

    //Input values for movement and jumping
    private float forwardInputValue; //Input for moving 
    private float strafeInputValue;  //Input for strafing 
    private bool jumpInput;          //Input for jumping

    //Gravity and velocity settings
    private float terminalVelocity = 53f; //Max falling speed
    private float verticalVelocity;       //Current velocity in the y-axis

    //Reference to Unitys CharacterController component
    private UCharacterController characterController;

    void Awake()
    {
        //Grab the CharacterController component attached to this GameObject
        characterController = GetComponent<UCharacterController>();
    }

    void Update()
    {
        //Get raw input for movement and jumping each frame
        forwardInputValue = Input.GetAxisRaw("Vertical"); //W/S or Up/Down arrow keys
        strafeInputValue = Input.GetAxisRaw("Horizontal"); //A/D or Left/Right arrow keys
        jumpInput = Input.GetButtonDown("Jump"); //Spacebar to jump

        //Calls Movement and JumpAndGravity every frame
        Movement();
        JumpAndGravity();
    }

    //Method to handle character movement
    void Movement()
    {
        //Calculate movement direction based on player input and apply movement speed
        Vector3 direction = (transform.forward * forwardInputValue
                            + transform.right * strafeInputValue).normalized
                            * movementSpeed * Time.deltaTime;

        //Apply vertical velocity (for jumping and falling)
        direction += Vector3.up * verticalVelocity * Time.deltaTime;

        //Move the character using CharacterController's built-in Move() function
        characterController.Move(direction);
    }

    //Method to handle jumping and gravity application
    void JumpAndGravity()
    {
        //Check if the character is grounded
        if (characterController.isGrounded)
        {
            //Reset vertical velocity when grounded (to prevent small fall glitches)
            if (verticalVelocity < 0.0f)
            {
                verticalVelocity = -2f;
            }

            //Handle jumping input and calculate the jump velocity
            if (jumpInput)
            {
                verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y);
            }
        }
        else
        {
            //If not grounded, apply gravity (clamping to terminal velocity)
            if (verticalVelocity < terminalVelocity)
            {
                //Apply additional gravity when falling (to speed up the fall)
                float gravityMultiplier = 1;
                if (characterController.velocity.y < -1)
                {
                    gravityMultiplier = fallGravityMultiplier;
                }
                //Increase vertical velocity based on gravity and fall multiplier
                verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
            }
        }
    }
}