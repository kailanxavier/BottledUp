using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : BaseAttackable
{
    public Timer timer;
    protected override void Attack()
    {
        Debug.Log("Game should end now");
        EndGame();
    }

    private void EndGame()
    {
        timer.gameEnded = true;
    }
}
