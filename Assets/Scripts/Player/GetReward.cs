using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetReward : MonoBehaviour
{
    public Animator chestAnim;
    public GameObject openEffect;
    public GameObject rewardEffect;
    public AudioSource rewardSource;
    public AudioClip openClip;
    public AudioClip rewardClip;

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
}
