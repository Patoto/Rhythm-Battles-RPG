using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager
{
    //Singleton
    [HideInInspector] public static GameManager Instance;

    //Global managers
    [HideInInspector] public AudioManager AudioManager;
    [HideInInspector] public InputManager InputManager;
    [HideInInspector] public UIManager UIManager;

    //Other managers
    [HideInInspector] public GameplayManager GameplayManager;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        SetupSingleton();
        ConnectAllManagers();
    }

    private void SetupSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            RemoveParent();
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void RemoveParent()
    {
        transform.SetParent(null);
    }

    public override void ConnectManager() { }

    private void ConnectAllManagers()
    {
        Manager[] managers = FindObjectsOfType<Manager>();
        foreach (Manager tempManager in managers)
        {
            tempManager.ConnectManager();
        }
    }
}

public abstract class Manager : MonoBehaviour
{
    public abstract void ConnectManager();
}