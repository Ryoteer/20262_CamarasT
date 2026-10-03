using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Instance

    public static GameManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private BasePlayerModel _basePlayer;

    public BasePlayerModel BasePlayer
    {
        get { return _basePlayer; }
        set { _basePlayer = value; }
    }
}
