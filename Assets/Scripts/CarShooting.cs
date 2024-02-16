using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarShooting : MonoBehaviour
{
    public GameObject projectilePrefab;    // Prefab for the projectile
    public GameObject projectilePrefab2;    // Prefab for the projectile
    public Transform shootPoint;           // Point from where the projectile will be spawned
    public Transform shootPoint2;
    public float projectileSpeed = 30f;    // Speed of the projectile
    public float bulletDeathDelay = 5f;
    public float BuffTimer = 3f;
    public bool IsBulletBuffedSmallBullets;
    public bool IsBulletBuffeBigBullet;
    public float bigBulletCount=0;

    public int listSize;
    List<GameObject> listedPrefab;

    public PlayerInputHandler playerInputHandler;
    private void Awake()
    {
        playerInputHandler = GetComponentInParent<PlayerInputHandler>();
        
    }

        private void Start()
    {
        IsBulletBuffedSmallBullets = false;
        IsBulletBuffeBigBullet = false;

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
        if(playerInputHandler.isShooting )
        {
            if(IsBulletBuffedSmallBullets == true)
            {
                Shoot();

            }
             else if(IsBulletBuffeBigBullet == true)
            {
                Shoot2();
                if(bigBulletCount>=1)
                {
                    IsBulletBuffeBigBullet = false;

                }
                
            }
            
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




    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bullets Charge")
        {

            Debug.Log($"omg");
            Destroy(col.gameObject);
            StartCoroutine(TemporaryCollisionFlag());
        }
    if (col.gameObject.tag == "BuffBulletchaser")
    {

        
        Destroy(col.gameObject);
        IsBulletBuffeBigBullet = true;

    }

}
    IEnumerator TemporaryCollisionFlag()
    {
    IsBulletBuffedSmallBullets = true;
         yield return new WaitForSeconds(BuffTimer);
    IsBulletBuffedSmallBullets = false;
    }



void Shoot2()
{
    GameObject closestPlayer = FindClosestPlayer();
    if (closestPlayer != null)
    {
        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab2, shootPoint2.position, Quaternion.identity);

        // Set projectile's target to follow the closest player
        projectile.GetComponent<Projectile>().SetTarget(closestPlayer.transform);
        bigBulletCount++;
    }
}


GameObject FindClosestPlayer()
    {
         GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
         GameObject closestPlayer = null;
         float closestDistance = Mathf.Infinity;
          foreach (GameObject player in players)
          {
               if (player != gameObject) // Exclude the shooting player from consideration
               {
                   float distance = Vector3.Distance(transform.position, player.transform.position);
                   if (distance < closestDistance)
                   {
                       closestPlayer = player;
                       closestDistance = distance;
                   }
               }
          }
          return closestPlayer;
    }
}
