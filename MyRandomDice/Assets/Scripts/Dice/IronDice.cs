using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronDice : DiceBase
{
    protected override void Start()
    {
        base.Start();
        attackTurm = 1f;
    }

    protected override void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackTurm - (DicePower - 1) * AttackSpeedUp_Coefficient)
        {
          
            GameObject TargetEnemy = null;
            float maxHp = 0f;
            
            var list = EnemyManager.Instance.GetEnemyList();
            if (isAI)
                list = EnemyManager.Instance.GetEnemyListAI();

            foreach (var v in list)
            {
                if (v.name.Contains("boss")) //보스우선타격
                {
                    TargetEnemy = v;
                    break;
                }
                //hp젤높은애타격
                if (maxHp < v.GetComponent<EnemyBase>().HP)
                {
                    maxHp = v.GetComponent<EnemyBase>().HP;
                    TargetEnemy = v;
                }

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
