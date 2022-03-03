using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyBase : MonoBehaviour
{
    public int mode = 0;
    [SerializeField] 
    public float speed = 0.3f;
    private GameObject go_Turn1;
    private GameObject go_Turn2;
    private GameObject go_Turn3;
    private GameObject go_Turn4;
    private GameObject go_goal1;
    private GameObject go_goal2;

    GameObject IceEffectResource1;
    GameObject IceEffectResource2;
    GameObject IceEffectResource3;

    private float myHP;
    public float HP { get { return myHP; } set { myHP = value; } }
    protected Text HP_TextUI;

    public int slowDebuffPhase=0;
    public int SlowDebuffPhase { get { return slowDebuffPhase; }}

    private float slowCoefficient =0.05f;
    private float slowCoefficient_smallEnemy = 0f;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        go_Turn1 = GameObject.Find("Turn1");
        go_Turn2 = GameObject.Find("Turn2");
        go_Turn3 = GameObject.Find("Turn3");
        go_Turn4 = GameObject.Find("Turn4");
        go_goal1 = GameObject.Find("end_goal1");
        go_goal2 = GameObject.Find("end_goal2");

        float waveAddHp = 700f;
        if ((GameManager.Instance.WaveNumber - 1) > 3)
            waveAddHp = 2000f;
        
        myHP = 500f +( (GameManager.Instance.WaveNumber-2)* waveAddHp);

        if (name == "enemy_small(Clone)")
        {
          //  myHP *= 100f;
            slowCoefficient_smallEnemy = 0.1f;
        }
        if (name == "enemy_big(Clone)")
            myHP *= 3f;
        HP_TextUI = gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>();
        HP_TextUI.text = myHP.ToString();

        IceEffectResource1 = Resources.Load<GameObject>("Effect/Ice1");
        IceEffectResource2 = Resources.Load<GameObject>("Effect/Ice2");
        IceEffectResource3 = Resources.Load<GameObject>("Effect/Ice3");

        IceEffect1 = Instantiate(IceEffectResource1, transform.position, Quaternion.identity);
        IceEffect2 = Instantiate(IceEffectResource2, transform.position, Quaternion.identity);
        IceEffect3 = Instantiate(IceEffectResource3, transform.position, Quaternion.identity);
        IceEffect1.transform.parent = transform;
        IceEffect2.transform.parent = transform;
        IceEffect3.transform.parent = transform;

        IceEffect1.SetActive(false);
        IceEffect2.SetActive(false);
        IceEffect3.SetActive(false);

      

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
        //SlowTimerFunc();
        SlowTimerFunc();

    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == "Turn1")
        {
            mode = 1;

        }
        if (other.gameObject.name == "Turn2")
        {
            mode = 2;

        }
        if (other.gameObject.name == "Turn3")
        {
            mode = 3;

        }
        if (other.gameObject.name == "Turn4")
        {
            mode = 0;

        }
       
        if (other.gameObject.name == "end_goal1")
        {

            GameManager.Instance.BottomUserLifeDown();
            if(name.Contains("boss"))
                GameManager.Instance.BottomUserLifeDown();

            MyDie();
        }
        if (other.gameObject.name == "end_goal2")
        {
            GameManager.Instance.TopUserLifeDown();
            if (name.Contains("boss"))
                GameManager.Instance.TopUserLifeDown();

            MyDie();
        }

        if (other.gameObject.name== "SpawnPosition")
        {
            mode = 0;
        }
        if (other.gameObject.name == "SpawnPosition2")
        {
            mode = 2;
        }


    }
    private void Move()
    {
        
        if (mode == 0)
            transform.Translate(Vector2.up * Time.deltaTime * (speed - (slowDebuffPhase * (slowCoefficient+ slowCoefficient_smallEnemy))));
        else if (mode == 1)
            transform.Translate(Vector2.right * Time.deltaTime * (speed - (slowDebuffPhase * (slowCoefficient + slowCoefficient_smallEnemy))));
        else if (mode == 2)
            transform.Translate(Vector2.down * Time.deltaTime * (speed -(slowDebuffPhase * (slowCoefficient + slowCoefficient_smallEnemy))));
        else if (mode == 3)
            transform.Translate(Vector2.left * Time.deltaTime * (speed - (slowDebuffPhase * (slowCoefficient + slowCoefficient_smallEnemy))));

        // if(slowDebuffPhase>0)
        // Debug.Log("허허"+(speed - (slowDebuffPhase * slowCoefficient)));
    }

    public virtual void MyDie()
    {
        EnemyManager.Instance.RemoveEnemyinLIST(gameObject);
        Destroy(this.gameObject);
    }

    public void TakeDamage(float damage)
    {
        myHP -= damage;

        if(myHP<=0)
        {
            MyDie();
        }

        HP_TextUI.text = myHP.ToString();
    }

    GameObject IceEffect1;
    GameObject IceEffect2;
    GameObject IceEffect3;
    public void SlowDebuffFunc()
    {

             slowTimer = 0f;
        if (slowDebuffPhase >= 3) return;

        slowDebuffPhase = slowDebuffPhase + 1;
   

        SlowEffectOff();

        if(SlowDebuffPhase ==1)
            IceEffect1.SetActive(true);
        if (SlowDebuffPhase == 2)
            IceEffect2.SetActive(true);
        if (SlowDebuffPhase == 3)
            IceEffect3.SetActive(true);

        //Debug.Log("느려짐  "+ slowDebuffPhase);

     


    }

    private float slowTimer=0f;
    private float slowOffTime = 5f;

    private void SlowTimerFunc()
    {
        if (slowDebuffPhase < 1) return;

        slowTimer += Time.deltaTime;
        if (slowTimer>= slowOffTime+(slowDebuffPhase))
        {
            SlowEffectOff();
            slowDebuffPhase = 0;
            slowTimer = 0f;
        }
      

    }
    private void SlowEffectOff()
    {
       
       
            IceEffect1.SetActive(false);
            IceEffect2.SetActive(false);
            IceEffect3.SetActive(false);
    }
}
