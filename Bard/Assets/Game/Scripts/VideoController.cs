using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour {
    public VideoPlayer videoPlayer;

    void Start() {
        videoPlayer.loopPointReached += OnVideoFinished;

        // Video oynatma
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp) { 
        // Yeni sahne yükleniyor
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
