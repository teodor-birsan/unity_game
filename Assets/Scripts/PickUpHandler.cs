using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHandler : MonoBehaviour
{
    public GameObject projectile;
    private ShootProjectile shootScript;
    // Start is called before the first frame update
    void Start()
    {
        shootScript = GameObject.Find("Player").GetComponent<ShootProjectile>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (shootScript.projectilePrefabV2.Find(projectile) == null)
        {
            shootScript.projectilePrefabV2.AddLast(new LinkedListNode<GameObject>(projectile));
            if(shootScript.equipedProjectile == null){
                shootScript.equipedProjectile = projectile;
            }
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("This object has been already picked up!\n");
        }
        
    }
}
