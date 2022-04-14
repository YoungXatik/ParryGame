
using System;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    [SerializeField] public Animator shieldAnim;
    [SerializeField] public bool Block;
    [SerializeField] public bool canAttack;
    [SerializeField] public Animator cameraAnim;
    [SerializeField] private GameObject backgroundMusic;
    [SerializeField] private float backgroundMusicVolumeDown;

    private void Start()
    {
        shieldAnim = GetComponent<Animator>();
        backgroundMusic = GameObject.FindWithTag("BackGroundMusic");
        backgroundMusicVolumeDown = 0.2f;
    }

    public void Defend()
    {
        shieldAnim.SetBool("Block", true);
        // Time.timeScale = 1f;
        backgroundMusic.GetComponent<AudioSource>().volume += backgroundMusicVolumeDown;
    }

    public void ShieldEvent()
    {
        shieldAnim.SetBool("Block", false);
        Block = false;
    }

    public void TrueBlockStatus()
    {
        Block = true;
    }

    public void CameraBlockShakeStart()
    {
        cameraAnim.SetBool("BlockShake", true);
    }

    public void CameraBlockShakeEnd()
    {
        cameraAnim.SetBool("BlockShake", false);
    }

    public void CanAttackFalse()
    {
        canAttack = false;
    }

    public void CanAttackTrue()
    {
        canAttack = true;
    }

}
