using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class MasterDiceScript : MonoBehaviour
{
    private bool buttonActive = false;
    public bool ButtonActive { get => buttonActive; set => buttonActive = value; }
    private Text myText;
    public int DicePower = 1;
    public event Action myDelegate;
    public bool isMyPick = false;
    private GameObject go_playerController;
    private GameObject mySpText;
    private bool go_to_my_place = false;
    Vector3 Destination = new Vector3(0f, 0f, 0f);
    //public bool ButtonActive { set{ buttonActive = value; } }
    // Start is called before the first frame update
    void Start()
    {
        go_playerController = GameObject.Find("PlayerController");
        mySpText = transform.GetChild(1).gameObject;
        GetComponent<Button>().onClick.AddListener(DiceButtonClicked);
        myText = gameObject.transform.GetChild(0).GetComponent<Text>();
        myText.text = "LV.1";
        myText.fontStyle = FontStyle.Bold;
        myText.rectTransform.localScale = new Vector3(0.84f, 0.18f, 1f);
        myText.fontSize = 27;


        InitializeOP(50); // 오브젝트풀생성
    }

    // Update is called once per frame
    void Update()
    {
        if(go_to_my_place)
        {
            transform.position = Vector3.Lerp(transform.position, Destination, Time.deltaTime*3f);
        }
    }

    public void DiceButtonClicked()
    {
        if (GameManager.Instance.bGameOver) return;
        if (ButtonActive == false)
        {
            DiceManager.Instance.player1_AddDice(gameObject);
        
            mySpText.SetActive(true);
        }
        else
        {
            if (DiceManager.Instance.DeckChoiceIng) return;


            DiceUpgrade();
        }
    }

    public void DiceUpgrade(bool isAi=false)
    {
        if (DicePower >= 5) return;
        if (!isAi && !isMyPick) return;

        if (go_playerController.GetComponent<PlayerController>().GetMySp() < 100) return;
        go_playerController.GetComponent<PlayerController>().AddMySp(-100);
        DicePower++;
        myText.text = "LV." + DicePower.ToString();


        myDelegate?.Invoke();

    }


    public void MoveToPlace(Vector3 dest)
    {
        Destination = dest;
        go_to_my_place = true;
    }

    /////////////////////////////////////////////////////
    ///
    //마스터 주사위들은 각자 자기종류의 오브젝트풀을 가지고있음
    //주사위들은 자기의 슈퍼바이져(마스터주사위) 의 오브젝트풀에서 총알을 꺼내서씀
    [SerializeField]
    private GameObject poolingObjectPrefab;
    Queue<GameObject> poolingObjectQueue = new Queue<GameObject>();





    private void InitializeOP(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }


    }
    private GameObject CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab);
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }
    public  GameObject GetObject()
    {
        if (poolingObjectQueue.Count > 0)
        {
            var obj = poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true); return obj;
        }
        else
        {
            var newObj = CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }
    public void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
        poolingObjectQueue.Enqueue(obj);
    }
}
