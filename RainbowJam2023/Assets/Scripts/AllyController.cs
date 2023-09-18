using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AllyStatus
{
    None = 0,
    Bullied = 1,
    Following = 2,
    Waiting = 3,
}
public class AllyController : CharacterBase
{  
        private Transform leader;
        public float followDistance = .5f;
        public AllyStatus allyStatus;
        
        public int allyIndex;
        public new void Spawn()
        {
            BeginFollowingPlayer();
        }
        

        void LateUpdate ()
        {
            if (leader != null)
            {
                   //lerp towards leader
                Vector3 targetPos =
                    leader.position + (transform.position - leader.position).normalized * followDistance;
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
            }
            else if (allyStatus == AllyStatus.Waiting)
            {
                if (Vector3.Distance(PlayerManager.Instance.GetPlayer().transform.position, transform.position) < noticeDistance)
                {
                    GameManager.Instance.ShowAllyPickupModal(allyIndex);
                    BeginFollowingPlayer();
                }
            }

        }
        
        private void Update()
        {
            if (allyStatus == AllyStatus.Following)
            {
                Fire();
            }
        }
        
        protected override CharacterBase GetTarget()
        {
            return GetClosestEnemy();
        }
        
        public override void TakeDamage(ProjectileController projectile, float damage)
        {
            //allies don't take damage right now
        }

        public void BeginFollowingPlayer()
        {
            allyStatus = AllyStatus.Following;
            if (PlayerManager.Instance.GetPlayer().allies.Count == 0)
            {
                Debug.Log("Spawned first ally");
                leader = PlayerManager.Instance.GetPlayer().transform;
            }
            else
            {
                Debug.Log("Spawned ally");
                leader = PlayerManager.Instance.GetPlayer().allies[^1].transform;
            }
            PlayerManager.Instance.allies.Add(this);
            GameManager.Instance.ShowAllyPickupModal(allyIndex);
        }

        public void Unbully()
        {
            allyStatus = AllyStatus.Waiting;
        }
}
