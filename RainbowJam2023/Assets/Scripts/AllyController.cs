using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyController : CharacterBase
{  
        private Transform leader;
        public float followDistance = .5f;

        public void Instantiate()
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
            //lerp towards leader
            Vector3 targetPos = leader.position + (transform.position - leader.position).normalized * followDistance;
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);

        }
        
        private void Update()
        {
            Fire();
        }
        
        protected override CharacterBase GetTarget()
        {
            return GetClosestEnemy();
        }
}
