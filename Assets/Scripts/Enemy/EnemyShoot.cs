using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject enemyBullet;
    public Transform spawnBulletPoint;

    public Transform playerPosition;
    public float bulletVelocity = 100;

    public float maxDistance = 100;

    // Start is called before the first frame update
    void Start()
    {
      
        Invoke("ShootPlayer", 3);
    }

    // Update is called once per frame
    void ShootPlayer()
    {
        
        if (playerPosition != null && CanSeeTarget())
        {
            Debug.Log("Dispara");
            transform.LookAt(playerPosition);
            Vector3 playerDirection = playerPosition.position - transform.position;
            GameObject newBullet = Instantiate(enemyBullet, spawnBulletPoint.position,
                spawnBulletPoint.rotation);
            
            newBullet.GetComponent<Rigidbody>().AddForce(playerDirection * bulletVelocity,
                ForceMode.Force);

            Invoke("ShootPlayer", 3);
        }
    }
    
    
    bool CanSeeTarget()
    {
        if (playerPosition == null)
        {
            return false;
        }

        // Calcula la dirección hacia el objetivo
        Vector3 direction = playerPosition.position - transform.position;

        // Lanza un rayo en la dirección del objetivo
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, maxDistance))
        {
            // Verifica si el rayo golpea al objeto objetivo
            if (hit.collider.transform == playerPosition)
            {
                // El objeto está a la vista
                return true;
            }
        }

        // El objeto no está a la vista
        return false;
    }
}