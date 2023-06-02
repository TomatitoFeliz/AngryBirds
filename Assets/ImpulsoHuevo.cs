using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulsoHuevo : MonoBehaviour
{
    public float fuerza = 1;
    private void Awake()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * fuerza, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
