using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;
using DG.Tweening;
using Random = UnityEngine.Random;

public class CharacterBase : MonoBehaviour
{
    public float maxDistance = 100f;
    public float speed = 6f;
    
    public float fireSpeed = 0.5f;
    public float timeLastFired = 0f;
    public float fireDistance = 20f;
    public float knockbackResistance = 0f;
    public GameObject projectilePrefab;
    public bool isActive;
    
    public ProceduralImage healthBar;
    public ProceduralImage healthBarOverlay;
    public int totalHealth;
    private int currentHealth;
    public float noticeDistance = 2f;

    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            if(healthBar!=null)
            {
                healthBar.fillAmount = (float)currentHealth / (float)totalHealth;
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Awake()
    {
        Instantiate();
    }

    protected virtual void Instantiate()
    {
        currentHealth = totalHealth;
    }

    public EnemyController GetClosestEnemy()
    {
        //get closest target if any
        List<EnemyController> enemies = EnemyManager.Instance.GetEnemies();
        EnemyController closestEnemy = null;
        float closestDistance = float.MaxValue;
        foreach (EnemyController enemy in enemies)
        {
            if (enemy.isActive)
            {
                float dist = GetDistance(transform.position, enemy.transform.position);
                if (dist < fireDistance)
                {
                    if (closestEnemy == null)
                    {
                        closestEnemy = enemy;
                    }
                    else
                    {
                        if (dist < closestDistance)
                        {
                            closestEnemy = enemy;
                            closestDistance = dist;
                        }
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

    protected virtual ProjectileController Fire()
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
                return projectile;
            }
        }
        return null;
    }
    
    public static float GetDistance(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a, b);
    }

    protected virtual void Die()
    {
        Debug.Log("virtual method should be overridden for this character");
    }

    public virtual void TakeDamage(ProjectileController projectile, float damage)
    {
        CurrentHealth -= (int)damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
        //knockback
        Vector3 direction = transform.position - projectile.transform.position;
        direction.y = 0;
        direction.Normalize();
        transform.position += direction * projectile.knockback * (1-knockbackResistance);
        //healthbar effect
        healthBarOverlay.color =
            new Color(healthBarOverlay.color.r, healthBarOverlay.color.g, healthBarOverlay.color.b, 1);
        healthBarOverlay.DOFade(0f, .1f);


    }

    public virtual void PlayJumpingAnimation()
    {
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOLocalJump(transform.position, 1f, 1, .5f));
        s.AppendInterval(Random.Range(.3f, 1f));
        s.SetLoops(-1);
        s.Play();
    }
    
    // void OnTriggerEnter2D(Collider2D col)
    // {
    //     Debug.Log("Trigger entered2: " + col.gameObject.name);
    //     if (this is EnemyController && col.gameObject.tag == "Enemy")
    //     {
    //         TakeDamage(col.GetComponent<ProjectileController>(), col.GetComponent<ProjectileController>().damage);
    //         Destroy(gameObject);
    //     }
    //     else if (this is PlayerManager && col.gameObject.tag == "Player")
    //     {
    //         TakeDamage(col.GetComponent<ProjectileController>(), col.GetComponent<ProjectileController>().damage);
    //         Destroy(gameObject);
    //     }
    // }
}
