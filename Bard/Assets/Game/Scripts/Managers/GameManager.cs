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
    public void MainMenu() {
        if (!isInit)
            return;
        SceneManager.LoadScene(1);
    }
    private void OnApplicationFocus(bool focus) {
        if (focus == false)
            Pause(false);
    }
    public void Pause(bool value) {

    }
    public bool GetIfRotating() {
        return MainManager.Instance.TowerManager.GetIfRotating();
    }
}
