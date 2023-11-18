using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondFollow : MonoBehaviour
{
    public Transform followTransform;
    public float followSpeed = 1.0f;
    public float delay = 1.0f;
    public float maxSpeed = 5.0f;

    private float time = 0.0f;

    void Update()
    {
        Follow();
    }
    void Follow()
    {
        if (followTransform != null)
        {
            // Gecikmeyi takip et
            time += Time.deltaTime;

            if (time >= delay)
            {
                // takip edilecek objenin pozisyonunu al
                Vector3 takipEdilecekPozisyon = followTransform.position;

                // obje ile takip eden obje aras�ndaki mesafeyi hesapla
                float mesafe = Vector3.Distance(transform.position, takipEdilecekPozisyon);

                // h�z� ayarla (uzakla�t�k�a h�zlans�n, ancak maksimum h�z� ge�mesin)
                float hiz = Mathf.Min(mesafe * followSpeed, maxSpeed);

                // takip eden objenin pozisyonunu g�ncelle
                transform.position = Vector3.MoveTowards(transform.position, takipEdilecekPozisyon, hiz * Time.deltaTime);
            }
        }
    }

}
