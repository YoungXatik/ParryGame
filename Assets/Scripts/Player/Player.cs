
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public Animator deadAnimator;
    [SerializeField] public GameObject shield;
    [SerializeField] public GameObject weapon;
    [SerializeField] public AudioSource source;
    [SerializeField] public AudioClip walkClip;

    [SerializeField] public ParticleSystem blockEffect;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        blockEffect.gameObject.SetActive(true);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void Update()
    {
        if (health <= 0)
        {
            shield.SetActive(false);
            weapon.SetActive(false);
            deadAnimator.SetBool("Dead", true);
            Invoke("RestartLevel", 2f);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CameraBack()
    {
        deadAnimator.SetBool("GetHit", false);
    }

    public void StartWalking()
    {
        deadAnimator.SetBool("Walk", true);
    }

    public void PlayWalkSound()
    {
        source.PlayOneShot(walkClip);
    }

    public void AttackShakeEnd()
    {
        deadAnimator.SetBool("AttackShake", false);
    }

    public void HugeAttackShakeEnd()
    {
        deadAnimator.SetBool("HugeAttackShake", false);
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
    }
}
