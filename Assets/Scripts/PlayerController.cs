using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputHandler _input;

    private float moveSpeed = 10f;
    public new Camera camera;
    public GameObject projectilePrefab;

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
        ShootProjectile();
    }

    private void MovePlayer()
    {
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
        var movmentVector = MoveTowardTarget(targetVector);
        RotateTowardsMouse();

    }

    private void RotateTowardsMouse()
    {
        Ray ray = camera.ScreenPointToRay(_input.MousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime;
        targetVector = Quaternion.Euler(0, transform.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }

    private void ShootProjectile()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
        }
    }
}
