using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class LevelMovementrotation : MonoBehaviour
{
    public GameObject player;
    public Vector3 rotationAngles;
    public float rotationDuration;
    public float nextPointDuration;
    public bool rotate;
    public Transform[] points;
    public int pointsCount;

    

    private void OnTriggerEnter(Collider other)
    {
        if (rotate)
        {
            player.transform.DORotate(rotationAngles, rotationDuration);
        }
        else
        {
            StartCoroutine(NextPoint());
        }
    }

    IEnumerator NextPoint()
    {
        var Seq = DOTween.Sequence();
        Seq.Append(player.transform.DOMove(points[pointsCount].position, rotationDuration));
        pointsCount++;
        if (pointsCount != points.Length)
        {
            StartCoroutine(NextPoint());
        }
        else
        {
            StopAllCoroutines();
        }
        yield return new WaitForSeconds(nextPointDuration);
    }
}
