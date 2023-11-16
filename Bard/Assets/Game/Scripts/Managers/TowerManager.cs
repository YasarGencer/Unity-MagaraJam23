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

    public void Initialize() {
        isInit = true;
    } 

    void RotateRight() {
        if (!isInit)
            return;
        Rotate(90);
    }
    void RotateLeft() {
        if (!isInit)
            return;
        Rotate(-90);
    }
    void Rotate(float angle) {
        var rotation = new Vector3(tower.rotation.x, tower.rotation.y + angle, tower.rotation.z);
        tower.DORotate(rotation, 3 / rotationSpeed, RotateMode.WorldAxisAdd);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.D))
            RotateRight();
        else if (Input.GetKeyDown(KeyCode.A))
            RotateLeft();
    }
}
