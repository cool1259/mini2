using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : BossBase
{
    protected override void BossSkill()
    {
        for (int i = 1; i <= 15; i++)
        {
            if (myPosPlayer.Dice_Pos_Dice_Array[i] != null)
                myPosPlayer.Dice_Pos_Dice_Array[i].GetComponent<DiceBase>().Boss3Debuff();
        }


    }
}
