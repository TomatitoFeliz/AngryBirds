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
    public GameObject pivoteGameObject;
    public Rigidbody2D pivote;
    public float tiempoQuitarSprintJoin;
    public float tiempoFinJuego;

    private Vector2 pPivote;
    private Vector2 diferencia;
    private Vector2 pInicial;

    private bool estaArrastrando;

    public LineRenderer bolaLineRenderer;
    public GameObject canasto;

    void Start()
    {
        bolaLineRenderer.enabled = false;

        pInicial = bola.transform.position;
        pPivote = pivoteGameObject.transform.position;

        bolaSprintJoint = bola.GetComponent<SpringJoint2D>();        
        bolaSprintJoint.connectedBody = pivote;

        camara = Camera.main;

        bolaRigidbody = bola.GetComponent<Rigidbody2D>();
        bolaRigidbody.isKinematic = true;
    }

    void Update()
    {
        bolaLineRenderer.SetPosition(1, bola.transform.position);

        //Si no tiene asociado rigidbody nos salimos:
        if (bolaRigidbody == null) { return;  }

       if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
          
            if (estaArrastrando)
            {
                LanzarBola();
            }
        
            estaArrastrando = false;

            return;
        }

        //Tomar control de la bola:
        Vector2 posicionTocar = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector2 posicionMundo = camara.ScreenToWorldPoint(posicionTocar);

        Debug.Log("Diferencia" + (posicionMundo - pPivote));
        diferencia = (posicionMundo - pPivote);

        if (diferencia.x < 4 && diferencia.x > -4 && diferencia.y < 4 && diferencia.y > -4)
        {
            canasto.SetActive(true);
            bolaLineRenderer.enabled = true;

            bolaSprintJoint.enabled = true;
            estaArrastrando = true;
            bolaRigidbody.position = posicionMundo;
        }
        else
        {
            canasto.SetActive(false);
            bolaLineRenderer.enabled = false;

            bolaRigidbody.position = pInicial;
            bolaSprintJoint.enabled = false;
            estaArrastrando = false;
        }


        Debug.Log(posicionMundo);
    }

    private void LanzarBola()
    {
        canasto.SetActive(false);
        bolaLineRenderer.enabled = false;

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
