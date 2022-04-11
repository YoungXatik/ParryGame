
using System;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    [SerializeField] public Animator shieldAnim;
    [SerializeField] public bool Block;
    [SerializeField] public Animator cameraAnim;
     
    private void Start()
    {
        shieldAnim = GetComponent<Animator>();
    }

    public void Defend()
    {
        shieldAnim.SetBool("Block",true);
    }
    
    public void ShieldEvent()
    {
        shieldAnim.SetBool("Block",false);
        Block = false;
    }

    public void TrueBlockStatus()
    {
        Block = true;
    }
    
    public void CameraBlockShakeStart()
    {
        cameraAnim.SetBool("BlockShake",true);
    }
    
    public void CameraBlockShakeEnd()
    {
        cameraAnim.SetBool("BlockShake",false);
    }
}
