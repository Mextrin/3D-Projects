using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneOnEnter : MonoBehaviour, IMenu
{
    [SerializeField]
    string levelName;

    public void OnEnter()
    {
        Application.LoadLevel(levelName);
    }
}
