using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;

    [HideInInspector] public TestSceneManager TestSceneManager;

    public UIManager UIManager;
    public AudioManager AudioManager;
    public InputManager InputManager;

    void Awake()
    {
        InitializeGameManager();
    }

    private void InitializeGameManager()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}