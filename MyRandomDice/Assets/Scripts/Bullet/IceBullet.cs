using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : Bullet
{
 
    protected override void Start()
    {
        base.Start();
       

       
 
    }
    //대상이 타겟인지 검사안해서 스플데미지가능
    protected override void OnTriggerEnter2D(Collider2D other)
    {
      
        if (other.gameObject.tag == "Enemy" && target == other.gameObject)
        {

            other.gameObject.GetComponent<EnemyBase>().TakeDamage(damage);
            GetComponent<Animator>().speed = 1f;

            other.gameObject.GetComponent<EnemyBase>().SlowDebuffFunc();

           


        }
    }
}
