using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet
{
    //protected override void Start()
    //{
    //    myAnim = GetComponent<Animator>();
    //    myAnim.speed = 0f;
    //}


    //protected override void Update()
    //{
    //    base.Update();
    //    // if (EnemyManager.Instance.GetFrontEnemy())
    //    // transform.position= EnemyManager.Instance.GetFrontEnemy().transform.position;
    //    if (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)//애니메이션종료
    //        ReturnObjectPool();
    //}

    //protected override void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.tag == "Enemy")
    //    {
    //        other.gameObject.GetComponent<EnemyBase>().TakeDamage(damage);
    //        GetComponent<Animator>().speed = 1f;
    //        //  Destroy(gameObject);
    //    }
    //}
}
