using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDice : DiceBase
{

    protected override void Start()
    {
        base.Start();
       // myBulletResouce = Resources.Load<GameObject>("Bullet/IceBullet");
        attackTurm = 1f;

    }
    protected override void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackTurm - (DicePower - 1) * AttackSpeedUp_Coefficient)
        {
          // var list_ = EnemyManager.Instance.GetEnemyList();
            GameObject TargetEnemy = null;
            bool bFindNoIceEnemy = false;

            //아이스 3단계미만인애있나부터검사



            var frontEnemy = EnemyManager.Instance.GetFrontEnemy();
            if(isAI) frontEnemy = EnemyManager.Instance.GetFrontEnemyAI();
            //앞부터 체크

            if (frontEnemy!=null &&frontEnemy.GetComponent<EnemyBase>().SlowDebuffPhase < 3)
            {
                bFindNoIceEnemy = true;
                TargetEnemy = frontEnemy;
            }

            else //나머지 체크
            {
                var list = EnemyManager.Instance.GetEnemyList();
                if (isAI)
                    list = EnemyManager.Instance.GetEnemyListAI();
                foreach (var v in list)
                {
                    if (v != null && v.gameObject.GetComponent<EnemyBase>().SlowDebuffPhase < 3)
                    {

                        Debug.Log("아이스적발견");
                        bFindNoIceEnemy = true;
                        TargetEnemy = v;
                        break;
                    }
                }
            }

            if (bFindNoIceEnemy == false)
            {
                if(!isAI)
                    TargetEnemy = EnemyManager.Instance.GetFrontEnemy();
                else
                    TargetEnemy = EnemyManager.Instance.GetFrontEnemyAI();


            }



            attackTimer = 0f;
            var ob = mySuperVisor.GetComponent<MasterDiceScript>().GetObject();
            ob.GetComponent<Bullet>().myOwn = mySuperVisor;
            ob.transform.position = transform.position;
            ob.GetComponent<Bullet>().SetMyDamage(GetDiceDamage());
            ob.GetComponent<Bullet>().OnTarget(TargetEnemy);

           


        }
    }
}
