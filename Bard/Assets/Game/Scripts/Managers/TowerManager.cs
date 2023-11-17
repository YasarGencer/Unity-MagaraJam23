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

    PlayerMovementScript playerMovementScript;

    public void Initialize() {
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScript>();
        isInit = true;
    }
    private void Update() {
        //if (Input.GetKeyDown(KeyCode.K))
        //    RotateRight();
        //else if (Input.GetKeyDown(KeyCode.J))
        //    RotateLeft();
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
            playerMovementScript.RotateCaller(-angle, Vector3.up, 3 / rotationSpeed);
            tween = tower.DORotate(rotation, 3 / rotationSpeed, RotateMode.LocalAxisAdd).OnComplete(() => {
                tween = null;
                Vector3 currentRotation = tower.eulerAngles;
                float newYRotation = Mathf.Round(currentRotation.y / 90) * 90;
                tower.transform.rotation = Quaternion.Euler(currentRotation.x, newYRotation, currentRotation.z);
            });
            tween.Play();
        }
    }
}
