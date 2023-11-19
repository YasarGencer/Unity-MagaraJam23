using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool isInit = false;
    [SerializeField] CanvasGroup pauseScreen;
    [SerializeField] Slider music, sfx;
    bool canPause = true;
    bool isPaused = false;
    public bool IsPaused {  get { return isPaused; } }
    public void Initialize() {
        isInit = true;
        music.onValueChanged.AddListener(MusicVolumeChanged);
        sfx.onValueChanged.AddListener(SFXVolumeChanged);
        Pause(false);
    }

    private void MusicVolumeChanged(float arg0) {
        GameManager.MusicVolume = arg0;
    }
    private void SFXVolumeChanged(float arg0) {
        GameManager.SFXVolume = arg0;
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
    private void Update() {
        if (!isInit) 
            return;
        if (!canPause) 
            return;
        if(SceneManager.GetActiveScene().buildIndex != 1)
            if (Input.GetKeyDown(KeyCode.Escape))
                Pause(!isPaused); 
    }
    private void OnApplicationFocus(bool focus) {
        if (focus == false)
            Pause(true);
    }
    public void Options(bool value) {
        pauseScreen.DOFade(value ? 1 : 0, .25f);
    }
    public void Pause(bool value) {
        music.value = GameManager.MusicVolume;
        sfx.value = GameManager.SFXVolume;
        canPause = false;
        isPaused = value;
        pauseScreen.DOFade(value ? 1 : 0, .25f).OnComplete(()=> canPause = true);
        MainManager.Instance.TimeManager.Pause(value);
    }
    public bool GetIfRotating() {
        return MainManager.Instance.TowerManager.GetIfRotating();
    } 

    public static float MusicVolume {
        get {
            return PlayerPrefs.GetFloat("music",.5f);
        }
        set {
            PlayerPrefs.SetFloat("music", value);
        }
    }
    public static float SFXVolume {
        get {
            return PlayerPrefs.GetFloat("sfx", .5f);
        }
        set {
            PlayerPrefs.SetFloat("sfx", value);
        }
    }
}
