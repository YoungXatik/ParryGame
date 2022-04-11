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
            Seq.Append(player.transform.DOMove(points[0].position, movementDuration));
            Seq.Append(player.transform.DOMove(points[1].position, movementDuration));
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
        playerAnim.deadAnimator.SetBool("Walk",true);
    }

    public void EndWalk()
    {
        playerAnim.deadAnimator.SetBool("Walk",false);
    }
}
