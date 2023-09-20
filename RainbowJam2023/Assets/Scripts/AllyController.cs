using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
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
        public GameObject helpTextContainer;
        public TextMeshProUGUI helpText;
        
        public int allyIndex;
        public new void Spawn()
        {
            BeginFollowingPlayer();
        }

        private void Start()
        {
            if(allyStatus == AllyStatus.Bullied || allyStatus == AllyStatus.Waiting)
                PlayHelpAnimation();
        }


        void LateUpdate ()
        {
            if (leader != null && allyStatus == AllyStatus.Following)
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
                    if (GameManager.Instance.IsAllAlliesFollowing())
                    {
                        GameManager.Instance.WinGame();
                    }
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
            helpTextContainer.SetActive(false);
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
            // GameManager.Instance.ShowAllyPickupModal(allyIndex);
        }

        public void Unbully()
        {
            allyStatus = AllyStatus.Waiting;
        }

        public override void PlayJumpingAnimation()
        {
            allyStatus = AllyStatus.None;
            base.PlayJumpingAnimation();
        }

        private void PlayHelpAnimation()
        {
            helpTextContainer.SetActive(true);
            helpText.transform.DOLocalJump(Vector3.zero, 20f, 1, 1f)
                .SetLoops(-1).SetDelay(1f).SetEase(Ease.OutBounce);
        }

}
