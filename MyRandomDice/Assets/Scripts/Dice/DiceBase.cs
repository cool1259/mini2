using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBase : MonoBehaviour
{
    private GameObject go_DiceEyes;
    private GameObject DiceEyes_instance;
    protected int diceLevel = 1;
    private int max_dice_level = 6;
    public bool isDrag = false;
    private Vector3 origin_pos;
    private GameObject stayDice;
    protected string diceName = "";
    protected GameObject go_playerController;
    public int pos_num;
    public int DiceLevel { get { return diceLevel; } set { diceLevel = value; } }
    //  public GameObject Go_playerController{set{ go_playerController = value; }}
    public int DicePower = 1;
    public GameObject myUpgradeButton;
    protected GameObject mySuperVisor;
    public GameObject ColorBox;
    public GameObject PowerUpCanvas_Resource;
    public GameObject PowerUpCanvas_Instance;

    protected GameObject myBulletResouce;

    protected float attackTurm = 0.5f;
    protected float attackTimer = 0f;
    protected float Dice_Damage = 100f;
    protected float CriticalDamageCoefficient = 1f;
    protected float DamageUp_Coefficient = 50f;
    protected float AttackSpeedUp_Coefficient = 0.04f;
    bool bColorSetting = false;

    public int CriticalBuffNum = 0;

    GameObject buffCriticalResouce;

    public bool isAI = false;
    public GameObject SpEffectResource;

    private GameObject Boss2Effect;
    private GameObject Boss2EffectResource;
    private GameObject Boss3EffectResource;
    public bool bBoss2Debuff = false;


    public void SetMyPlayer(GameObject player_)
    {
        go_playerController = player_;
    }
    public void SetPos(int pos_num_)
    {
        pos_num = pos_num_;
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {

        //DiceEyes_instance.transform.GetChild(0).gameObject.SetActive(true);

        myBulletResouce = Resources.Load<GameObject>("Bullet/NormalBullet");
        buffCriticalResouce = Resources.Load<GameObject>("Effect/buff_critical");
        SpEffectResource = Resources.Load<GameObject>("Effect/SpEffectPrefab");
        PowerUpCanvas_Resource = Resources.Load<GameObject>("PowerUpCanvas");
        PowerUpCanvas_Instance = Instantiate(PowerUpCanvas_Resource, transform.position + new Vector3(0f, 0.2f, 0f), Quaternion.identity);
        //PowerUpCanvas_Instance.transform.parent = transform;
        PowerUpCanvas_Instance.SetActive(false);

        Boss2EffectResource = Resources.Load<GameObject>("BossEffect/Boss2Effect");
        Boss3EffectResource = Resources.Load<GameObject>("BossEffect/Boss3Effect");



        origin_pos = transform.position;




        mySuperVisor = DiceManager.Instance.GetDicePair(gameObject.name);
        mySuperVisor.GetComponent<MasterDiceScript>().myDelegate += DicePowerUp;
        DicePower = mySuperVisor.GetComponent<MasterDiceScript>().DicePower;


        Invoke("ResearchAround", 0.5f);
        GameManager.Instance.StartWaveAction += NewWave;

        InvokeRepeating("AICombine", 1.5f,1.5f);

       
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.bGameOver) return;
     
        CanvasTime();

        if (bBoss2Debuff) return;
        if (isAI && EnemyManager.Instance.targetNumAI()>0) Attack();
        if (!isAI && EnemyManager.Instance.targetNum() > 0) Attack();

       
    }

    public void SetDiceLevel(int n)
    {
        diceLevel = n;
        go_DiceEyes = Resources.Load<GameObject>("DiceEyes");
        DiceEyes_instance = Instantiate(go_DiceEyes, transform.position, Quaternion.identity);
        DiceEyes_instance.transform.parent = transform;
        DiceEyes_instance.transform.GetChild(n-1).gameObject.SetActive(true);
       
        if(!bColorSetting)
        SetDiceEyesColor();
    }
    private void SetDiceEyesColor()
    {
        bColorSetting = true;
        //Color[] arrayColor = new Color[10];
        //arrayColor[0]= new Color(255f, 0f, 0f);
        //arrayColor[1] = new Color(243f, 195f, 60f);
        //arrayColor[2] = new Color(60f, 224f, 243f);
        //arrayColor[3] = new Color(54f, 215f, 57f);
        //arrayColor[4] = new Color(54f, 188f, 214f);
        //arrayColor[5] = new Color(140f, 170f, 170f);
        //arrayColor[6] = new Color(80f, 170f, 60f);
        //arrayColor[7] = new Color(60f, 200f, 200f);
        //arrayColor[8] = new Color(255f, 170f, 60f);
        //arrayColor[9] = new Color(240f, 255f, 0f);



        //if (gameObject.name.Contains("01")) mycolor = new Color(255f, 0f, 0f);
        //else if (gameObject.name.Contains("02")) mycolor = new Color(243f, 195f, 60f);
        //else if (gameObject.name.Contains("03")) mycolor = new Color(60f, 224f, 243f);
        //else if (gameObject.name.Contains("04")) mycolor = new Color(54f, 215f, 57f);
        //else if (gameObject.name.Contains("05")) mycolor = new Color(54f, 188f, 214f);
        //else if (gameObject.name.Contains("06")) mycolor = new Color(140f, 170f, 170f);
        //else if (gameObject.name.Contains("12")) mycolor = new Color(80f, 170f, 60f);
        //else if (gameObject.name.Contains("16")) mycolor = new Color(60f, 200f, 200f);
        //else if (gameObject.name.Contains("17")) mycolor = new Color(255f, 170f, 60f);
        //else if (gameObject.name.Contains("18")) mycolor = new Color(240f, 255f, 0f);

        Color mycolor = new Color(255f, 255f, 255f);

        ColorBox = GameObject.Find("ColorBox");


        for (int i = 0; i < DiceEyes_instance.transform.childCount; i++)
        {
            for (int j = 0; j < DiceEyes_instance.transform.GetChild(i).transform.childCount; j++)
            {
                var eye = DiceEyes_instance.transform.GetChild(i).transform.GetChild(j).gameObject;

                eye.GetComponent<SpriteRenderer>().sortingOrder = 1;



                if (gameObject.name.Contains("01")) eye.GetComponent<SpriteRenderer>().color = ColorBox.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                else if (gameObject.name.Contains("14")) eye.GetComponent<SpriteRenderer>().color = ColorBox.transform.GetChild(8).GetComponent<SpriteRenderer>().color;
                else if (gameObject.name.Contains("03")) eye.GetComponent<SpriteRenderer>().color = ColorBox.transform.GetChild(2).GetComponent<SpriteRenderer>().color;
                else if (gameObject.name.Contains("10")) eye.GetComponent<SpriteRenderer>().color = ColorBox.transform.GetChild(4).GetComponent<SpriteRenderer>().color;
                else if (gameObject.name.Contains("05")) eye.GetComponent<SpriteRenderer>().color = ColorBox.transform.GetChild(4).GetComponent<SpriteRenderer>().color;
                else if (gameObject.name.Contains("06")) eye.GetComponent<SpriteRenderer>().color = ColorBox.transform.GetChild(5).GetComponent<SpriteRenderer>().color;
                else if (gameObject.name.Contains("12")) eye.GetComponent<SpriteRenderer>().color = ColorBox.transform.GetChild(6).GetComponent<SpriteRenderer>().color;
                else if (gameObject.name.Contains("16")) eye.GetComponent<SpriteRenderer>().color = ColorBox.transform.GetChild(7).GetComponent<SpriteRenderer>().color;
                else if (gameObject.name.Contains("15")) eye.GetComponent<SpriteRenderer>().color = ColorBox.transform.GetChild(2).GetComponent<SpriteRenderer>().color;
                else if (gameObject.name.Contains("18")) eye.GetComponent<SpriteRenderer>().color = ColorBox.transform.GetChild(9).GetComponent<SpriteRenderer>().color;

            }
        }
    }
    protected virtual void Attack()
    {


        attackTimer += Time.deltaTime;
        if (attackTimer >= attackTurm - (DicePower-1)*AttackSpeedUp_Coefficient)
        {
            attackTimer = 0f;
            // Instantiate(myBulletResouce, transform.position, Quaternion.identity).GetComponent<Bullet>().OnTarget(EnemyManager.Instance.GetFrontEnemy());
            var ob=mySuperVisor.GetComponent<MasterDiceScript>().GetObject();
            ob.GetComponent<Bullet>().myOwn = mySuperVisor;
            ob.transform.position = transform.position;
            ob.GetComponent<Bullet>().SetMyDamage(GetDiceDamage());
            if(!isAI)
                ob.GetComponent<Bullet>().OnTarget(EnemyManager.Instance.GetFrontEnemy());
            else
                ob.GetComponent<Bullet>().OnTarget(EnemyManager.Instance.GetFrontEnemyAI());

        }
    }
    public float GetDiceDamage()
    {
        return Dice_Damage * CriticalDamageCoefficient + ((diceLevel - 1) * DamageUp_Coefficient) + CriticalDamageCoefficient;
    }

    public void DiceLevelUp()
    {
        if (DiceLevel >= max_dice_level) return;


        // DiceEyes_instance.transform.GetChild(DiceLevel - 1).gameObject.SetActive(false);
        //DiceLevel = DiceLevel + 1;
        //DiceEyes_instance.transform.GetChild(DiceLevel - 1).gameObject.SetActive(true);
        go_playerController.GetComponent<PlayerController>().CreateDice(DiceLevel + 1, pos_num, isAI);
        DiceDie();

    }

    //보스가 쓸거임
    public void DiceChange()
    {
        
        go_playerController.GetComponent<PlayerController>().CreateDice(DiceLevel, pos_num,isAI);
        DiceDie();

    }
    public void DragStart()
    {
        if (bBoss2Debuff) return;

        

        //origin_pos = transform.position; //돌아올자리
        isDrag = true;
        CanvasOff();
    }
    public void DragEnd()
    {
        isDrag = false;
        //충돌중이면 나는사라지고 개를업그레이드함
        if (stayDice == null)
            transform.position = origin_pos; //짝없으면 원래위치로
        else //합체
        {
            DiceCombine(stayDice);
        }

    }
    private void OnTriggerStay2D(Collider2D other) //enter로하면 가까운다른애에서 exit되면잘안합쳐질때가있음
    {

        if (other.gameObject.tag == "Dice")
        {

            if (stayDice == other.gameObject) return;//이미얘가 짝이면

            //떠다니는애가 나랑같은 레벨의 다이스와 충돌함
            if (transform.position!=origin_pos)
            {
                if(DiceCombineCheck(other.gameObject))
                    stayDice = other.gameObject;
            }
               

            
        }

      
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //짝이되지못하고 떠남
        if (stayDice != null)
            stayDice = null;


   
    }

    //이거 델리게이트로 
    //그버튼함수를 다이스들이 구독함
    //버튼을누르면 그해당하는 모든 다이스들은 저절로 이함수 호출
    //왜냐 이미 구독했기 때문
    //오버라이딩해서 쓸것
    public virtual void DicePowerUp()
    {
        DicePower = mySuperVisor.GetComponent<MasterDiceScript>().DicePower;
        //Debug.Log("델리게이트업22 " + DicePower);
        CanvasOn();

       
    }

   
    public float canvas_speed = 2f;
    bool canvasMove = false;
    float canvasTimer = 0f;
    float canvasMaxTime = 0.5f;
    private void CanvasOn()
    {
        canvas_speed = 2f;
        canvasTimer = 0f;
        canvasMove = true;
        PowerUpCanvas_Instance.SetActive(true);
        PowerUpCanvas_Instance.transform.position = GetComponent<Transform>().position+new Vector3(0f,0.2f,0f);
    }

    private  void CanvasOff()
    {
        canvasMove = false;
        PowerUpCanvas_Instance.SetActive(false);
    }
    private void CanvasTime()
    {
        if (canvasMove)
        {
            PowerUpCanvas_Instance.transform.position += new Vector3(0f, 0.1f, 0f) * Time.deltaTime * canvas_speed;
            canvasTimer += Time.deltaTime;
            if (canvasTimer > canvasMaxTime) CanvasOff();
        }
    }

    protected virtual void DiceDie(bool ReturnInUsePos=false)
    {

        //사망처리
        if (buffCiriticalEffect)
        Destroy(buffCiriticalEffect);
        if(Boss2Effect)
            Destroy(Boss2Effect);

        if (ReturnInUsePos)
            go_playerController.GetComponent<PlayerController>().DiceReduce(pos_num);



        go_playerController.GetComponent<PlayerController>().ReturnInUseDice(pos_num);

        CanvasOff();
        mySuperVisor.GetComponent<MasterDiceScript>().myDelegate -= DicePowerUp; //아니 그냥델리게이트에 ?.Invoke()하면됨


        if (gameObject.name == "14_critical(Clone)")
            gameObject.GetComponent<CriticalDice>().ArroundBuffOff();
        Destroy(gameObject);
    }

    GameObject buffCiriticalEffect;
    bool BuffOn = false;
    public void SetIncreaseCriticalBuff()
    {
        if (name == "14_critical(Clone)") return;

        //크리티컬장착
        if (BuffOn==false)
        {
            BuffOn = true;
            CriticalDamageCoefficient = 2f;
            if(buffCriticalResouce)
            buffCiriticalEffect=Instantiate(buffCriticalResouce, transform.position + new Vector3(0f, 0.1f, 0f), Quaternion.identity);
        }

    }


    public void SetDecreaseCriticalBuff()
    {
        if (name == "14_critical(Clone)") return;


        //크리티컬해제
        if (BuffOn==true)
        {
            BuffOn = false;
            CriticalDamageCoefficient = 1f;
            Destroy(buffCiriticalEffect);
        }
        ResearchAround();

    }
    private void ResearchAround()
    {

        if (name == "14_critical(Clone)") return;
     
        var arr = go_playerController.GetComponent<PlayerController>().Dice_Pos_Dice_Array;

        // 주변버프
        if (pos_num % 5 != 0)
        {
            if (arr[pos_num + 1] != null && arr[pos_num + 1].name == "14_critical(Clone)")
                SetIncreaseCriticalBuff();
        }
        if (pos_num != 1 && pos_num != 6 && pos_num != 11)
        {
            if (arr[pos_num - 1] != null && arr[pos_num - 1].name == "14_critical(Clone)")
                SetIncreaseCriticalBuff();
           
        }
        if (pos_num <= 10 && arr[pos_num + 5] != null && arr[pos_num + 5].name == "14_critical(Clone)")
            SetIncreaseCriticalBuff();
        if (pos_num >= 5 && arr[pos_num - 5] != null && arr[pos_num -5].name == "14_critical(Clone)")
            SetIncreaseCriticalBuff();
    }


    
    public void Boss2Debuff()
    {
        bBoss2Debuff = true;
       Boss2Effect = Instantiate(Boss2EffectResource, transform.position , Quaternion.identity);
        Boss2Effect.transform.parent = transform;
    }
    public void Boss3Debuff()
    {

        Destroy(Instantiate(Boss3EffectResource, transform.position + new Vector3(0f, 0.2f, 0f), Quaternion.identity),0.3f);
        DiceChange();
    }
    private void NewWave()
    {
        if (Boss2Effect)
        {
            Destroy(Boss2Effect);
            bBoss2Debuff = false;
        }
    }
    private bool DiceCombineCheck(GameObject otherDice)
    {
       if( otherDice.gameObject.GetComponent<DiceBase>().DiceLevel == DiceLevel && DiceLevel < max_dice_level)
       {
            //종류도같아야됨
            if (gameObject.name == otherDice.gameObject.name || ((gameObject.name == "18_mimic(Clone)") || otherDice.gameObject.name == "18_mimic(Clone)"))
                return true;
       }
        return false;
    }
    private void DiceCombine(GameObject otherDice)
    {
        if (otherDice.GetComponent<DiceBase>().bBoss2Debuff)
        {
            transform.position = origin_pos;
            return;
        }


        //왜 둘다 검사하냐-> mimic주사위 떄문에
        if (gameObject.name == "16_sacrifice(Clone)" || otherDice.name == "16_sacrifice(Clone)")
        {
            go_playerController.GetComponent<PlayerController>().AddMySp(100 + (diceLevel - 1) * 50);
            if(!isAI)
            Instantiate(SpEffectResource, stayDice.transform.position + new Vector3(0f, 0.2f, 0f), Quaternion.identity);

        }

        otherDice.GetComponent<DiceBase>().DiceLevelUp();


        DiceDie(true);
    }


    private void AICombine()
    {
        if (isAI)
        {
            for (int i = 1; i <= 15; i++)
            {
                if (i == pos_num) return;//나자신
                if (pos_num < i) return; //한쪽만실행하게
                if (go_playerController.GetComponent<PlayerController>().Dice_Pos_Dice_Array[i] != null)
                {
                    var otherDice = go_playerController.GetComponent<PlayerController>().Dice_Pos_Dice_Array[i];
                    if (DiceCombineCheck(otherDice))
                    {
                       DiceCombine(otherDice);
                        return;
                    }


                }

            }
        }
    }

}
