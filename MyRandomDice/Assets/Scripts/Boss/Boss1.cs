using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : BossBase
{
    GameObject Boss1EffectResource;
    GameObject Boss1EffectMyResource;
    GameObject Boss1Effect;
    bool skillOn = false;
    float timer = 0f;
    int count = 0;
    protected override void Start()
    {
        base.Start();

        Boss1EffectResource = Resources.Load<GameObject>("BossEffect/Boss1Effect");
        Boss1EffectMyResource = Resources.Load<GameObject>("BossEffect/Boss1EffectMy");
    }

    protected virtual void Update()
    {
        base.Update();
        if(skillOn)
        {
            timer += Time.deltaTime;
            if(timer>1f)
            {
                timer = 0f;

                if (bBottomBoss)
                    EnemyManager.Instance.EnemySpawnBottom();
                else
                    EnemyManager.Instance.EnemySpawnTop();
                count++;

                if (count == 5)
                    skillOn = false;
            }
        }

    }
    protected override void BossSkill()
    {
        var v = Instantiate(Boss1EffectMyResource, transform.position, Quaternion.identity);
        v.transform.parent = transform;
        Destroy(v, 1f);



        Transform tr = EnemyManager.Instance.GO_SpawnPosition.GetComponent<Transform>();
        if (!bBottomBoss) tr = EnemyManager.Instance.GO_SpawnPosition2.GetComponent<Transform>();
        Boss1Effect = Instantiate(Boss1EffectResource, tr.position, Quaternion.identity);
        Destroy(Boss1Effect, 5f);
        skillOn = true;


    }

    public override void MyDie()
    {
        if (Boss1Effect) Destroy(Boss1Effect);
       base.MyDie();
    }
}
