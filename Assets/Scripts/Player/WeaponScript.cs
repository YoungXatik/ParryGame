
using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;
using DG.Tweening;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] public Animator weaponAnim;
    [SerializeField] public Animator cameraAnim;
    [SerializeField] public int damage;
    [SerializeField] private bool attacked;
    [SerializeField] public ShieldScript shieldScript;

    [Header("Effects")]
    [SerializeField] public GameObject attackEffect;
    [SerializeField] public GameObject hitEffect;

    [SerializeField] public AudioSource hitSource;
    [SerializeField] public AudioClip hitClip;
    [SerializeField] public AudioClip attackClip;

    [Header("Camera")] 
    [SerializeField] private Camera mainCamera;

    [SerializeField] private float startFOV;
    [SerializeField] private float cameraFOV;


    private void Start()
    {
        weaponAnim = GetComponent<Animator>();
        hitEffect.SetActive(false);
        hitSource = GetComponent<AudioSource>();
        shieldScript = FindObjectOfType<ShieldScript>();
        startFOV = mainCamera.fieldOfView;
    }



    public void Attack()
    {
        if (shieldScript.canAttack)
        {

            Random rand = new Random();
            int numberOfPunch = rand.Next(1, 2);

            if (numberOfPunch == 1)
            {
                weaponAnim.SetBool("Attack", true);
                attacked = true;
                hitSource.PlayOneShot(hitClip);
            }
            else if (numberOfPunch == 2)
            {
                weaponAnim.SetBool("Attack1", true);
                attacked = true;
                hitSource.PlayOneShot(hitClip);
            }
            else if (numberOfPunch == 3)
            {
                weaponAnim.SetBool("Attack2", true);
                attacked = true;
                hitSource.PlayOneShot(hitClip);
            }
            else if (numberOfPunch == 4)
            {
                weaponAnim.SetBool("Attack3", true);
                attacked = true;
                hitSource.PlayOneShot(hitClip);
            }
        }
        else
        {
            return;
        }
    }

    public void StartFOV()
    {
        cameraAnim.SetBool("FOVStart",true);
    }

    public void EndStartFOV()
    {
        cameraAnim.SetBool("FOVStart",false);
    }

    public void StartFOVBack()
    {
        cameraAnim.SetBool("FOVBack",true);
    }

    public void EndFOVBack()
    {
        cameraAnim.SetBool("FOVBack",false);
    }

    public void AttackEvent()
    {
        weaponAnim.SetBool("Attack", false);
        weaponAnim.SetBool("Attack1", false);
        weaponAnim.SetBool("Attack2", false);
        weaponAnim.SetBool("Attack3", false);
        attacked = false;
        hitEffect.SetActive(false);
        attackEffect.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (other.GetComponent<EnemyMovement>().stunned)
            {
                other.GetComponent<EnemyMovement>().TakeDamage(damage);
            }
            else
            {
                weaponAnim.SetBool("Attack", false);
                weaponAnim.SetBool("Attack1", false);
                weaponAnim.SetBool("Attack2", false);
                weaponAnim.SetBool("Attack3", false);
            }
        }
    }

    public void PlayHitEffect()
    {
        hitSource.PlayOneShot(attackClip);
        hitEffect.SetActive(true);
        attackEffect.SetActive(true);
        Instantiate(hitEffect, hitEffect.transform.position, Quaternion.identity);
    }

    public void HitEffectClose()
    {
        hitEffect.SetActive(false);
        attackEffect.SetActive(false);
    }

    public void SlowMotionStart()
    {
        Time.timeScale = 0.2f;
    }

    public void SlowMotionEnd()
    {
        Time.timeScale = 1f;
    }

    public void CameraAttackShake()
    {
        cameraAnim.SetBool("AttackShake", true);
    }

    public void HugeCameraAttackShake()
    {
        cameraAnim.SetBool("HugeAttackShake",true);
    }
}
