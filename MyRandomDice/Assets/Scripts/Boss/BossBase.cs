using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : EnemyBase
{
   public bool bBottomBoss = true;
    GameObject playerController;
    GameObject AIcontroller;
   protected PlayerController myPosPlayer;
    
    protected override void Start()
    {
        base.Start();
        
       // if(GameManager.Instance.WaveNumber<=4)
        HP = HP * 20f; 
       
        HP_TextUI.text = HP.ToString();

    }
   public void SetBottom(bool b)
    {
        playerController = GameObject.Find("PlayerController");
        AIcontroller = GameObject.Find("AIcontroller");
        bBottomBoss = b;
        if (bBottomBoss)
            myPosPlayer = playerController.GetComponent<PlayerController>();
        else
            myPosPlayer = AIcontroller.GetComponent<PlayerController>();
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (other.tag == "BossSkillPos")
        {
            BossSkill();
        }
    }
    protected virtual void BossSkill()
    {

    }

    //protected override void MyDie()
    //{
    //    GameManager.Instance.BossDie();
    //    EnemyManager.Instance.RemoveEnemyinLIST(gameObject);
    //    Destroy(this.gameObject);
    
    //}
}
