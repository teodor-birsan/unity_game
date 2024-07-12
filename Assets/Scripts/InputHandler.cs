using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    /* A vector which stores data on the input from the player.
     * 
     */
    public Vector2 InputVector { get; private set; }
    public Vector3 MousePosition { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        InputVector = new Vector3(horizontalInput, verticalInput);
        MousePosition = Input.mousePosition;
    }
}
