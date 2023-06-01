using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Huevo : MonoBehaviour
{

    public Rigidbody2D rigidbody2D;

    private Camera camara;
    bool tiraHuevo;
    private Vector2 diferencia;
    private Vector2 pPivote;

    public GameObject prefabHuevo;

    private void Start()
    {
        tiraHuevo = false;
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        pPivote = gameObject.transform.position;
    }

    private void Update()
    {
        if(rigidbody2D.velocity.x != 0)
        {
            if (!Touchscreen.current.primaryTouch.press.isPressed)
            {

                if (tiraHuevo)
                {
                    LanzarHuevo();
                }

                tiraHuevo = false;

                return;
            }

            //Tomar control de la bola:
            Vector2 posicionTocar = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector2 posicionMundo = camara.ScreenToWorldPoint(posicionTocar);
            diferencia = posicionMundo - pPivote;

            if (diferencia.x < 4 && diferencia.x > -4 && diferencia.y < 4 && diferencia.y > -4)
            {
                tiraHuevo = true;
            }
            else
            {
                tiraHuevo = false;
            }
        }
    }
    void LanzarHuevo()
    {
        Instantiate(prefabHuevo, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1), Quaternion.identity);
    }
}
