
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

    [SerializeField] public GameObject blockEffect;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        blockEffect.SetActive(false);
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
}
