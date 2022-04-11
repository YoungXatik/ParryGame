using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickIt : MonoBehaviour
{
    [SerializeField] private Animator kickItAnim;
    
    public void ChangeState()
    {
        kickItAnim.SetBool("StartFlex",true);
    }

    public void EndSecondState()
    {
        kickItAnim.SetBool("StartFlex",false);
    }
}
