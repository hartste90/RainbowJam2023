using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class PlayerManager : CharacterBase
{
    
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
        GetComponent<PlayerMovement>().enabled = false;
    }
}
