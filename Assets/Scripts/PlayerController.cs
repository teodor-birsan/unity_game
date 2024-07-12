using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputHandler _input;

    private float moveSpeed = 10f;
    public new Camera camera;
    //public GameObject projectilePrefab;
    public List<GameObject> projectilePrefab = new List<GameObject>();
    private int projectileIndex = 0;

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
        SelectProjectile();
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
        if(Input.GetMouseButtonDown(0) && projectilePrefab.Any())
        {
            Instantiate(projectilePrefab[projectileIndex], transform.position, transform.rotation);
        }
    }


    private void SelectProjectile()
    {
        if(Input.mouseScrollDelta.y > 0)
        {
            if(projectileIndex + 1 == projectilePrefab.Count)
            {
                projectileIndex = 0;
            }
            else
            {
                projectileIndex++;
            }
        }
        else if(Input.mouseScrollDelta.y < 0)
        {
            if(projectileIndex == 0)
            {
                projectileIndex = projectilePrefab.Count - 1;
            }
            else
            {
                projectileIndex--;
            }
        }
    }
}
