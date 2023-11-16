using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool isInit = false;
    public void Initialize() {
        isInit = true;
    } 
    public void Die() {
        if (!isInit)
            return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public bool GetIfRotating() {
        return MainManager.Instance.TowerManager.GetIfRotating();
    }
}
