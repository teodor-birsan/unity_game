using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    public float projectileSpeed;
    public float projectileDamage;
    public float projectileTime;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
        DestroyProjectile();
    }

    private void MoveProjectile()
    {
        // Moves the projectile through the world at a constant speed every second.
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Props") || other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject, projectileTime);
    }

}
