using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickIt : MonoBehaviour
{
    [SerializeField] private Animator kickItAnim;
    [SerializeField] private Animator parryAnim;
    
    public void ChangeState()
    {
        kickItAnim.SetBool("StartFlex",true);
    }

    public void EndSecondState()
    {
        kickItAnim.SetBool("StartFlex",false);
    }

    public void ChangeParryState()
    {
        parryAnim.SetBool("Start",true);
    }

    public void EndParrySecondState()
    {
        parryAnim.SetBool("Start",false);
    }
}
