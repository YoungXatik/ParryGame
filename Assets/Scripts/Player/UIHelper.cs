using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHelper : MonoBehaviour
{
    [SerializeField] private GameObject blockUI;
    [SerializeField] private GameObject attackUI;
    [SerializeField] public EnemyMovement firstEnemy;

    private void Start()
    {
        attackUI.SetActive(false);
    }

    public void OpenBlockHelp()
    {
        blockUI.SetActive(true);
        Time.timeScale = 0.1f;
    }

    public void CloseBlockHelp()
    {
        blockUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenAttackHelp()
    {
        attackUI.SetActive(true);
    }

    public void CloseAttackUI()
    {
        attackUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void DestroyBlockUI()
    {
        OpenAttackHelp();
        Destroy(blockUI);
    }

    public void DestroyAttackUI()
    {
        Destroy(attackUI);
        firstEnemy.seeDistance = 20f;
    }


}
