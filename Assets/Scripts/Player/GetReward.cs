using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetReward : MonoBehaviour
{
    public Animator chestAnim;
    public GameObject openEffect;
    public GameObject rewardEffect;
    public AudioSource rewardSource;
    public AudioClip openClip;
    public AudioClip rewardClip;
    public GameObject nextLevelAnim;

    private void OnTriggerEnter(Collider other)
    {
        chestAnim.SetBool("Open",true);
    }

    private void Start()
    {
        rewardSource = GetComponent<AudioSource>();
        chestAnim = GetComponent<Animator>();
    }

    public void OpenChest()
    {
        openEffect.SetActive(true);
        rewardSource.PlayOneShot(openClip);
    }

    public void GetRewardFunc()
    {
        rewardEffect.SetActive(true);
        rewardSource.PlayOneShot(rewardClip);
        
    }

    public void NextLevelAnimStart()
    {
        nextLevelAnim.SetActive(true);
        chestAnim.SetBool("LoadLevel",true);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
