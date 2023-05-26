using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrucción : MonoBehaviour
{
    public float vida;
    private int daño;

    Rigidbody2D rigidbody2D;

    private void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude < 10)
        {
            daño = 1;
        }
    }
}
