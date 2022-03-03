using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class PlayerController : MonoBehaviour
{

    public Transform[] dice_Pos=new Transform[16];
    private List<int> enable_DicePos_List = new List<int>();
    private int remain_Dice_Num = 0;
    private int max_Dice_Num = 15;
    bool deckEnable = false;
    private int mySp=400;

    private List<GameObject> myDice_Instance_List = new List<GameObject>();
    public LinkedList<int> player_InUse_Dice_List = new LinkedList<int>();
    public GameObject[] Dice_Pos_Dice_Array = new GameObject[16];
    private Dictionary<string, int> DiceCountHash= new Dictionary<string, int>();
    public Text SpText;
    public bool isAI=false;

    // Start is called before the first frame update
    void Start()
    {
       

        if (!isAI)
        {
            for (int i = 1; i <= 15; i++)
            {
                string s = i.ToString();
                dice_Pos[i] = GameObject.Find("Pos/pos" + s).transform;
                enable_DicePos_List.Add(i);
            }
        }
        else
        {
            for (int i = 1; i <= 15; i++)
            {
                string s = i.ToString();
                dice_Pos[i] = GameObject.Find("Pos2/pos" + s).transform;
                enable_DicePos_List.Add(i);
            }
        }
        //for (int i = 1; i <= 5; i++)
        //{
        //    string s = i.ToString();
           
        //}
        if(!isAI)
            DiceManager.Instance.DeckChoiceFinish_delegate += DiceDeckOn;
       else
            DiceManager.Instance.DeckChoiceFinish_delegate += DiceDeckOnAI;

    }
    float aiSpawnnTimer = 0f;
    float aiSpawnnTime = 2f;
    int spawnCount = 0;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.bGameOver) return;
       


        if (isAI && deckEnable)
        {
            
           
            aiSpawnnTimer += Time.deltaTime;
            if (aiSpawnnTimer > aiSpawnnTime)
            {
                aiSpawnnTimer = 0f;
                //Debug.Log(mySp+"   으아ㅏㅏㅏㅏㅏㅁ나ㅣㅇㄻ니임나이ㅏㅁ이ㅏㄴㅁㅇㄴ미ㅏㅁ");
               
                if (mySp < 100) return;
                if (GameManager.Instance.WaveNumber < 3) CreateDice();
                else
                {
                    if (spawnCount >= 3)
                    {
                        spawnCount = 0;
                        if (!AIPowerUp())
                            CreateDice();
                    }
                    else
                    {
                        CreateDice();

                    }
                    spawnCount++;
                }
            }
        }
    }

    //버튼용
    public void CreateDice()
    {
        if (deckEnable == false) return;
        if (remain_Dice_Num == max_Dice_Num)
            return;

        if (mySp < 100) return;
        AddMySp(-100);
        
        int pos_num;
        int list_index = Random.Range(0, enable_DicePos_List.Count);
        pos_num = enable_DicePos_List[list_index]; //남은자리중 랜덤한위치선택


        remain_Dice_Num++;

        var go=Instantiate(myDice_Instance_List[Random.Range(0, myDice_Instance_List.Count)], dice_Pos[pos_num].position, Quaternion.identity);
        go.GetComponent<DiceBase>().SetPos(pos_num);
        go.GetComponent<DiceBase>().SetDiceLevel(1);
        go.GetComponent<DiceBase>().SetMyPlayer(gameObject);

        if (isAI)
            go.GetComponent<DiceBase>().isAI = true;
            

            
      
        Dice_Pos_Dice_Array[pos_num] = go;
        player_InUse_Dice_List.AddLast(pos_num);

        enable_DicePos_List.RemoveAt(list_index); //이자리이제못씀
        //나중에 주사위 합칠때
        //그주사위자리를 다시 enable_DicePos_List에 add함 remain_Dice_Num도 하나줄이고

        
    }

    //합체용
    public void CreateDice(int level_,int pos_num_,bool isAI_)
    {
        if (deckEnable == false) return;
  
        var go = Instantiate(myDice_Instance_List[Random.Range(0, myDice_Instance_List.Count)], dice_Pos[pos_num_].position, Quaternion.identity);
        go.GetComponent<DiceBase>().SetPos(pos_num_);
        go.GetComponent<DiceBase>().SetDiceLevel(level_);
        go.GetComponent<DiceBase>().SetMyPlayer(gameObject);

     

        Dice_Pos_Dice_Array[pos_num_] = go;
        player_InUse_Dice_List.AddLast(pos_num_);
        if (isAI)
            go.GetComponent<DiceBase>().isAI = true;
      
       
        
       

    }
    public void DiceReduce(int pos_number)
    {
       remain_Dice_Num--;
        enable_DicePos_List.Add(pos_number);

        Dice_Pos_Dice_Array[pos_number] = null;
    }

    public void ReturnInUseDice(int pos_num_)
    {
        player_InUse_Dice_List.Remove(pos_num_);
    }

    public void DiceDeckOn()
    {
      
        var deck = DiceManager.Instance.GetDeck(1);
        foreach (var dice in deck)
        {
            myDice_Instance_List.Add(Resources.Load<GameObject>("Dice/" + dice.gameObject.name));
        }
        deckEnable = true;
    }
    public void DiceDeckOnAI()
    {

        var deck = DiceManager.Instance.GetDeck(2);
        foreach (var dice in deck)
        {
            myDice_Instance_List.Add(Resources.Load<GameObject>("Dice/" + dice.gameObject.name));
        }
        deckEnable = true;
    }
    public void AddMySp(int sp)
    {
        mySp = mySp + sp;
       // Debug.Log("멍꼬 " + mySp);
        if (SpText && !isAI)
            SpText.text = mySp.ToString();
    }
    public int GetMySp()
    {
        return mySp;
    }

   
    private bool AIPowerUp()
    {
        DiceCountHash.Clear();
       // Debug.Log("흠냐" + DiceCountHash.Count);
        //board 돌면서 해시에 주사기 종류 별로 레벨 저장 (종류별로 주사위 레벨의 합을 구해야됨)
        for (int i = 1; i <= 15; i++)
        {
            if (Dice_Pos_Dice_Array[i] != null && Dice_Pos_Dice_Array[i].GetComponent<DiceBase>().DicePower<5)
            {
                if (!DiceCountHash.ContainsKey(Dice_Pos_Dice_Array[i].name))
                    DiceCountHash.Add(Dice_Pos_Dice_Array[i].name, 0);

                 DiceCountHash[Dice_Pos_Dice_Array[i].name]+= Dice_Pos_Dice_Array[i].GetComponent<DiceBase>().DiceLevel;

            }
        }
        //가장높은 주사위레벨가진 주사위 powerUp 근데 전부 만랩이면 리턴펠스(이러면 ai는 그냥  createDice함)
        var sortedDict = from entry in DiceCountHash orderby entry.Value descending select entry;
        foreach(var v in sortedDict)
        {
           
            DiceManager.Instance.DicePowerUp_inDiceManager(v.Key);
            return true;
            
        }
    
        return false;
    }

  
}
