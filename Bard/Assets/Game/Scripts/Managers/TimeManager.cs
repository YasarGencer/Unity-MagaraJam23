using System;
using System.Collections;
using System.Collections.Generic; 
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour { 
    bool inInit = false;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float time;
    bool isCounting = false;
    public void Initialize() {
        if (inInit)
            return;
        isCounting = true;
    }
    private void Update() {
        if (isCounting == false)
            return;
        time += Time.deltaTime;
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        // TextMeshProUGUI nesnesine dakika ve saniyeyi yazdýr
        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
    public void Pause(bool pause) {
        isCounting = pause;
    }
}
