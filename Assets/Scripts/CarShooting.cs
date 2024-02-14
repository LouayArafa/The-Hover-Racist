using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarShooting : MonoBehaviour
{
    public GameObject projectilePrefab;    // Prefab for the projectile
    public Transform shootPoint;           // Point from where the projectile will be spawned
    public float projectileSpeed = 10f;    // Speed of the projectile
    public float bulletDeathDelay;

    public int listSize;
    List<GameObject> listedPrefab;

    PlayerInputHandler playerInputHandler;
    private void Awake()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();
    }

        private void Start()
    {
        
        listedPrefab = new List<GameObject>();
        for (int i = 0; i < listSize; i++)
        {
            GameObject obj = Instantiate(projectilePrefab);
            obj.SetActive(false);
            listedPrefab.Add(obj);
        }
    }
    public void Update()
    {
        if(playerInputHandler.isShooting)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Rigidbody carRb = GetComponent<Rigidbody>();
        Vector3 carMovementDirection = carRb.velocity.normalized;
        foreach (GameObject prefabed in listedPrefab)
        {
            if (!prefabed.activeInHierarchy)
            {
                prefabed.SetActive(true);
                prefabed.transform.position = shootPoint.position;
                Rigidbody projectileRb = prefabed.GetComponent<Rigidbody>();
                if (projectileRb != null)
                {
                    projectileRb.velocity = shootPoint.forward * projectileSpeed + carMovementDirection*carRb.velocity.magnitude; 
                }

                StartCoroutine(ReturningPrefab(prefabed, bulletDeathDelay));
                return;
            }
        }
        
    }
    IEnumerator ReturningPrefab(GameObject projectile, float delay)
    {

        yield return new WaitForSeconds(delay);
        projectile.SetActive(false);

    }
    public void Returning(GameObject projectile)
    {
        projectile.SetActive(false);
    }

    
}
