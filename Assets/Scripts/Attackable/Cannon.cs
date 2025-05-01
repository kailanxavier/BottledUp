using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Cannon : BaseAttackable
{
    public Timer timer;

    protected override void Attack()
    {
        EndGame();
    }

    private void EndGame()
    {
        timer.gameEnded = true;
    }

}
