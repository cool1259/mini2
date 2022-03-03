using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject GO_SpawnPosition;
    public GameObject GO_SpawnPosition2;

    private GameObject enemy_Normal; 
    private GameObject enemy_Small;
    private GameObject enemy_Big;

    private GameObject Boss1;
    private GameObject Boss2;
    private GameObject Boss3;

    private static EnemyManager instance = null;
    private LinkedList<GameObject> enemyList = new LinkedList<GameObject>();
    private LinkedList<GameObject> enemyList2 = new LinkedList<GameObject>();
    private GameObject go_playerController;
    private GameObject go_AIController;
    //Public 프로퍼티로 선언해서 외부에서 private 멤버변수에 접근만 가능하게 구현 
    public static EnemyManager Instance 
    { 
        get 
        { 
            if (null == instance) 
            { 
                return null; 
            }
            return instance; 
        } 
    }

   
    private void Awake()
    {
        if (null == instance)
        { // 씬 시작될때 인스턴스 초기화, 씬을 넘어갈때도 유지되기위한 처리 
            instance = this;
           // DontDestroyOnLoad(this.gameObject);
        }
        else
        { // instance가, GameManager가 존재한다면 GameObject 제거 
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GO_SpawnPosition = GameObject.Find("SpawnPosition");
        GO_SpawnPosition2 = GameObject.Find("SpawnPosition2");
        enemy_Normal = Resources.Load<GameObject>("Enemy/enemy_normal");
        enemy_Small = Resources.Load<GameObject>("Enemy/enemy_small");
        enemy_Big = Resources.Load<GameObject>("Enemy/enemy_big");
        Boss1 = Resources.Load<GameObject>("Enemy/boss_1");
        Boss2 = Resources.Load<GameObject>("Enemy/boss_2");
        Boss3 = Resources.Load<GameObject>("Enemy/boss_3");
        go_playerController = GameObject.Find("PlayerController");
        go_AIController = GameObject.Find("AIcontroller");
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void EM_Boss1Spawn()
    {
        var v1 = Instantiate(Boss1, GO_SpawnPosition.transform.position, Quaternion.identity);
        enemyList.AddLast(v1);
        v1.GetComponent<BossBase>().SetBottom(true);

        var v2 = Instantiate(Boss1, GO_SpawnPosition2.transform.position, Quaternion.identity);
        enemyList2.AddLast(v2);
        v2.GetComponent<BossBase>().SetBottom(false);
    }
    public void EM_Boss2Spawn()
    {
        var v1 = Instantiate(Boss2, GO_SpawnPosition.transform.position, Quaternion.identity);
        enemyList.AddLast(v1);
        v1.GetComponent<BossBase>().SetBottom(true);

        var v2 = Instantiate(Boss2, GO_SpawnPosition2.transform.position, Quaternion.identity);
        enemyList2.AddLast(v2);
        v2.GetComponent<BossBase>().SetBottom(false);
    }
    public void EM_Boss3Spawn()
    {
        var v1 = Instantiate(Boss3, GO_SpawnPosition.transform.position, Quaternion.identity);
        enemyList.AddLast(v1);
        v1.GetComponent<BossBase>().SetBottom(true);

        var v2 = Instantiate(Boss3, GO_SpawnPosition2.transform.position, Quaternion.identity);
        enemyList2.AddLast(v2);
        v2.GetComponent<BossBase>().SetBottom(false);
    }


    public void EnemySpawnTop()
    {
        enemyList2.AddLast(Instantiate(enemy_Normal, GO_SpawnPosition2.transform.position, Quaternion.identity));
    }
    public void EnemySpawnBottom()
    {
        enemyList.AddLast(Instantiate(enemy_Normal, GO_SpawnPosition.transform.position, Quaternion.identity));
    }
    public void EnemySpawnBig()
    {
        enemyList.AddLast(Instantiate(enemy_Big, GO_SpawnPosition.transform.position, Quaternion.identity));
        enemyList2.AddLast(Instantiate(enemy_Big, GO_SpawnPosition2.transform.position, Quaternion.identity));


    }
    public void EnemySpawnSmall()
    {
        enemyList.AddLast(Instantiate(enemy_Small, GO_SpawnPosition.transform.position, Quaternion.identity));
        enemyList2.AddLast(Instantiate(enemy_Small, GO_SpawnPosition2.transform.position, Quaternion.identity));

    }

    public void ClearAllEnemy()
    {
        foreach(var v in enemyList)
        {
            Destroy(v);
        }
        enemyList.Clear();


        foreach (var v in enemyList2)
        {
            Destroy(v);
        }
        enemyList2.Clear();
    }


   
    public void RemoveEnemyinLIST(GameObject gm ,bool isBoss=false)
    {
        int addSpNum = 20;
        if (isBoss) addSpNum = 300;

        if (enemyList.Contains(gm))
        {
            enemyList.Remove(gm);
            if (go_playerController)
                go_playerController.GetComponent<PlayerController>().AddMySp(addSpNum);
       
        }
        else
        {
            enemyList2.Remove(gm);
            if (go_AIController)
                go_AIController.GetComponent<PlayerController>().AddMySp(addSpNum);
        }

        if(GameManager.Instance.bBossTime)
        {
            if (enemyList.Count == 0 && enemyList2.Count == 0 && !GameManager.Instance.bGameOver)
                GameManager.Instance.StartWave();
        }
    }

    public GameObject GetFrontEnemy()
    {
        if (enemyList.Count == 0) return null;


        GameObject frontEnemy = enemyList.First.Value;
        // Transform tr = frontEnemy.transform;
        foreach (var enemy in enemyList)
        {

            if (frontEnemy.GetComponent<EnemyBase>().mode == enemy.GetComponent<EnemyBase>().mode)
            {


                if (frontEnemy.GetComponent<EnemyBase>().mode == 2)
                {
                    if (enemy.transform.position.y < frontEnemy.transform.position.y)
                    {
                        frontEnemy = enemy;
                        continue;
                    }
                }
                else if (frontEnemy.GetComponent<EnemyBase>().mode == 1)
                {
                    if (enemy.transform.position.x > frontEnemy.transform.position.x)
                    {
                        frontEnemy = enemy;
                        continue;
                    }
                }
                else if (frontEnemy.GetComponent<EnemyBase>().mode == 0)
                {
                    if (enemy.transform.position.y > frontEnemy.transform.position.y)
                    {
                        frontEnemy = enemy;
                        continue;
                    }
                }

            }
            else
            {
                if (frontEnemy.GetComponent<EnemyBase>().mode < enemy.GetComponent<EnemyBase>().mode)
                {
                    frontEnemy = enemy;
                    continue;
                }
            }

        }

        return frontEnemy;
            //return enemyList.First.Value;
    }

    public GameObject GetFrontEnemyAI()
    {
        if (enemyList2.Count == 0) return null;


        GameObject frontEnemy = enemyList2.First.Value;
        // Transform tr = frontEnemy.transform;
        foreach (var enemy in enemyList2)
        {

            if (frontEnemy.GetComponent<EnemyBase>().mode == enemy.GetComponent<EnemyBase>().mode)
            {


                if (frontEnemy.GetComponent<EnemyBase>().mode == 2)
                {
                    if (enemy.transform.position.y < frontEnemy.transform.position.y)
                    {
                        frontEnemy = enemy;
                        continue;
                    }
                }
                else if (frontEnemy.GetComponent<EnemyBase>().mode == 3)
                {
                    if (enemy.transform.position.x < frontEnemy.transform.position.x)
                    {
                        frontEnemy = enemy;
                        continue;
                    }
                }
                else if (frontEnemy.GetComponent<EnemyBase>().mode == 0)
                {
                    if (enemy.transform.position.y > frontEnemy.transform.position.y)
                    {
                        frontEnemy = enemy;
                        continue;
                    }
                }

            }
            else
            {
                if (enemy.transform.position.x < frontEnemy.transform.position.x)
                {
                    frontEnemy = enemy;
                    continue;
                }
            }

        }

        return frontEnemy;
        //return enemyList.First.Value;
    }


    public LinkedList<GameObject> GetEnemyList()
    {

            return enemyList;
    }

    public LinkedList<GameObject> GetEnemyListAI()
    {

        return enemyList2;
    }

    public int targetNum()
    {

        return enemyList.Count;
    }
    public int targetNumAI()
    {

        return enemyList2.Count;
    }
}
