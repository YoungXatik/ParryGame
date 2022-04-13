using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHelperForAttack : MonoBehaviour
{
    public GameObject attackUI;
    public EnemyMovement firstEnemyToFight;
    

    public void CloseAttackHelp()
    {
        attackUI.SetActive(false);
        firstEnemyToFight.seeDistance = 20f;
    }
}
