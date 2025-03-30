using UnityEngine;

public class PlayerMovementV2 : MonoBehaviour
{

    public float movementSpeed = 5f;
    private Rigidbody rBody;
    private InputHandler inputHandler;
    public new Camera camera;
    public float speedMultiplier = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateTowardsMouse();
        MovePlayer();
        Run();
    }

    private void RotateTowardsMouse()
    {
        // Casts a ray from the mouse cursor to the screen
        Ray ray = camera.ScreenPointToRay(inputHandler.MousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            // The point where the ray is casted
            var target = hitInfo.point;
            // Prevents the player to look upwards or downwards
            target.y = transform.position.y;
            // Rotates the player to the target vector
            transform.LookAt(target);
            //Debug.DrawRay(transform.position, target, Color.red, 5f);
        }
    }
    private void MovePlayer()
    {
        if(inputHandler.InputVector.sqrMagnitude == 2)
        {
            rBody.linearVelocity = transform.TransformDirection(movementSpeed * 0.7f * Time.deltaTime * inputHandler.InputVector);
        }
        else
        {
            rBody.linearVelocity = transform.TransformDirection(movementSpeed * Time.deltaTime * inputHandler.InputVector);
        }
    }
    
    private void Run()
    {
        var runningSpeed = movementSpeed * speedMultiplier;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rBody.AddRelativeForce(runningSpeed * Time.deltaTime * inputHandler.InputVector, ForceMode.Impulse);
        }
    }
}
