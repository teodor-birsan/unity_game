using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public float health;
    private PlayerController playerRef;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        playerRef = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            health -= playerRef.equipedProjectile.GetComponent<ProjectileHandler>().projectileDamage;  
        }
    }
}
