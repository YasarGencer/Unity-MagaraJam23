using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform platform;
    [SerializeField] Transform endPoint;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] float stopTime = 1;
    [SerializeField] float moveTime = 1;
    private void Start() {
        StartCoroutine(Move());
        lineRenderer.positionCount = 2;
    }
    public IEnumerator Move() {
        yield return new WaitForSeconds(stopTime);
        if (Vector2.Distance(Vector3.zero, platform.localPosition) > Vector2.Distance(endPoint.localPosition, platform.localPosition))
            platform.DOLocalMove(Vector3.zero, moveTime).OnComplete(() => StartCoroutine(Move()));
        else
            platform.DOLocalMove(endPoint.localPosition, moveTime).OnComplete(() => StartCoroutine(Move()));
    }
    private void Update() {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPoint.position);
    }
}
