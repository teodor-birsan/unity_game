using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // InputHandler reference
    private InputHandler _input;
    // The movement speed of the player.
    private readonly float moveSpeed = 10f;
    // Player camera
    public new Camera camera;
    // A list of projectile prefabs.
    public LinkedList<GameObject> projectilePrefabV2 = new LinkedList<GameObject>();
    // The equipped projectile
    public GameObject equipedProjectile;

    // Start is called before the first frame update
    private void Awake()
    {
        _input = GetComponent<InputHandler>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ShootProjetile();
        CycleProjectileList();
    }

    private void MovePlayer()
    {
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
        MoveTowardTarget(targetVector);
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

    private void MoveTowardTarget(Vector3 targetVector)
    {
        // The speed is constant every second, not every frame
        var speed = moveSpeed * Time.deltaTime;

        // Rotates the movment vector along the y axis
        targetVector = Quaternion.Euler(0, transform.eulerAngles.y, 0) * targetVector;

        // Calculates where the player should be afer one of the WASD keys is pressed
        var targetPosition = transform.position + targetVector * speed;
        //Updates the position of the player
        transform.position = targetPosition;
    }

    private void ShootProjetile(){
        if(equipedProjectile != null && Input.GetMouseButtonDown(0)){
            Instantiate(equipedProjectile, transform.position, transform.rotation);
        }
    }

    private void CycleProjectileList(){
        if(projectilePrefabV2.Count > 0){
            if(Input.mouseScrollDelta.y > 0){
                if(equipedProjectile.Equals(projectilePrefabV2.Last.Value)){
                    equipedProjectile = projectilePrefabV2.First.Value;
                }
                else{
                    equipedProjectile = projectilePrefabV2.Find(equipedProjectile).Next.Value;
                }
            }
            if(Input.mouseScrollDelta.y < 0){
                if(equipedProjectile.Equals(projectilePrefabV2.First.Value)){
                    equipedProjectile = projectilePrefabV2.Last.Value;
                }
                else{
                    equipedProjectile = projectilePrefabV2.Find(equipedProjectile).Previous.Value;
                }
            }
        }
    }

}
