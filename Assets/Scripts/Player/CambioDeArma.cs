using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioDeArma : MonoBehaviour
{
    public GameObject[] arma;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CambiarDeArma();
    }


    void CambiarDeArma()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < arma.Length; i++)
                arma[i].SetActive(false);
            arma[0].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            for (int i = 0; i < arma.Length; i++)
                arma[i].SetActive(false);
            arma[1].SetActive(true);
        }
    }
}