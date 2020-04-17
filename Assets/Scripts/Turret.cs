using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{   

    private  Transform target;
    [Header("Atributes")]
    public float range = 15f;
    public float fireRate = 1.0f;
    private float fireCountDown = 0f;

    [Header("Unitiy Setup Fields")]
    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public GameObject buletPrefab;
    public Transform firePoint;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f); // ce vrei sa repeti de la ce punct si de cate ori pe secunda
    }       
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range); // pozitia turetei si raza

    }
    void UpdateTarget () //cautam tinte noi cea mai apropriata si verificam daca este target
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearstEnemy = null;

        foreach (GameObject enemy in enemies )
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)  // gasim inamicul cel mai aproriat
            {
                shortestDistance = distanceToEnemy;
                nearstEnemy = enemy;
            }
        }

        if (nearstEnemy != null && shortestDistance <= range)
        {
            target = nearstEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null) // do nothing
            return;
        Vector3 direction = target.position - transform.position; // pozitia
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation,lookRotation,Time.deltaTime* turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); // around Y axis


        if(fireCountDown<= 0)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;

    }

    void Shoot()
    {
        GameObject bulletGO =(GameObject)Instantiate(buletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Skeek(target);

    }
}
