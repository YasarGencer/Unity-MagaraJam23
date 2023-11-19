using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeSpawner : MonoBehaviour
{
    public GameObject theCube;
    public Transform spawnTransform;
    private GameObject yeniKup;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Deniyor");
            SpawnCube();
        }   
    }
    public void SpawnCube()
    {
        if (yeniKup!=null)
        {
            yeniKup.SetActive(false);
        }
        Vector3 spawnKonumu = spawnTransform.transform.position;

        // Yeni bir k�p spawnla
        yeniKup = Instantiate(theCube, spawnKonumu, Quaternion.identity);

        // �ste�e ba�l� olarak yeni k�p� B objesinin child'� yapabilirsiniz
        yeniKup.transform.parent = spawnTransform.transform;
        yeniKup.transform.localScale = yeniKup.transform.localScale * 0.95f;
        //Instantiate(theCube);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnCube();
        }
    }
}
