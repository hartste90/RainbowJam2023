using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterBase
{
    public int enemyIndex = -1;
    public List<EnemyController> enemyGroup;
    public AllyController bulliedAlly;
    
    protected override void Instantiate()
    { 
        base.Instantiate();
        //set enemy group for all enemies in group
        foreach (EnemyController enemy in enemyGroup)
        {
            enemy.enemyGroup = enemyGroup;
        }
    }    
    private void Update()
    {
        if (!isActive)
        {
            if (enemyIndex > -1 && Vector3.Distance(PlayerManager.Instance.GetPlayer().transform.position, transform.position) < noticeDistance)
            {
                GameManager.Instance.ShowEnemyModal(enemyIndex);
                isActive = true;
                foreach (EnemyController enemy in enemyGroup)
                {
                    enemy.isActive = true;
                }
            }
        }
        else
        {
            Fire();
        }

    }
    
    protected override CharacterBase GetTarget()
    {
        //if player is close enough, target player
        if(GetDistance(transform.position, PlayerManager.Instance.GetPlayer().transform.position) < fireDistance)
            return PlayerManager.Instance.GetPlayer();
        return null;
    }
    
    protected override void Die()
    {
        EnemyManager.Instance.RemoveEnemy(this);
        enemyGroup.Remove(this);
        if (enemyGroup.Count == 0)
        {
            //all enemies in group are dead
            if (bulliedAlly != null)
            {
                bulliedAlly.Unbully();
            }
        }
        Destroy(gameObject);
    }
}
