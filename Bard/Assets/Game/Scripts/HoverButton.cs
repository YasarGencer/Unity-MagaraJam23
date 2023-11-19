using UnityEngine;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour
{
    public Button targetButton; // Büyütülecek olan buton
    public float scaleFactor = 1.2f; // Büyütme faktörü

    private Vector3 originalSize; // Orijinal boyut

    private void Start()
    {
        // Baþlangýçta orijinal boyutu kaydet
        originalSize = targetButton.GetComponent<RectTransform>().sizeDelta;
    }

    public void OnPointerEnter()
    {
        // Mouse butonun üzerine geldiðinde büyüt
        RectTransform buttonRect = targetButton.GetComponent<RectTransform>();
        buttonRect.sizeDelta = originalSize * scaleFactor;
    }

    public void OnPointerExit()
    {
        // Mouse butonun üzerinden çýkýnca orijinal boyuta dön
        RectTransform buttonRect = targetButton.GetComponent<RectTransform>();
        buttonRect.sizeDelta = originalSize;
    }
}
