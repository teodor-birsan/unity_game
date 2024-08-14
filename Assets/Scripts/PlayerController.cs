using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // InputHandler reference
    private InputHandler _input;
    // The movement speed of the player.
    private float moveSpeed = 7f;
    // Player camera
    public new Camera camera;
    // A list of projectile prefabs.
    public List<GameObject> projectilePrefab = new List<GameObject>();
    // The index of the equipped projectile.
    private int projectileIndex;
    // The equipped projectile
    public GameObject equipedProjectile;
    private Rigidbody playerRigidbody;
    private float runningSpeed = 15f;

    // Start is called before the first frame update
    private void Awake()
    {
        _input = GetComponent<InputHandler>();
    }

       void Start()
        {
            playerRigidbody = GetComponent<Rigidbody>();
        }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ShootProjectile();
        SelectProjectile();
    }

    private void MovePlayer()
    {
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // Running
            MoveTowardTarget(targetVector, runningSpeed);
        }
        else
        {
            // Walking
            MoveTowardTarget(targetVector,moveSpeed);
        }
        RotateTowardsMouse();

    }

    private void RotateTowardsMouse()
    {
        // Casts a ray from the mouse cursor to the screen
        Ray ray = camera.ScreenPointToRay(_input.MousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            // The point where the ray is casted
            var target = hitInfo.point;
            // Prevents the player to look upwards or downwards
            target.y = transform.position.y;
            // Rotates the player to the target vector
            transform.LookAt(target);
        }
    }

    private void MoveTowardTarget(Vector3 targetVector, float moveSpeed)
    {

        // Rotates the movment vector along the y axis
        targetVector = Quaternion.Euler(0, transform.eulerAngles.y, 0) * targetVector;

        // Pushes the player in the direction of the targetVector
        playerRigidbody.AddForce(targetVector * moveSpeed);
    }

    // When left click is pressed a projectile prefab is created
    private void ShootProjectile()
    {
        if(Input.GetMouseButtonDown(0) && projectilePrefab.Any())
        {
            // Creates the projectile prefab with the index projectileIndex at the player position 
            // and has the same roatation as the player.
            Instantiate(projectilePrefab[projectileIndex], transform.position, transform.rotation);
        }

        if(Input.GetMouseButtonDown(0) && !projectilePrefab.Any())
        {
            Debug.Log("You have no projectiles!\n");
        }
    }


    private void SelectProjectile()
    {
        // Scroll up
        if(Input.mouseScrollDelta.y > 0)
        {
            // If the current projectile is the last in the list, the next one will the the first one,
            // else the next projectile will be equipped.
            if(projectileIndex + 1 == projectilePrefab.Count)
            {
                projectileIndex = 0;
            }
            else
            {
                projectileIndex++;
            }
        }
        // Scroll down
        else if(Input.mouseScrollDelta.y < 0)
        {
            // If the current projectile is the first in the list, the previous one will be the last one,
            // else the previous projectile will be equipped.
            if(projectileIndex == 0)
            {
                projectileIndex = projectilePrefab.Count - 1;
            }
            else
            {
                projectileIndex--;
            }
        }
        if(projectilePrefab.Any())
            equipedProjectile = projectilePrefab[projectileIndex];
    }
    public void setProjectileIndex()
    {
        projectileIndex = 0;
    }

}
