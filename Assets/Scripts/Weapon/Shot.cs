using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public Transform spawnPoint; //punto salida de la bala
    public GameObject bullet;

    public float shotForce = 10000;

    public float shotRate = 0.5f; //tiempo próximo disparo

    private float shotRateTime = 0;


    public AudioClip shotSound;
    private AudioSource shotAudioSource;

    public TextMeshProUGUI textAmmo;

    // Start is called before the first frame update
    private void Awake()
    {
        shotAudioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (GameManager.instance.muerto)
                return;
            if (Time.time > shotRateTime
                && GameManager.instance.gunAmmo > 0)
            {
                CambiarCanvas();
                
                if (shotSound != null)
                    shotAudioSource.PlayOneShot(shotSound);
                GameObject newBullet = Instantiate(
                    bullet, spawnPoint.position, spawnPoint.rotation);

                newBullet.GetComponent<Rigidbody>()
                    .AddForce(spawnPoint.forward * shotForce * Time.deltaTime, ForceMode.Impulse);
                shotRateTime = Time.time + shotRate;
                //Destroy(newBullet, 10);

                /////////////////////////crear prefab bala
            }
        }
    }

    private void CambiarCanvas()
    {
        switch (GameManager.instance.tipoDeArma)
        {
            case  1:// escopeta
                GameManager.instance.gunAmmo--;
                textAmmo.text = GameManager.instance.gunAmmo.ToString();
                break;
            case  2:// escopeta
                GameManager.instance.grenades--;
                textAmmo.text = GameManager.instance.grenades.ToString();
                break;
        }



    }
}