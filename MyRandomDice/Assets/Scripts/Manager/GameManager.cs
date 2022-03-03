using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
   public  bool bGameOver = false;
    public int WaveNumber = 0;
    bool SpawnRepeat = false;
    float spawnTime=0f;
    // instance 멤버변수는 private하게 선언 
    private static GameManager instance = null;

    public GameObject BossUIAll;
    public GameObject BossUI1;
    public GameObject BossUI2;
    public GameObject BossUI3;
    public Text BossTimeUI;
    public Text CenterTextUI;


    private int TopUserLife=3;
    private int BottomUserLife = 3;

    private GameObject TopUserHPUI1;
    private GameObject TopUserHPUI2;
    private GameObject TopUserHPUI3;
    private GameObject BottomUserHPUI1;
    private GameObject BottomUserHPUI2;
    private GameObject BottomUserHPUI3;


    private GameObject ResultCanvas;
    private GameObject StartUI;
    private GameObject ResultText;
    //Public 프로퍼티로 선언해서 외부에서 private 멤버변수에 접근만 가능하게 구현 
    public static GameManager Instance
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
        Screen.SetResolution(1440, 2960, true);
        bGameOver = false;
        BossUIAll = GameObject.Find("BossUI");
        BossUI1 = BossUIAll.transform.GetChild(0).gameObject; // GameObject.Find("icon_boss_1");
        BossUI2 = BossUIAll.transform.GetChild(1).gameObject; //GameObject.Find("icon_boss_2");
        BossUI3 = BossUIAll.transform.GetChild(2).gameObject; //GameObject.Find("icon_boss_3");
        BossTimeUI = BossUIAll.transform.GetChild(3).gameObject.GetComponent<Text>(); //GameObject.Find("BossTime");
        CenterTextUI = BossUIAll.transform.GetChild(4).gameObject.GetComponent<Text>(); //GameObject.Find("BossTime");

        BossUIAll.SetActive(true);


        TopUserHPUI1 = GameObject.Find("TopLife1");
        TopUserHPUI2 = GameObject.Find("TopLife2");
        TopUserHPUI3 = GameObject.Find("TopLife3");
        BottomUserHPUI1 = GameObject.Find("BottomLife1");
        BottomUserHPUI2 = GameObject.Find("BottomLife2");
        BottomUserHPUI3 = GameObject.Find("BottomLife3");

        ResultCanvas= GameObject.Find("ResultCanvas");
        ResultText = GameObject.Find("ResultText");
        StartUI = GameObject.Find("StartUI");
        ResultCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            BossPick = 1;
            BossSpawn();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BossPick = 2;
            BossSpawn();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            BossPick = 3;
            BossSpawn();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {

            SceneManager.LoadScene("SampleScene");
        }
        if (SpawnRepeat)
        {

            //잠시
           GM_EnemySpawn();


            bossTime -= Time.deltaTime;
            if(bossTime>=0f)
                BossTimeUI.text = ((int)bossTime).ToString();
            else if (notBoss)
            {
                notBoss = false;
                //보스스폰
                BossSpawn();//이거 enemy들이 start때 델리게이트구독해서 이때 모두 사라지게함 (sp는안주고)
            }
           

        }
       
    }
    int spawnCount = 0;
    float spawnFixedTime = 1f;
    bool bigSpawn = false;
    private void GM_EnemySpawn()
    {
        spawnTime += Time.deltaTime;

        spawnFixedTime = 1f;
        if (spawnCount==8 ) spawnFixedTime = 1.4f;
        if (spawnCount==9) spawnFixedTime = 1.4f;


        if (spawnTime >= spawnFixedTime)
        {
            spawnTime = 0f;

            if (spawnCount == 8)
            {
                EnemyManager.Instance.EnemySpawnBig();

            }
            else if (spawnCount == 10)
            {
                EnemyManager.Instance.EnemySpawnSmall();
                EnemyManager.Instance.EnemySpawnBottom();
                EnemyManager.Instance.EnemySpawnTop();
                spawnCount = 1;
            }
            else
            {
                EnemyManager.Instance.EnemySpawnBottom();
                EnemyManager.Instance.EnemySpawnTop();
            }

            spawnCount++;
        }
    }
    public void StartGame()
    {
        StartWave();
        StartUI.SetActive(false);
    }

    bool notBoss=true;
   // private int remainBoss=0;//ai꺼랑 내꺼 //보스가죽으면 이거감소시키고 이거 0되면 다시 startwave
    float bossTime = 100f;
    int BossPick = 1;
    public event Action StartWaveAction;
    public bool bBossTime = false;

    public void StartWave()
    {
        StartWaveAction?.Invoke();
        CenterTextUI.text ="wave "+WaveNumber +" 남은시간 :";
        WaveNumber++;
        BossUI1.SetActive(false);
        BossUI2.SetActive(false);
        BossUI3.SetActive(false);
        bossTime = 40f;

        SpawnRepeat = true;
        BossUIAll.SetActive(true);
        notBoss = true;
        bBossTime = false;
        if (BossPick==1)
        {
            BossUI1.SetActive(true);
        }
        else if (BossPick == 2)
        {
            BossUI2.SetActive(true);
        }
        else if (BossPick == 3)
        {
            BossUI3.SetActive(true);
        }
       
    }
    private void BossSpawn()
    {
  
        SpawnRepeat = false;
        EnemyManager.Instance.ClearAllEnemy();
        if (BossPick == 1)
            EnemyManager.Instance.EM_Boss1Spawn();
        else if (BossPick == 2)
            EnemyManager.Instance.EM_Boss2Spawn();
        else if (BossPick == 3)
            EnemyManager.Instance.EM_Boss3Spawn();
        bBossTime = true;

        BossPick++;
        if (BossPick == 4) BossPick = 1;
    }
    

    public void ReStartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

   
    public void TopUserLifeDown()
    {
        if (TopUserLife == 3)
            TopUserHPUI1.SetActive(false);
        else if (TopUserLife == 2)
            TopUserHPUI2.SetActive(false);
        else if (TopUserLife == 1)
            TopUserHPUI1.SetActive(false);


        TopUserLife--;

        if(TopUserLife==0)
        {
            GameEnd(true);
        }

    }

    public void BottomUserLifeDown()
    {
        if (BottomUserLife == 3)
            BottomUserHPUI1.SetActive(false);
        else if (BottomUserLife == 2)
            BottomUserHPUI2.SetActive(false);
        else if (BottomUserLife == 1)
            BottomUserHPUI3.SetActive(false);
        BottomUserLife--;

        if (BottomUserLife == 0)
        {
            GameEnd(false);
        }
  
    }
    
    private void GameEnd(bool win)
    {
        SpawnRepeat = false;
        EnemyManager.Instance.ClearAllEnemy();

        ResultCanvas.SetActive(true);
        bGameOver = true;
        if (win)
            ResultText.GetComponent<Text>().text = "승리";
        else
            ResultText.GetComponent<Text>().text = "패배";

    }

}
