using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterBase
{
    private void Update()
    {
        Fire();
    }
    
    protected override CharacterBase GetTarget()
    {
        //if player is close enough, target player
        if(GetDistance(transform.position, PlayerManager.Instance.GetPlayer().transform.position) < fireDistance)
            return PlayerManager.Instance.GetPlayer();
        return null;
    }
}
