using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDice : DiceBase
{
    protected override void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackTurm - (DicePower - 1) * AttackSpeedUp_Coefficient)
        {
            attackTimer = 0f;
            var ob = mySuperVisor.GetComponent<MasterDiceScript>().GetObject();
            ob.GetComponent<Bullet>().myOwn = mySuperVisor;
            ob.transform.position = transform.position;
            //sp에 비례데미지
            ob.GetComponent<Bullet>().SetMyDamage((int)(go_playerController.GetComponent<PlayerController>().GetMySp()*0.1f)+ GetDiceDamage());
            if (!isAI)
                ob.GetComponent<Bullet>().OnTarget(EnemyManager.Instance.GetFrontEnemy());
            else
                ob.GetComponent<Bullet>().OnTarget(EnemyManager.Instance.GetFrontEnemyAI());
        }
    }
}
