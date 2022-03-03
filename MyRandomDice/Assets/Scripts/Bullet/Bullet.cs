using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

   protected float damage = 20f;
    protected GameObject target;
    protected float bullet_spped = 7f;
    bool targetOn = false;
   protected Animator myAnim;
    public GameObject myOwn;

   
    // Start is called before the first frame update
    protected virtual void Start()
    {
        myAnim = GetComponent<Animator>();
        myAnim.speed = 0f;
        Invoke("ReturnObjectPool", 2f);

    }
    // Update is called once per frame
     protected virtual void  Update()
    {

        if (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)//애니메이션종료
            ReturnObjectPool();


        if (target!=null)
        {
            transform.position = Vector2.Lerp(transform.position, target.transform.position, bullet_spped*Time.deltaTime);
        }
        else if(targetOn==true)//타겟on이 됐는데 타겟이없다는건 적이이미죽은거 타겟을잃음
        {
            ReturnObjectPool();
        }

    }

   public void OnTarget(GameObject target_)
    {
        if (target_ == null) return;
        target=target_;
        targetOn = true;
    }
    public void SetMyDamage(float mydamage_)
    {
        damage = mydamage_;
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && target== other.gameObject)
        {
            other.gameObject.GetComponent<EnemyBase>().TakeDamage(damage);
            ReturnObjectPool();
            // myAnim.speed = 1f;
        }
    }

    protected void ReturnObjectPool()
    {
        myOwn.GetComponent<MasterDiceScript>().ReturnObject(gameObject);
    }    



}
