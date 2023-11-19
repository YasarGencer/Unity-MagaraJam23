using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{ // Sahne yüklemek için kullanýlacak olan fonksiyon
    public void LoadScene(string sceneName)
    {
        // Ýstenen sahneyi yükle
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        // Uygulamadan çýk
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
