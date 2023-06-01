using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrucción : MonoBehaviour
{
    public float vida;
    public float dañoHit;
    public int saltos = 1;
    Rigidbody2D rigidbody2D;
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

            if (collision.gameObject.tag != "Rojo" || collision.gameObject.tag != "Blanco")
            {
                vida -= dañoHit;

                if (gameObject.tag == "Enemigo" && saltos != 0)
                {
                    rigidbody2D.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
                    saltos--; 
                }
            }
        }
        
    }
}
