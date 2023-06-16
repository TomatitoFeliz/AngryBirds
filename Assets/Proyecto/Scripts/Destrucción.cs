using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Destrucción : MonoBehaviour
{
    public float vida;
    public float dañoHit;
    public int saltos = 1;
    Rigidbody2D rigidbody2D;

    public Sprite[] fasesDestruction;

    public AudioClip hit, wood, pig;
    public AudioSource audioSource;

    private int faseDolor = 0;

    private void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (vida <= 0)
        {
            if (gameObject.tag == "MuroMadera")
            {
                EndGame.instance.puntuacion += 3000;
            }
            else
            {
                EndGame.instance.puntuacion += 5000;
            }
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (Time.time > 1)
            {
                if (collision.gameObject.tag == "Rojo" || collision.gameObject.tag == "Blanco")
                {
                    audioSource.PlayOneShot(hit);

                    if (collision.rigidbody.velocity.x >= 10 && vida <= 20)
                    {
                        PuntuacionVida();
                        Destroy(gameObject);
                    }
                    else
                    {
                        vida -= collision.rigidbody.velocity.x;
                        EndGame.instance.puntuacion += 1000;
                        audioSource.PlayOneShot(pig);
                    }
                    if (gameObject.tag == "CerdoNormal" && gameObject.tag == "CerdoTanke" && saltos != 0)
                    {
                        rigidbody2D.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
                        saltos--;
                    }
                }

                if (collision.gameObject.tag != "Rojo" || collision.gameObject.tag != "Blanco" || collision.gameObject.tag == "Huevo")
                {
                    vida -= dañoHit;
                    EndGame.instance.puntuacion += 1000;
                    if (gameObject.tag == "CerdoNormal" && gameObject.tag == "CerdoTanke" && saltos != 0)
                    {
                        rigidbody2D.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
                        saltos--;
                    }
                }

                if (collision.gameObject.tag == "Huevo")
                {

                    if (collision.rigidbody.velocity.y >= -10 && vida <= 20)
                    {
                        PuntuacionVida();
                        Destroy(gameObject);
                    }
                    else
                    {
                        EndGame.instance.puntuacion += 1000;
                        vida += collision.rigidbody.velocity.y;
                    }
                }
            }
            FaseChange();
        }
    }

    private void PuntuacionVida()
    {
        if (vida >= 15 && vida <= 20)
        {
            EndGame.instance.puntuacion += 10000;
        }
        else if (vida >= 10 && vida <= 15)
        {
            EndGame.instance.puntuacion += 3000;
        }
        else if (vida >= 0 && vida <= 10)
        {
            EndGame.instance.puntuacion += 1000;
        }
    }

    private void FaseChange()
    {
        //DestucciónCerdoTanke:
        if (gameObject.tag == "CerdoTanke")
        {
            if (vida <= 25 && faseDolor == 0)
            {
                audioSource.PlayOneShot(pig);
                gameObject.GetComponent<SpriteRenderer>().sprite = fasesDestruction[0];
                faseDolor++;
            }
            if (vida <= 10 && faseDolor == 1)
            {
                audioSource.PlayOneShot(pig);
                gameObject.GetComponent<SpriteRenderer>().sprite = fasesDestruction[1];
                faseDolor++;
            }
        }

        //DestucciónCerdoNormal:
        if (gameObject.tag == "CerdoNormal")
        {
            if (vida <= 15 && faseDolor == 0)
            {
                audioSource.PlayOneShot(pig);
                gameObject.GetComponent<SpriteRenderer>().sprite = fasesDestruction[0];
                faseDolor++;
            }
            if (vida <= 7 && faseDolor == 1)
            {
                audioSource.PlayOneShot(pig);
                gameObject.GetComponent<SpriteRenderer>().sprite = fasesDestruction[1];
                faseDolor++;
            }
        }

        //DestucciónMadera:
        if (gameObject.tag == "MuroMadera")
        {
            if (vida <= 10 && faseDolor == 0)
            {
                audioSource.PlayOneShot(wood);
                gameObject.GetComponent<SpriteRenderer>().sprite = fasesDestruction[0];
                faseDolor++;
            }
            if (vida <= 5 && faseDolor == 1)
            {
                audioSource.PlayOneShot(wood);
                gameObject.GetComponent<SpriteRenderer>().sprite = fasesDestruction[1];
                faseDolor++;
            }
        }
    }
}


