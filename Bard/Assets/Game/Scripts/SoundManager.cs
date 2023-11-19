using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioClip loopMusic; // D�ng� m�zi�i dosyas�
    private AudioSource audioSource;

    void Start()
    {
        // Sahneler aras�nda yok edilmemesi i�in bu GameObject'i belirt
        DontDestroyOnLoad(gameObject);

        // Ses kayna��n� olu�tur
        audioSource = gameObject.AddComponent<AudioSource>();

        // D�ng� m�zi�ini ayarla
        audioSource.clip = loopMusic;

        // D�ng� m�zi�ini �almaya ba�la ve loop �zelli�ini etkinle�tir
        audioSource.loop = true;
        audioSource.Play();
    }

    // Sahne y�klendi�inde �a�r�lan Unity fonksiyonu
    private void Update()
    {
        audioSource.volume = GameManager.MusicVolume;
    }
}
