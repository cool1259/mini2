using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorrnBullet : Bullet
{

    float LifeTime = 0f;
    float LifeTimer = 6f;
    // Start is called before the first frame update
    protected override void Start()
    {
       // Invoke("ReturnObjectPool", 3f);
    }
    private void OnEnable()
    {
        LifeTime = 0f;
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        LifeTime += Time.deltaTime;
        if (LifeTime >= LifeTimer)
        {
           
            ReturnObjectPool();
        }
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        
      
    }
    protected void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.tag == "Enemy")
        {
          
            other.gameObject.GetComponent<EnemyBase>().TakeDamage(damage);

        }
    }
}
