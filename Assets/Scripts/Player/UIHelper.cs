using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHelper : MonoBehaviour
{
    [SerializeField] private GameObject blockUI;
    [SerializeField] private GameObject attackUI;

    public void OpenBlockHelp()
    {
        blockUI.SetActive(true);
    }

    public void CloseBlockHelp()
    {
        blockUI.SetActive(false);
        attackUI.SetActive(true);
    }

    
    



}
