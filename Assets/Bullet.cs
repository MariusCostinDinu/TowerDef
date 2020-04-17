using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;
    public GameObject inpactEffect;
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
                return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame) //lungimea
        {//when we hit
            HitTarget();
        }

        transform.Translate(dir.normalized * distanceThisFrame,Space.World);
    }

    private void HitTarget()
    {
        GameObject effectInstanance = (GameObject)Instantiate(inpactEffect, transform.position, transform.rotation);
        Destroy(effectInstanance, 2f);
        Destroy(target.gameObject); // o sa facem HP
        Destroy(gameObject);
    }

    public void Skeek(Transform _target)
    {
        
        target = _target;
    }

   
}
