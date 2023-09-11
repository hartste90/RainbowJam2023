using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 10f;
    public float hitDistance = .1f;
    public float aliveTime = 10f;
    private Vector3 direction;
    private CharacterBase target;
    private float spawnTime;
    
    public void Instantiate(CharacterBase targetSet)
    {
        target = targetSet;
        direction = targetSet.transform.position - transform.position;
        spawnTime = Time.time;
        
    }

    private void Update()
    {
        if(Time.time-spawnTime > aliveTime)
            Destroy(gameObject);
        //move towards target
        if (direction != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, direction * 100f, speed * Time.deltaTime);
            if (target != null && Vector3.Distance(transform.position, target.transform.position) < hitDistance)
            {
                //TODO: apply damage to target
                target.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
