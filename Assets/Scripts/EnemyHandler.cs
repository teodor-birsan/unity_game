using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    // The health of the npc.
    public float health;
    // Reference to the PlayerController script
    private ShootProjectile playerRef;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        playerRef = GameObject.Find("Player").GetComponent<ShootProjectile>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the health reaches 0 or goes below 0, the object is destoryed.
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the object collides with another object with the tag "Projectile", its health will be reduced.
        if (other.gameObject.CompareTag("Projectile"))
        {
            health -= playerRef.equipedProjectile.GetComponent<ProjectileHandler>().projectileDamage;  
        }
    }
}
