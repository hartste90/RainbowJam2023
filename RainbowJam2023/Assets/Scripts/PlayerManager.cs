using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class PlayerManager : CharacterBase
{
    private int lastColorIdx = 0;
    
    public static PlayerManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        Instantiate();
    }
    
    public List<AllyController> allies = new List<AllyController>();
    public AllyController allyPrefab;
    

    public PlayerManager GetPlayer()
    {
        return this;
    }
    void Update()
    {
        Fire();
    }

    protected override ProjectileController Fire()
    {
        PlayerProjectileController projectile = base.Fire() as PlayerProjectileController;
        if (projectile != null)
        {
            projectile.SetColor(lastColorIdx);
            lastColorIdx++;
        }
        return projectile;
    }

    protected override CharacterBase GetTarget()
    {
        return GetClosestEnemy();
    }

    public void SpawnAlly()
    {
        AllyController ally = Instantiate(allyPrefab, transform.position + Vector3.left * 2f, Quaternion.identity).GetComponent<AllyController>();
        ally.Spawn();
        allies.Add(ally);
    }

    protected override void Die()
    {
        GameManager.Instance.OnPlayerDied();
        
    }

    public void RestPlayer()
    {
        CurrentHealth = totalHealth;
    }


    public void DisableGameplayInput()
    {
        GetComponent<PlayerMovement>().rb.velocity = Vector3.zero;
        GetComponent<PlayerMovement>().enabled = false;
    }
}
