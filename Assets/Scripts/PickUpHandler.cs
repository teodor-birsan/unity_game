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
        if (!playerController.projectilePrefab.Find(place => place.gameObject == projectile))
        {
            playerController.projectilePrefab.Add(projectile);
            playerController.setProjectileIndex();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("This object has been already picked up!\n");
        }
        
    }
}
