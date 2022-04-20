
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
    [SerializeField] private bool isKatana;
    [SerializeField] private BoxCollider katanaCollider;

    [Header("Effects")]
    [SerializeField] public GameObject attackEffect;
    [SerializeField] public GameObject hitEffect;

    [SerializeField] public AudioSource hitSource;
    [SerializeField] public AudioClip hitClip;
    [SerializeField] public AudioClip attackClip;

    [Header("Camera")]
    [SerializeField] private Camera mainCamera;

    [Header("SlashScript")]
    [SerializeField] public MeleeWeaponTrail weaponTrail; // Здесь подключаем скрипт

    private void Start()
    {
        weaponAnim = GetComponent<Animator>();
        hitEffect.SetActive(false);
        hitSource = GetComponent<AudioSource>();
        shieldScript = FindObjectOfType<ShieldScript>();

        if (isKatana)
        {
            weaponTrail = GetComponent<MeleeWeaponTrail>();
        }
    }



    public void Attack()
    {
        if (shieldScript.canAttack)
        {

            Random rand = new Random();
            int numberOfPunch = rand.Next(1, 3);

            if (numberOfPunch == 1)
            {
                weaponAnim.SetBool("Attack", true);
                attacked = true;
                hitSource.PlayOneShot(hitClip);
                if (isKatana)
                {
                    katanaCollider.enabled = false;
                }
            }
            else if (numberOfPunch == 20)
            {
                weaponAnim.SetBool("Attack1", true);
                attacked = true;
                hitSource.PlayOneShot(hitClip);
                if (isKatana)
                {
                    katanaCollider.enabled = false;
                }
            }
            else if (numberOfPunch == 2)
            {
                weaponAnim.SetBool("Attack2", true);
                attacked = true;
                hitSource.PlayOneShot(hitClip);
                if (isKatana)
                {
                    katanaCollider.enabled = false;
                }
            }
            else if (numberOfPunch == 4)
            {
                weaponAnim.SetBool("Attack3", true);
                attacked = true;
                hitSource.PlayOneShot(hitClip);
                if (isKatana)
                {
                    katanaCollider.enabled = false;
                }
            }
        }
        else
        {
            return;
        }
    }

    public void AttackShake2()
    {
        cameraAnim.SetBool("AttackShake2", true);
    }



    public void StartFOV()
    {
        cameraAnim.SetBool("FOVStart", true);
    }

    public void EndStartFOV()
    {
        cameraAnim.SetBool("FOVStart", false);
    }

    public void StartFOVBack()
    {
        cameraAnim.SetBool("FOVBack", true);
    }

    public void EndFOVBack()
    {
        cameraAnim.SetBool("FOVBack", false);
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
        shieldScript.canAttack = false;
        if (isKatana)
        {
            weaponTrail.Emit = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (other.GetComponent<EnemyMovement>().stunned)
            {
                other.GetComponent<EnemyMovement>().TakeDamage(damage);
                Debug.Log("Gotcha!");
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
        if (isKatana)
        {
            katanaCollider.enabled = true;
            weaponTrail.Emit = true;
        }

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
        cameraAnim.SetBool("HugeAttackShake", true);
    }


}
