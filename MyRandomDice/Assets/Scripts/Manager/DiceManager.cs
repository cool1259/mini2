using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DiceManager : MonoBehaviour
{
    public Transform[] Dice_Button_Pos = new Transform[11];

    private List<GameObject> ALL_Dice = new List<GameObject>();
    private List<int> Remain_dice_Index = new List<int>();

    private List<GameObject> player1_Dice_List = new List<GameObject>();
    private List<GameObject> player2_Dice_List = new List<GameObject>();

    public Transform[] ChoiceDice_Tr  = new Transform[2];
    Dictionary<string, GameObject> DicePair = new Dictionary<string, GameObject>();
    int select1;
    int select2;

    int selectDiceNumber =1;

    public event Action DeckChoiceFinish_delegate;
    public bool DeckChoiceIng = true;


    bool moveDiceButton1 = false;
    bool moveDiceButton2 = false;

    // instance 멤버변수는 private하게 선언 
    private static DiceManager instance = null;
    //Public 프로퍼티로 선언해서 외부에서 private 멤버변수에 접근만 가능하게 구현 
    public static DiceManager Instance
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



        for (int i = 1; i <= 10; i++)
        {
            string s = i.ToString();
            Dice_Button_Pos[i] = GameObject.Find("powerUp_pos" + s).transform;
        }
        int allDiceNum = GameObject.Find("DiceButton").transform.childCount;
        for (int i = 0; i < allDiceNum; i++)
        {

            GameObject go = GameObject.Find("DiceButton").transform.GetChild(i).gameObject;
            ALL_Dice.Add(go);
            DicePair.Add(go.name + "(Clone)", go);
            ALL_Dice[i].gameObject.SetActive(false);
            Remain_dice_Index.Add(i);
        }
        Dice_Choice();
    }

    //// Start is called before the first frame update
    //void Start()
    //{
    //    for (int i = 1; i <= 10; i++)
    //    {
    //        string s = i.ToString();
    //        Dice_Button_Pos[i] = GameObject.Find("powerUp_pos" + s).transform;
    //    }
    //    int allDiceNum = GameObject.Find("DiceButton").transform.childCount;
    //    for (int i = 0; i < allDiceNum; i++)
    //    {

    //        GameObject go = GameObject.Find("DiceButton").transform.GetChild(i).gameObject;
    //        ALL_Dice.Add(go);
    //        DicePair.Add(go.name+"(Clone)", go);
    //        ALL_Dice[i].gameObject.SetActive(false);
    //        Remain_dice_Index.Add(i);
    //    }
    //    Dice_Choice();
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
    public  void player1_AddDice(GameObject dice_button)
    {
        if (dice_button.GetComponent<MasterDiceScript>().ButtonActive == true) return;
        dice_button.GetComponent<MasterDiceScript>().ButtonActive = true;
        dice_button.GetComponent<MasterDiceScript>().isMyPick = true;

        player1_Dice_List.Add(dice_button);
        // dice_button.transform.position = Dice_Button_Pos[selectDiceNumber].position;
        dice_button.GetComponent<MasterDiceScript>().MoveToPlace(Dice_Button_Pos[selectDiceNumber].position);
        dice_button.transform.localScale = new Vector3(0.57f, 3f, 1f);


        //선택안한쪽은 상대에게로
        if (dice_button == ALL_Dice[select1])
            player2_AddDice(ALL_Dice[select2]);
        else if (dice_button == ALL_Dice[select2])
            player2_AddDice(ALL_Dice[select1]);


        selectDiceNumber++; 
        if (selectDiceNumber <= 5)
            Dice_Choice();

        if (selectDiceNumber == 6)
            DeckChoiceFinish();


    }
    public void player2_AddDice(GameObject dice_button)
    {
        if (dice_button.GetComponent<MasterDiceScript>().ButtonActive == true) return;
        dice_button.GetComponent<MasterDiceScript>().ButtonActive = true;
        player2_Dice_List.Add(dice_button);
        // dice_button.transform.position = Dice_Button_Pos[selectDiceNumber + 5].position;
        dice_button.GetComponent<MasterDiceScript>().MoveToPlace(Dice_Button_Pos[selectDiceNumber + 5].position);

        dice_button.transform.localScale = new Vector3(0.5f, 2f, 1f);
       

    }

    public void Dice_Choice()
    {

        if (selectDiceNumber > 5) return;

   

        //index= remain_dice_index의 인덱스번호 그번호에있는애가 당첨번호
        //select1,2 =당첨번호

        int index =UnityEngine.Random.Range(0, Remain_dice_Index.Count); //남아있는자리중 뽑음
        select1 = Remain_dice_Index[index];//그자리에있는번호가 당첨번호
        Remain_dice_Index.RemoveAt(index); //아하 인덱스를 넣어야지

        index = UnityEngine.Random.Range(0, Remain_dice_Index.Count);
        select2 = Remain_dice_Index[index];
        Remain_dice_Index.RemoveAt(index);
      
        ALL_Dice[select1].transform.position = ChoiceDice_Tr[0].position;
        ALL_Dice[select2].transform.position = ChoiceDice_Tr[1].position;

        ALL_Dice[select1].SetActive(true);
        ALL_Dice[select2].SetActive(true);

    }

    public GameObject GetDicePair(string dice_name)
    {
        if (DicePair[dice_name])
            return DicePair[dice_name];
        else
            return null;
    }

    public List<GameObject> GetDeck(int n)
    {
        if (n == 1)
            return player1_Dice_List;
        else
            return player2_Dice_List;
    }

    private void DeckChoiceFinish()
    {
        DeckChoiceFinish_delegate();
        DeckChoiceIng = false;

        GameManager.Instance.StartGame();

       
    }


    public void DicePowerUp_inDiceManager(string diceName)
    {
        for(int i=0;i< player2_Dice_List.Count;i++)
        {
            if (diceName.Contains(player2_Dice_List[i].name))
            {
                player2_Dice_List[i].GetComponent<MasterDiceScript>().DiceUpgrade(true);
                break;
            }
              
        }
    }


   

}
