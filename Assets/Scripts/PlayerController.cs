using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputHandler _input;

    [SerializeField] private float moveSpeed;
    public new Camera camera;
    [SerializeField] private float rotateSpeed;
    /*[SerializeField]
    private bool roateTowardMouse;*/

    // Start is called before the first frame update
    private void Awake()
    {
        _input = GetComponent<InputHandler>();
    }

    /*    void Start()
        {

        }*/

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
        var movmentVector = MoveTowardTarget(targetVector);
        RotateTowardsMouse();

        /*      if (!roateTowardMouse)
                  RotatateTowardMovmentVector(movmentVector);
              else
              {
                  RotateTowardsMouse();
              }*/
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

    private void RotatateTowardMovmentVector(Vector3 movmentVector)
    {
        if (movmentVector.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(movmentVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime;
        targetVector = Quaternion.Euler(0, transform.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }
}
