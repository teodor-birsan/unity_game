using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    // A list of projectile prefabs.
    public LinkedList<GameObject> projectilePrefabV2 = new LinkedList<GameObject>();
    // The equipped projectile
    public GameObject equipedProjectile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        CycleProjectileList();
        
    }

    private void Shoot()
    {
        if (equipedProjectile != null && Input.GetMouseButtonDown(0))
        {
            Instantiate(equipedProjectile, transform.position, transform.rotation);
        }
    }

    private void CycleProjectileList()
    {
        if (projectilePrefabV2.Count > 0)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                if (equipedProjectile.Equals(projectilePrefabV2.Last.Value))
                {
                    equipedProjectile = projectilePrefabV2.First.Value;
                }
                else
                {
                    equipedProjectile = projectilePrefabV2.Find(equipedProjectile).Next.Value;
                }
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                if (equipedProjectile.Equals(projectilePrefabV2.First.Value))
                {
                    equipedProjectile = projectilePrefabV2.Last.Value;
                }
                else
                {
                    equipedProjectile = projectilePrefabV2.Find(equipedProjectile).Previous.Value;
                }
            }
        }
    }
}
