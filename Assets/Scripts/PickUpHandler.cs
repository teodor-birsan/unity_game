using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHandler : MonoBehaviour
{
    public GameObject projectile;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerController.projectilePrefabV2.Find(projectile) == null)
        {
            playerController.projectilePrefabV2.AddLast(new LinkedListNode<GameObject>(projectile));
            if(playerController.equipedProjectile == null){
                playerController.equipedProjectile = projectile;
            }
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("This object has been already picked up!\n");
        }
        
    }
}
