using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileController : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 10f;
    public float hitDistance = .1f;
    public float aliveTime = 10f;
    private Vector3 direction;
    private CharacterBase target;
    private float spawnTime;
    public float knockback = .5f;
    public float rotateSpeed = 1f;
    public SpriteRenderer spriteRenderer;
    public void Instantiate(CharacterBase targetSet)
    {
        target = targetSet;
        Vector2 v = targetSet.GetComponent<Collider2D>().ClosestPoint(transform.position);
        direction = new Vector3(v.x, v.y, 0) - transform.position;
        spawnTime = Time.time;
        transform.Rotate(Vector3.forward * Random.Range(0, 360));
        transform.DOLocalRotate(Vector3.forward * -360, rotateSpeed, RotateMode.FastBeyond360).SetLoops(-1);

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (target is EnemyController && col.gameObject.tag == "Enemy")
        {
            col.GetComponent<EnemyController>().TakeDamage(this, damage);
            Destroy(gameObject);
        }
        else if (target is PlayerManager && col.gameObject.tag == "Player")
        {
            col.GetComponent<PlayerManager>().TakeDamage(this, damage);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(Time.time-spawnTime > aliveTime)
            Destroy(gameObject);
        //move towards target
        if (direction != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, direction * 100f, speed * Time.deltaTime);
            // if (target != null && Vector3.Distance(transform.position, target.transform.position) < hitDistance)
            // {
            //     //TODO: apply damage to target
            //     
            // }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }
}
