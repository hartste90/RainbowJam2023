using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public float maxDistance = 100f;
    public float speed = 6f;
    
    public float fireSpeed = 0.5f;
    public float timeLastFired = 0f;
    public float fireDistance = 20f;
    public GameObject projectilePrefab;
    public bool isActive;

    public EnemyController GetClosestEnemy()
    {
        //get closest target if any
        List<EnemyController> enemies = EnemyManager.Instance.GetEnemies();
        EnemyController closestEnemy = null;
        float closestDistance = float.MaxValue;
        foreach (EnemyController enemy in enemies)
        {
            float dist = GetDistance(transform.position, enemy.transform.position);
            if(dist < fireDistance)
            {
                if(closestEnemy == null)
                {
                    closestEnemy = enemy;
                }
                else
                {
                    if(dist < closestDistance)
                    {
                        closestEnemy = enemy;
                        closestDistance = dist;
                    }
                }
            }
        }
        return closestEnemy;
    }

    protected virtual CharacterBase GetTarget()
    {
        throw new System.NotImplementedException();
    }

    protected void Fire()
    {
        //check if we should fire
        if (Time.time - timeLastFired > fireSpeed)
        {
            timeLastFired = Time.time;
            CharacterBase closestEnemy = GetTarget();
            if (closestEnemy != null)
            {
                ProjectileController projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity)
                    .GetComponent<ProjectileController>();
                projectile.Instantiate(closestEnemy);
            }
        }
    }
    
    public static float GetDistance(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a, b);
    }
    
}
