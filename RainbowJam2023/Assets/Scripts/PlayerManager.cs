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
    }
    
    public List<AllyController> allies = new List<AllyController>();
    public AllyController allyPrefab;
    

    public PlayerManager GetPlayer()
    {
        return this;
    }
    void Update()
    {
        MovePlayer();
        Fire();

    }

    protected override CharacterBase GetTarget()
    {
        return GetClosestEnemy();
    }

    /// <summary>
    /// moves the player towards the mouse position
    /// </summary>
    void MovePlayer()
    {
//         if (Input.GetMouseButton(0))
//         {
//             Debug.Log("Mouse button is down");
//
//             //get mouse position
//             Vector3 fingerPos = Vector3.zero;
// #if UNITY_EDITOR
//             fingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Desktop
// #else
//         fingerPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position); //Mobile
// #endif
//             fingerPos = new Vector3(fingerPos.x, fingerPos.y, 0);
//             Vector3 pos = Vector3.MoveTowards(transform.position, fingerPos, speed * Time.deltaTime);
//             transform.position = pos;
//         }
    }

    public void SpawnAlly()
    {
        AllyController ally = Instantiate(allyPrefab, transform.position + Vector3.left * 2f, Quaternion.identity).GetComponent<AllyController>();
        ally.Instantiate();
        allies.Add(ally);
    }

    protected override void Die()
    {
        Debug.Log("Player died");
        
    }

    public void RestPlayer()
    {
        CurrentHealth = totalHealth;
    }
    
}
