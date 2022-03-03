using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindDice :DiceBase
{
    protected override void Start()
    {
        base.Start();
       // myBulletResouce = Resources.Load<GameObject>("Bullet/FirerBulletPrefab");
        attackTurm = 0.25f;
    }
}
