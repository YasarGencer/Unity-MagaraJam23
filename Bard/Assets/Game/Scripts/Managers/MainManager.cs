using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    GameManager gameManager;
    TowerManager towerManager;

    public GameManager GameManager { get { return gameManager; } }
    public TowerManager TowerManager { get { return towerManager; } }

    private void Start() {
        Initialize();
    }
    private void Initialize() {
        Instance = this;

        gameManager = GetComponent<GameManager>();
        towerManager = GetComponent<TowerManager>();

        gameManager.Initialize();
        towerManager.Initialize();
    }
}
