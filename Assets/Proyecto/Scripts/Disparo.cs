using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Disparo : MonoBehaviour
{
    private Camera camara;
    private Rigidbody2D bolaRigidbody;
    private SpringJoint2D bolaSprintJoint;

    public GameObject bola;
    public Rigidbody2D pivote;
    public float tiempoQuitarSprintJoin;
    public float tiempoFinJuego;

    private bool estaArrastrando;

    void Start()
    {
        bolaSprintJoint.enabled = false;

        camara = Camera.main;

        bolaRigidbody = bola.GetComponent<Rigidbody2D>();
        bolaSprintJoint = bola.GetComponent<SpringJoint2D>();

        bolaSprintJoint.connectedBody = pivote;
    }

    void Update()
    {
        //Si no tiene asociado rigidbody nos salimos:
        if (bolaRigidbody == null) { return;  }

       if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            bolaSprintJoint.enabled = true;

            if (estaArrastrando)
            {
                LanzarBola();
            }

            estaArrastrando = false;

            return;
        }

        estaArrastrando = true;

        //Tomar control de la bola:
        bolaRigidbody.isKinematic = true;

        Vector2 posicionTocar = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector2 posicionMundo = camara.ScreenToWorldPoint(posicionTocar);

        bolaRigidbody.position = posicionMundo;

        Debug.Log(posicionMundo);
    }

    private void LanzarBola()
    {
        //La bola reacciona a la física dinámica:
        bolaRigidbody.isKinematic = false;
        bolaRigidbody = null;

        //retraso para desactivar el SprintJoin:
        Invoke(nameof(QuitarSprintJoin), tiempoQuitarSprintJoin);
    }

    private void QuitarSprintJoin()
    {
        bolaSprintJoint.enabled = false;
        bolaSprintJoint = null;

        Invoke(nameof(FinJuego), tiempoFinJuego);
    }

    private void FinJuego()
    {
        Debug.Log("FinJuego");
    }
}
