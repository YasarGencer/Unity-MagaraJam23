using UnityEngine;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour
{
    public Button targetButton; // B�y�t�lecek olan buton
    public float scaleFactor = 1.2f; // B�y�tme fakt�r�

    private Vector3 originalSize; // Orijinal boyut

    private void Start()
    {
        // Ba�lang��ta orijinal boyutu kaydet
        originalSize = targetButton.GetComponent<RectTransform>().sizeDelta;
    }

    public void OnPointerEnter()
    {
        // Mouse butonun �zerine geldi�inde b�y�t
        RectTransform buttonRect = targetButton.GetComponent<RectTransform>();
        buttonRect.sizeDelta = originalSize * scaleFactor;
    }

    public void OnPointerExit()
    {
        // Mouse butonun �zerinden ��k�nca orijinal boyuta d�n
        RectTransform buttonRect = targetButton.GetComponent<RectTransform>();
        buttonRect.sizeDelta = originalSize;
    }
}
