using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    bool isInit = false;

    [SerializeField] Transform tower;
    [SerializeField] float rotationSpeed = 1;
    Tween tween;

    public void Initialize() {
        isInit = true;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.D))
            RotateRight();
        else if (Input.GetKeyDown(KeyCode.A))
            RotateLeft();
    }
    public bool GetIfRotating() {
        return tween != null;
    }
    public void RotateRight() {
        if (!isInit)
            return;
        Rotate(90);
    }
    public void RotateLeft() {
        if (!isInit)
            return;
        Rotate(-90);
    }
    void Rotate(float angle) {
        var rotation = new Vector3(tower.rotation.x, tower.rotation.y + angle, tower.rotation.z);
        if(tween == null) {
            tween = tower.DORotate(rotation, 3 / rotationSpeed, RotateMode.LocalAxisAdd).OnComplete(() => tween = null);
            tween.Play();
        }
    }
}
