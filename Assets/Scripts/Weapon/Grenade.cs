using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 2f;
    float countdown;
    public float radio = 5f;
    public float fuerzaExplosion = 70;
    bool exploted = false;


    public AudioClip shotSound;
    public AudioSource shotAudioSource;


    public GameObject efectoExplosion;

    private void Awake()
    {
        shotAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0 && !exploted)
        {
            Exploted();
            exploted = true;
        }

        if (exploted && !shotAudioSource.isPlaying)
            Destroy(gameObject);
    }

    private void Exploted()
    {
        shotAudioSource.PlayOneShot(shotSound);

        Instantiate(efectoExplosion, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radio);
        foreach (var objetos in colliders)
        {
            Rigidbody rb = objetos.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(fuerzaExplosion, transform.position,
                    radio);
            }
        }
    }
}