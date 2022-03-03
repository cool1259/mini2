using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    private float click_turm = 1f;
    private GameObject drag_object;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( GameManager.Instance.bGameOver) return;

        click_turm += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {//마우스가 내려갔다!
            if (click_turm < 0.3f) return;
            click_turm = 0f;
            GameObject go;
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider)
            {
                if (!hit.collider.gameObject) return;
                go = hit.collider.gameObject;
                if (go.tag == "Enemy")
                {
                    go.GetComponent<EnemyBase>().MyDie();
                    //이렇게 하지말고 여기다가 해당 오브젝트의 클릭이벤트함수 호출하기??(
                    //그니까 각오브젝트마다 그놈의 클릭이벤트함수내가만드는거지
                    //아 상위오브젝트에 그거만들고 하위가 그거 오버라이딩??
                    //그래서 어떤오브젝트인지 검사안하고 그냥 그함수호출하기??
                }
                if (go.tag == "Dice")
                {
                    if (go.GetComponent<DiceBase>().bBoss2Debuff) return;
                    drag_object = go;
                    drag_object.GetComponent<DiceBase>().DragStart();
                   
                }
              
            }
        }
        //마우스끌기 일케해도되나?
        if (Input.GetMouseButton(0))
        {
            if (drag_object != null)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                drag_object.GetComponent<Transform>().position = new Vector3(pos.x, pos.y, -7.3f);
            }
        }
         
        if (Input.GetMouseButtonUp(0))
        {
            if(drag_object!=null)
            {
                drag_object.GetComponent<DiceBase>().DragEnd();
                drag_object = null;
            }
        }

    }

    ///// <summary>
    ///// 마우스가 내려간 오브젝트를 가지고 옵니다.
    ///// </summary>
    ///// <returns>선택된 오브젝트</returns>
    //private GameObject GetClickedObject()
    //{
    //    //충돌이 감지된 영역
    //    RaycastHit hit;
    //    //찾은 오브젝트
    //    GameObject target = null;

    //    //마우스 포이트 근처 좌표를 만든다.
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    //마우스 근처에 오브젝트가 있는지 확인
    //    if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))
    //    {//있다!
    //     //있으면 오브젝트를 저장한다.
    //        target = hit.collider.gameObject;
    //    }

    //    return target;
    //}
}
