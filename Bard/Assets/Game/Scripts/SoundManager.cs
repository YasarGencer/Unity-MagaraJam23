using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioClip loopMusic; // Döngü müziði dosyasý
    private AudioSource audioSource;

    void Start()
    {
        // Sahneler arasýnda yok edilmemesi için bu GameObject'i belirt
        DontDestroyOnLoad(gameObject);

        // Ses kaynaðýný oluþtur
        audioSource = gameObject.AddComponent<AudioSource>();

        // Döngü müziðini ayarla
        audioSource.clip = loopMusic;

        // Döngü müziðini çalmaya baþla ve loop özelliðini etkinleþtir
        audioSource.loop = true;
        audioSource.Play();
    }

    // Sahne yüklendiðinde çaðrýlan Unity fonksiyonu
    private void Update()
    {
        audioSource.volume = GameManager.MusicVolume;
    }
}
