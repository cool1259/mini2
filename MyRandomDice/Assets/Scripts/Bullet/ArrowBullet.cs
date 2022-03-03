using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBullet : Bullet
{
    protected override void Start()
    {
        bullet_spped = 3f;
    }

    protected override void Update()
    {
        base.Update();
        if (target)
            transform.LookAt(target.transform);
    }
 
}
