using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton

    public static GameManager instance { get; private set; }

    public int gunAmmo = 10;
    public int vidas = 10;
    public int grenades = 10;
    public int tipoDeArma = 1; // 1 escopeta, 2 granada.
    public bool muerto = false; 

    private void Awake()
    {
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
    }
}