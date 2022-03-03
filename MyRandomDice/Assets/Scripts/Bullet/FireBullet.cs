using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : Bullet
{
    Vector3 originScale;
    Vector2 originBoxSize;
    //protected override void Start()
    //{
    //    myAnim = GetComponent<Animator>();
    //    myAnim.speed = 0f;
    //    originScale = transform.localScale;

    //}
    private void Awake()
    {
        originScale = transform.localScale;
        originBoxSize = GetComponent<BoxCollider2D>().size;
    }
    private void OnEnable()
    {
        GetComponent<Animator>().speed = 0f;

        transform.localScale = originScale;
        GetComponent<BoxCollider2D>().size = originBoxSize;

        //Invoke("ReturnObjectPool", 2f);
    }
    //protected override void Update()
    //{
    //    base.Update();
    //    // if (EnemyManager.Instance.GetFrontEnemy())
    //    // transform.position= EnemyManager.Instance.GetFrontEnemy().transform.position;
    //    if (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)//애니메이션종료
    //        ReturnObjectPool();
    //}
    

    //대상이 타겟인지 검사안해서 스플데미지가능
    protected override void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            GetComponent<BoxCollider2D>().size = new Vector2(1.4f, 1.4f);
            other.gameObject.GetComponent<EnemyBase>().TakeDamage(damage);
            GetComponent<Animator>().speed = 1f;
            transform.localScale = originScale * 4f;
        }
    }
}
