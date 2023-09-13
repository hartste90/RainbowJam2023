using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AllyStatus
{
    None = 0,
    Bullied = 1,
    Following = 2,
}
public class AllyController : CharacterBase
{  
        private Transform leader;
        public float followDistance = .5f;
        public AllyStatus allyStatus;
        
        public int allyIndex;
        public new void Instantiate()
        {
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
            else
            {
                if (Vector3.Distance(PlayerManager.Instance.GetPlayer().transform.position, transform.position) < noticeDistance)
                {
                    GameManager.Instance.ShowAllyPickupModal(allyIndex);
                    leader = PlayerManager.Instance.GetPlayer().transform;
                    allyStatus = AllyStatus.Following;
                }
            }

        }
        
        private void Update()
        {
            Fire();
        }
        
        protected override CharacterBase GetTarget()
        {
            return GetClosestEnemy();
        }
        
        public override void TakeDamage(ProjectileController projectile, float damage)
        {
            //allies don't take damage right now
        }
}
