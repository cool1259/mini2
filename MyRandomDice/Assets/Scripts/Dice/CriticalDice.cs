using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalDice : DiceBase
{
    protected override void Start()
    {
        base.Start();


        Invoke("ArroundBuffOn", 0.1f);





    }

    //공격안함
    protected override void Attack()
    {
      

        
    }

    protected override void DiceDie(bool ReturnInUsePos = false)
    {


        ArroundBuffOff();
        base.DiceDie(ReturnInUsePos);


    }
    private void ArroundBuffOn()
    {
        var arr = go_playerController.GetComponent<PlayerController>().Dice_Pos_Dice_Array;

        // 주변버프
        if (pos_num % 5 != 0)
        {
            if (arr[pos_num + 1] != null)
                arr[pos_num + 1].GetComponent<DiceBase>().SetIncreaseCriticalBuff();
        }
        if (pos_num != 1 && pos_num != 6 && pos_num != 11)
        {
            if (arr[pos_num - 1] != null)
                arr[pos_num - 1].GetComponent<DiceBase>().SetIncreaseCriticalBuff();
        }
        if (pos_num<=10&&  arr[pos_num + 5] != null)
            arr[pos_num + 5].GetComponent<DiceBase>().SetIncreaseCriticalBuff();
        if (pos_num >=5 && arr[pos_num - 5] != null)
            arr[pos_num - 5].GetComponent<DiceBase>().SetIncreaseCriticalBuff();
    }
    public void ArroundBuffOff()
    {
        var arr = go_playerController.GetComponent<PlayerController>().Dice_Pos_Dice_Array;

        // 주변버프
        if (pos_num % 5 != 0)
        {
            if (arr[pos_num + 1] != null)
                arr[pos_num + 1].GetComponent<DiceBase>().SetDecreaseCriticalBuff();
        }
        if (pos_num != 1 && pos_num != 6 && pos_num != 11)
        {
            if (arr[pos_num - 1] != null)
                arr[pos_num - 1].GetComponent<DiceBase>().SetDecreaseCriticalBuff();
        }
        if (pos_num <= 10 && arr[pos_num + 5] != null)
            arr[pos_num + 5].GetComponent<DiceBase>().SetDecreaseCriticalBuff();
        if (pos_num >= 5 && arr[pos_num - 5] != null)
            arr[pos_num - 5].GetComponent<DiceBase>().SetDecreaseCriticalBuff();
    }
}
