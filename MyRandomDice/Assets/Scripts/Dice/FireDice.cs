using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDice : DiceBase
{

    protected override void Start()
    {
        base.Start();
      //  myBulletResouce = Resources.Load<GameObject>("Bullet/FirerBulletPrefab");
    }

    protected override void Attack() 
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackTurm - (DicePower - 1) * AttackSpeedUp_Coefficient)
        {
            attackTimer = 0f;
            var ob = mySuperVisor.GetComponent<MasterDiceScript>().GetObject();
            ob.GetComponent<Bullet>().myOwn = mySuperVisor;
            ob.transform.position = transform.position;
            ob.GetComponent<Bullet>().SetMyDamage(GetDiceDamage());
            if (!isAI)
                ob.GetComponent<Bullet>().OnTarget(EnemyManager.Instance.GetFrontEnemy());
            else
                ob.GetComponent<Bullet>().OnTarget(EnemyManager.Instance.GetFrontEnemyAI());
        }
    }
}
