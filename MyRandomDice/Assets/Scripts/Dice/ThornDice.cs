using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornDice : DiceBase
{
    protected override void Start()
    {
        base.Start();
       // myBulletResouce = Resources.Load<GameObject>("Bullet/ThornBulletPrefab");
        attackTimer = 5f;
        attackTurm = 10f;
        Dice_Damage = 1f;
        DamageUp_Coefficient = 0.5f;
    }
    protected override void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackTurm - (DicePower - 1) * AttackSpeedUp_Coefficient)
        {
            if (EnemyManager.Instance.GetFrontEnemy() == null) return;
           
            attackTimer = 0f;
            var ob = mySuperVisor.GetComponent<MasterDiceScript>().GetObject();
            ob.GetComponent<Bullet>().myOwn = mySuperVisor;
             ob.GetComponent<Bullet>().SetMyDamage((int)GetDiceDamage());
            if (!isAI)
                ob.transform.position = EnemyManager.Instance.GetFrontEnemy().GetComponent<Transform>().position;
            else
                ob.transform.position = EnemyManager.Instance.GetFrontEnemyAI().GetComponent<Transform>().position;

        }
    }

}
