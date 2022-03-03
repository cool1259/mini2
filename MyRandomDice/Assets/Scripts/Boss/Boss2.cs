using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : BossBase
{
    protected override void BossSkill()
    {
        int count = 0;
        List<GameObject> list = new List<GameObject>();
        for (int i = 1; i <= 15; i++)
        {
            if (myPosPlayer.Dice_Pos_Dice_Array[i] != null && myPosPlayer.Dice_Pos_Dice_Array[i].GetComponent<DiceBase>().bBoss2Debuff == false)
                list.Add(myPosPlayer.Dice_Pos_Dice_Array[i]);
            // Dice_Pos_Dice_Array[i].GetComponent<DiceBase>().Boss2Debuff();
        }



       if (list.Count == 0) return;

        if (list.Count == 1)
        {
            list[0].GetComponent<DiceBase>().Boss2Debuff();
            Debug.Log("언제");
        }
        else
        {
            
                int n = Random.Range(0, list.Count);
                list[n].GetComponent<DiceBase>().Boss2Debuff();       
                list.Clear();
            for (int i = 1; i <= 15; i++)
            {
                if (myPosPlayer.Dice_Pos_Dice_Array[i] != null && myPosPlayer.Dice_Pos_Dice_Array[i].GetComponent<DiceBase>().bBoss2Debuff == false)
                    list.Add(myPosPlayer.Dice_Pos_Dice_Array[i]);
                // Dice_Pos_Dice_Array[i].GetComponent<DiceBase>().Boss2Debuff();
            }

            int n2 = Random.Range(0, list.Count);
            list[n2].GetComponent<DiceBase>().Boss2Debuff();
                   
                


            
        }



    }
}
