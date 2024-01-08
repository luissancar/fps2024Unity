using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    public TextMeshProUGUI textAmmo;
    public TextMeshProUGUI textGrenades;


    public TextMeshProUGUI textVidas;

    public bool vulnerable = true;

    // Start is called before the first frame update
    void Start()
    {
        textVidas.text = GameManager.instance.vidas.ToString();
        textAmmo.text = GameManager.instance.gunAmmo.ToString();
        textGrenades.text = GameManager.instance.grenades.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemigo") && vulnerable)
        {
           PerderVida();
        }

        if (other.CompareTag("AmmoBox"))
        {
            GameManager.instance.gunAmmo += other.GetComponent<AmmoBox>().ammo;
            if (GameManager.instance.tipoDeArma == 1)
                textAmmo.text = GameManager.instance.gunAmmo.ToString();
            Destroy(other.gameObject);
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BulletEnemy"))
        {
            PerderVida();
        }
    }

    void PerderVida()
    { vulnerable = false;
        GameManager.instance.vidas -= 2;
        if (GameManager.instance.vidas <= 0)
            muerto();
        else
        {
            textVidas.text = GameManager.instance.vidas.ToString();
            StartCoroutine(VulnerableCorrutina());
        }
        
    }
    private void muerto()
    {
        GameManager.instance.muerto = true;
        textVidas.text = "0";

        // Obtener el componente Transform del GameObject actual
        Transform
            transformacion =
                transform; // Modificar la rotación alrededor del eje X (por ejemplo, rotar en X en cada frame)

        // Establecer un valor fijo para la rotación en el eje X
        float nuevoAnguloX = -128f; // Puedes ajustar el ángulo según tus necesidades
        transformacion.eulerAngles =
            new Vector3(nuevoAnguloX, transformacion.eulerAngles.y, transformacion.eulerAngles.z);

        StartCoroutine(ComenzarCorrutina());
    }


    IEnumerator VulnerableCorrutina()
    {
        yield return new WaitForSeconds(3);
        vulnerable = true;
    }

    IEnumerator ComenzarCorrutina()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}