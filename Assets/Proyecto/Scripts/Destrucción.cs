using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrucción : MonoBehaviour
{
    public float vida;
    public float dañoHit;
    public int saltos = 1;
    Rigidbody2D rigidbody2D;

    
    public Sprite[] fasesDestruction;
    private void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (vida <= 0)
        {
            Destroy(gameObject);
        }

        //DestucciónMadera:
        if (gameObject.tag == "MuroMadera")
        {
            if (vida <= 10)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = fasesDestruction[0];
            }
            if (vida <= 5)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = fasesDestruction[1];
            }
        }        
        
        //DestucciónCerdoNormal:
        if (gameObject.tag == "CerdoNormal")
        {
            if (vida <= 15)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = fasesDestruction[0];
            }
            if (vida <= 7)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = fasesDestruction[1];
            }
        }

        //DestucciónCerdoTanke:
        if (gameObject.tag == "CerdoTanke")
        {
            if (vida <= 25)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = fasesDestruction[0];
            }
            if (vida <= 10)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = fasesDestruction[1];
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time > 1)
        {
            if (collision.gameObject.tag == "Rojo" || collision.gameObject.tag == "Blanco")
            {
                if (collision.rigidbody.velocity.x >= 10 && vida <= 20)
                {
                    Destroy(gameObject);
                }        
                else
                {
                    vida -= collision.rigidbody.velocity.x;
                }
            }

            if (collision.gameObject.tag != "Rojo" || collision.gameObject.tag != "Blanco" || collision.gameObject.tag == "Huevo")
            {
                vida -= dañoHit;

                if (gameObject.tag == "CerdoNormal" && gameObject.tag == "CerdoTanke" && saltos != 0)
                {
                    rigidbody2D.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
                    saltos--; 
                }
            }
            
            if (collision.gameObject.tag == "Huevo")
            {
                Debug.Log(collision.rigidbody.velocity.y);

                if (collision.rigidbody.velocity.y >= -10 && vida <= 20)
                {
                    Destroy(gameObject);
                }
                else
                {
                    vida += collision.rigidbody.velocity.y;
                }
            }
        }
        
    }
}
