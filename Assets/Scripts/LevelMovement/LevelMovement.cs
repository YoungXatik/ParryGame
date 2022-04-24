using System;
using DG.Tweening;
using UnityEngine;

public class LevelMovement : MonoBehaviour
{
    public GameObject player;
    public Player playerAnim;
    public Transform[] points;
    public float movementDuration;

    public int currentEnemyCount;
    public int currentEnemyKills;

    public bool isNinja;

    private void Start()
    {
        player = GameObject.FindWithTag("PlayerObject");
        playerAnim = FindObjectOfType<Player>();
    }

    public void MoveNext()
    {
        currentEnemyKills++;
        
        if (currentEnemyKills == currentEnemyCount)
        {
            currentEnemyKills = 0;
            var Seq = DOTween.Sequence();
            Seq.Append(player.transform.DOMove(points[0].position, movementDuration).SetEase(Ease.Linear));
            Seq.Append(player.transform.DOMove(points[1].position, movementDuration).SetEase(Ease.Linear));
            StartWalk();
            Invoke("EndWalk",movementDuration * 2);
        }
        else
        {
            return;
        }
    }

    public void StartWalk()
    {
        if (isNinja)
        {
            playerAnim.deadAnimator.SetBool("NinjaWalk",true);
        }
        else
        {
            playerAnim.deadAnimator.SetBool("Walk",true);   
        }
        
    }

    public void EndWalk()
    {
        playerAnim.deadAnimator.SetBool("Walk",false);
        playerAnim.deadAnimator.SetBool("NinjaWalk",false);
    }
}
