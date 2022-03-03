using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDice : DiceBase
{


    protected override void Start()
    {
        base.Start();
        InvokeRepeating("SpBonus",1f ,7f);
    }

    private void SpBonus()
    {
        if (GameManager.Instance.bGameOver) return;
        go_playerController.GetComponent<PlayerController>().AddMySp(15 * diceLevel);
        Instantiate(SpEffectResource, transform.position + new Vector3(0f, 0.2f, 0f), Quaternion.identity);

    }


}
