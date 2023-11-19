using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{ // Sahne y�klemek i�in kullan�lacak olan fonksiyon
    public void LoadScene(string sceneName)
    {
        // �stenen sahneyi y�kle
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        // Uygulamadan ��k
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
